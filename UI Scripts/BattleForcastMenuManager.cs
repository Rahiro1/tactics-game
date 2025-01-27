using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleForcastMenuManager : MonoBehaviour
{
    [SerializeField] Button upButton, downButton;
    [SerializeField] TextMeshProUGUI hpAtkText, dmgAtkText, hitAtkText, amrAtkText, crtAtkText;
    [SerializeField] TextMeshProUGUI hpDefText, dmgDefText, hitDefText, amrDefText, crtDefText;
    [SerializeField] TextMeshProUGUI attackerDoubleText, defenderDoubleText;
    [SerializeField] Image attackerWeaponIcon, defenderWeaponIcon;
    [SerializeField] TextMeshProUGUI atkWeaponText, defWeaponText;
    private UnitController source;
    private UnitController target;



    public void OpenMenu(UnitController attackerUnit, UnitController defenderUnit)
    {
        Character attacker = attackerUnit.Character;
        Character defender = defenderUnit.Character;
        source = attackerUnit;
        target = defenderUnit;
        bool canDefenderRetaliate = true;

        // TODO - if defender is disarmed there will be problems, replace all defender assignments with "" or "N/A" or "-"

        if (defender.EquippedWeapon == null)
        {
            canDefenderRetaliate = false;
        } else if (defender.Range < GameManager.Instance.levelMapManager.GetManhattenDistance(attackerUnit.LocationTile, defenderUnit.LocationTile))
        {
            canDefenderRetaliate = false;
        }

        attackerDoubleText.gameObject.SetActive(false);
        defenderDoubleText.gameObject.SetActive(false);


        hpAtkText.text = attackerUnit.currentHP.ToString();
        
        if(attacker.EquippedWeapon.IsMagical)
        {
            dmgAtkText.text = Mathf.Max(0,attacker.Attack - defender.ModifiedResistance).ToString();
        } else
        {
            dmgAtkText.text = Mathf.Max(0,attacker.Attack - defenderUnit.UnitTotalGuard).ToString();
        }
        if (attacker.ModifiedSpeed >= defender.ModifiedSpeed + Define.SPEEDTHRESHOLD)
        {
            attackerDoubleText.gameObject.SetActive(true);
        }
        hitAtkText.text = Mathf.Clamp(attacker.OffensiveHit - defenderUnit.UnitTotalAvoid, 0, 100).ToString();
        amrAtkText.text = attacker.EquippedWeapon.rending.ToString();
        crtAtkText.text = Mathf.Max(0, (attacker.CriticalRate - defender.CriticalAvoid) * 100 / defender.CriticalAvoid).ToString(); // TODO use units here for crit avoid? Terrain?
        attackerWeaponIcon.sprite = attacker.EquippedWeapon.GetItemSO().itemIcon;
        atkWeaponText.text = attacker.EquippedWeapon.ItemName;

        hpDefText.text = defenderUnit.currentHP.ToString();

        if (canDefenderRetaliate)
        {
            if (defender.EquippedWeapon.IsMagical)
            {
                dmgDefText.text = Mathf.Max(0, defender.Attack - attacker.ModifiedResistance).ToString();
            }
            else
            {
                dmgDefText.text = Mathf.Max(0, defender.Attack - attackerUnit.UnitTotalGuard).ToString();
            }
            if (defender.ModifiedSpeed >= attacker.ModifiedSpeed + Define.SPEEDTHRESHOLD)
            {
                defenderDoubleText.gameObject.SetActive(true);
            }
            hitDefText.text = Mathf.Clamp(defender.DefensiveHit - attackerUnit.UnitTotalAvoid, 0, 100).ToString();
            amrDefText.text = defender.EquippedWeapon.rending.ToString();
            crtDefText.text = Mathf.Max(0, (defender.CriticalRate - attacker.CriticalAvoid) * 100 / attacker.CriticalAvoid).ToString();
            defenderWeaponIcon.sprite = defender.EquippedWeapon.GetItemSO().itemIcon;
            defenderWeaponIcon.gameObject.SetActive(true);
            defWeaponText.text = defender.EquippedWeapon.ItemName;
            defWeaponText.gameObject.SetActive(true);
        }
        else
        {
            dmgDefText.text = "-";
            hitDefText.text = "-";
            amrDefText.text = "-";
            crtDefText.text = "-";
            defenderWeaponIcon.gameObject.SetActive(false);
            defWeaponText.gameObject.SetActive(false);
        }
        
        

        gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    public void OnUpButtonClicked()
    {
        List<Weapon> attackerWeapons = new List<Weapon>();
        int index = -1;

        foreach(Item item in source.Character.CharacterInventory)
        {
            if(item is Weapon weapon)
            {
                if (source.Character.CanEquipWeapon(weapon))
                {
                    attackerWeapons.Add(weapon);
                    if (weapon == source.Character.EquippedWeapon)
                    {
                        index = attackerWeapons.IndexOf(weapon);
                    }
                }
            }
        }
        index = (index + 1) % attackerWeapons.Count;
        if (index > attackerWeapons.Count)
        {
            index -= attackerWeapons.Count;
        }

        source.Character.EquipWeapon(attackerWeapons[index]);
        OpenMenu(source, target);

    }

    public void OnDownButtonClicked()
    {
        List<Weapon> attackerWeapons = new List<Weapon>();
        int index = -1;

        foreach (Item item in source.Character.CharacterInventory)
        {
            if (item is Weapon weapon)
            {
                if (source.Character.CanEquipWeapon(weapon))
                {
                    attackerWeapons.Add(weapon);
                    if (weapon == source.Character.EquippedWeapon)
                    {
                        index = attackerWeapons.IndexOf(weapon);
                    }
                }
                
                
            }
        }
        index = index - 1;
        if(index < 0)
        {
            index += attackerWeapons.Count;
        }

        source.Character.EquipWeapon(attackerWeapons[index]);
        OpenMenu(source, target);

    }



    public void OnAttackButtonClicked()
    {

        GameManager gameManager = GameManager.Instance;
        CloseMenu();
        gameManager.mainGameMenuManager.CloseAllMenus();
        gameManager.StartCoroutine(OnAttack());
        

        
    }

    public IEnumerator OnAttack()
    {
        GameManager gameManager = GameManager.Instance;
        yield return gameManager.StartCoroutine(gameManager.BattleManager.ConductBattle(source, target));
        source.SetHasActed(true);
        gameManager.selectedPlayer = null;
        gameManager.SetState(new PlayerTurnState(gameManager));
        yield break;
    }
}
