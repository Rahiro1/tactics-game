using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager
{
    private bool isBattleOver;
    private WaitForSeconds waitForTenth = new WaitForSeconds(0.1f);
    private WaitForSeconds waitForFifth = new WaitForSeconds(0.2f);


    public IEnumerator ConductBattle(UnitController attackerUnit, UnitController defenderUnit)
    {
        GameManager gameManager = GameManager.Instance;
        GameEvents gameEvents = GameEvents.Instance;
        gameEvents.TriggerSkills(Define.SkillTriggerType.BattleStart);

        yield return gameManager.StartCoroutine(gameManager.cameraMovement.PanTo(attackerUnit.Location, defenderUnit.Location));

        yield return gameManager.StartCoroutine(ResolveBattle(attackerUnit, defenderUnit));
        if (!attackerUnit.IsDestroyed)
        {
            yield return gameManager.StartCoroutine(attackerUnit.Character.TriggerExperienceGain(defenderUnit));
        }
        if (!defenderUnit.IsDestroyed)
        {
            yield return gameManager.StartCoroutine(defenderUnit.Character.TriggerExperienceGain(attackerUnit));
        }
        gameEvents.TriggerSkills(Define.SkillTriggerType.BattleEnd);
        //yield return gameManager.StartCoroutine(testMethod());
        yield break;
    }

    public IEnumerator ResolveBattle(UnitController attackerUnit, UnitController defenderUnit)
    {
        GameManager gameManager = GameManager.Instance;
        Character attacker = attackerUnit.Character;
        Character defender = defenderUnit.Character;
        int distanceBetweenCombatants = Mathf.Abs(attackerUnit.Location.x - defenderUnit.Location.x) + Mathf.Abs(attackerUnit.Location.y - defenderUnit.Location.y);
        bool canDefenderRetalite = false;

        isBattleOver = false; // set to true if a character dies

        // determine if defender can retaliate
        if (defender.EquippedWeapon != null)
        {
            if (distanceBetweenCombatants <= defenderUnit.Character.EquippedWeapon.BonusRange)
            {
                canDefenderRetalite = true;
            }
        }

        // TODO - make the following into a method called up to four times
        yield return gameManager.StartCoroutine(PerformCombatRound(attackerUnit, defenderUnit));

        if (canDefenderRetalite && !isBattleOver)
        {
            yield return gameManager.StartCoroutine(PerformCombatRound(defenderUnit, attackerUnit));
        }


        if (attacker.Speed.GetModifiedValue() >= defender.Speed.GetModifiedValue() + Define.SPEEDTHRESHOLD && attacker.EquippedWeapon != null && !isBattleOver)
        {
            yield return gameManager.StartCoroutine(PerformCombatRound(attackerUnit, defenderUnit));

        } else if (defender.Speed.GetModifiedValue() >= attacker.Speed.GetModifiedValue() + Define.SPEEDTHRESHOLD && !isBattleOver)
        {
            if (canDefenderRetalite)
            {
                yield return gameManager.StartCoroutine(PerformCombatRound(defenderUnit, attackerUnit));
            }

        }

        //gameManager.skillNotificationDisplay.UnloadAll();
        yield break;
    }

    private IEnumerator PerformCombatRound(UnitController attackerUnit, UnitController defenderUnit)
    {
        GameManager gameManager = GameManager.Instance;
        Character attacker = attackerUnit.Character;
        Character defender = defenderUnit.Character;

        float rnd = RandomNumbers.DoubleRNCombatRoll(); //TODO Generate random numbers properly

        yield return waitForFifth;
        yield return gameManager.StartCoroutine(attackerUnit.PlayMapAttackAnimationStart(defenderUnit));
        if (attacker.OffensiveHit - defenderUnit.UnitTotalAvoid > rnd)
        {
            int critRate = attacker.CriticalRate - defender.CriticalAvoid;

            //Debug.Log("first attack hit");
            if (ResolveHit(attacker, defenderUnit))
            {
                yield return gameManager.StartCoroutine(attackerUnit.PlayMapAttackAnimationEnd());
                isBattleOver = true;
                yield break;
            }

        }
        yield return gameManager.StartCoroutine(attackerUnit.PlayMapAttackAnimationEnd());
    }

    private bool ResolveHit(Character damageDealer, UnitController damageRecieverUnit) // returns true if unit was destroyed
    {
        // the bool that this function returns is whether the defending units hp reached 0 - returns true if this happens 
        Character damageReciever = damageRecieverUnit.Character;
        int damagedealt;
        int power = damageDealer.Attack;
        //Debug.Log("Attacking unit attack: " + power.ToString());
        int reduction;
        int rending = damageDealer.Rending;
        int bonusRending = 0;
        int skillThreshold = 0;

        if (damageDealer.EquippedWeapon.IsMagical)
        {
            reduction = damageReciever.Resistance.GetModifiedValue();
        }
        else
        {
            reduction = damageRecieverUnit.UnitTotalGuard;
        }

        //Debug.Log("Defending unit Guard: " + reduction.ToString());

        damagedealt = power - reduction;

        skillThreshold = (damageDealer.CriticalRate - damageReciever.CriticalAvoid)*100/damageReciever.CriticalAvoid;
        
        damagedealt = damageDealer.DetermineSkillBonusDamage(damagedealt, rending, skillThreshold, out bonusRending);

        rending += bonusRending;

        //Debug.Log("damage dealt equals " + damagedealt.ToString());
        // use up weapon durability
        damageDealer.OnItemUse(damageDealer.EquippedWeapon);
        

        return damageRecieverUnit.TakeDamage(damagedealt, rending);

    }

    public int DeterminePotentialDamage(Character attacker, Weapon weapon, Character defender)
    {

        if (!attacker.CanEquipWeapon(weapon))
        {
            return -1;
        }

        int damage;

        Weapon startingWeapon = attacker.EquippedWeapon;

        attacker.EquipWeapon(weapon);

        if (weapon.IsMagical)
        {
            damage = Mathf.Max(0,attacker.Attack - defender.Resistance.GetModifiedValue());
        }
        else
        {
            damage = attacker.Attack - defender.Guard; // CONSIDER - this doesn't take terrain into account
        }
        
        if(attacker.Speed.GetModifiedValue() >= defender.Speed.GetModifiedValue() + Define.SPEEDTHRESHOLD)
        {
            damage *= 2;
        }

        if(startingWeapon == null)
        {
            attacker.UnEquipWeapon();
        }
        else
        {
            attacker.EquipWeapon(startingWeapon);
        }

        return damage;
    }

    public int DetermineWeaponHitRate(Character attacker, Weapon weapon, Character defender)
    {
        if (!attacker.CanEquipWeapon(weapon))
        {
            return -1;
        }

        Weapon startingWeapon = attacker.EquippedWeapon;

        attacker.EquipWeapon(weapon);


        int hitRate = Mathf.Clamp(attacker.OffensiveHit - defender.Avoid,0,100);

        if (startingWeapon == null)
        {
            attacker.UnEquipWeapon();
        }
        else
        {
            attacker.EquipWeapon(startingWeapon);
        }

        return hitRate;
    }

    public int DetermineWeaponScore(Character attacker, Weapon weapon, Character defender)
    {
        int score = 0;
        int damage = DeterminePotentialDamage(attacker, weapon, defender);
        int hitRate = DetermineWeaponHitRate(attacker, weapon, defender); // CONSIDER - this doesn't take terrain into account
        int defenderHealth = defender.currentHP;

        if (hitRate >= 50 && damage >= defenderHealth)
        {
            score += 10000 + hitRate*100;
        }
        if(hitRate >= 25 && damage >= defenderHealth)
        {
            score += 5000 + hitRate*100;
        }

        if(defender.EquippedWeapon == null)
        {
            score += 1000;
        }
        else if(defender.EquippedWeapon.BonusRange < weapon.BonusRange)
        {
            score += 1000;
        }

        score += damage;
        score -= defender.Defence.GetModifiedValue();

        if (weapon.Rending >= 3 && weapon.Rending > defender.currentArmour * 0.2f)
        {
            score += weapon.Rending * 2;
        }


        if (hitRate == 0)
        {
            score = 0;
        }

        return score;
    }

    private IEnumerator testMethod()
    {
        //Debug.Log("Test method okay"); // TODO - figure out why this method is neccessary
        yield break;
    }
}
