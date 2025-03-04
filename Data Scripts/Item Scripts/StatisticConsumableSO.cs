using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Items/Other/Consumable/Statistic")]
public class StatisticConsumableSO : ItemSO
{
    public int hP;
    public int strength;
    public int magic;
    public int offence;
    public int defence;
    public int resistence;
    public int speed;
    public int move;

    public override Item CreateItem()
    {
        return new StatisticConsumable(this);
    }

    public override void OnUse(UnitController unit)
    {
        unit.Character.HP.ChangeBaseStatValue(hP);
        unit.Character.Strength.ChangeBaseStatValue(strength);
        unit.Character.Magic.ChangeBaseStatValue(magic);
        unit.Character.Offence.ChangeBaseStatValue(offence);
        unit.Character.Defence.ChangeBaseStatValue(defence);
        unit.Character.Resistance.ChangeBaseStatValue(resistence);
        unit.Character.Speed.ChangeBaseStatValue(speed);
        unit.Character.Move.ChangeBaseStatValue(move);
    }
}
