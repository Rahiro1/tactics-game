using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is for passing enum values in unity events, as a function that accepts enums can't be called via a UnityEvent
/// </summary>
public class EventEnums : MonoBehaviour
{
    public Define.WeaponType weaponType;
    public Define.ArmourType armourType;
    public Define.MenuType menuType;
}
