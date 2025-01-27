using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticConsumable : Item
{
    public int strength;
    public int magic;
    public int offence;
    public int defence;
    public int resistence;
    public int speed;
    public int move;

    public StatisticConsumable(StatisticConsumableSO template) : base(template)
    {
        strength = template.strength;
        magic = template.magic;
        offence = template.offence;
        defence = template.defence;
        resistence = template.resistence;
        speed = template.speed;
        move = template.move;
    }


    public StatisticConsumable() : base()
    {

    }
}
