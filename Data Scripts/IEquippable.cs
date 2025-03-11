using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquippable
{
    public void Equip(Character chatacter);

    public void Unequip(Character character);

    public void CanEquip(Character character);
}
 