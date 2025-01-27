using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Shop")]
public class ShopSO : ScriptableObject
{
    public List<Define.ShopData> shop;
}
