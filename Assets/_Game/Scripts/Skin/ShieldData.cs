using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShieldData", menuName = "ScriptableObjects/ShieldData", order = 4)]
public class ShieldData : ScriptableObject
{
    [SerializeField] List<ShieldDataDetail> shieldDataList;

    public List<ShieldDataDetail> GetShieldDataList()
    {
        return new List<ShieldDataDetail>(shieldDataList);
    }
}


[System.Serializable]
public class ShieldDataDetail
{
    public ShieldType shieldType;
    public int price;
    public Sprite imageSprite;
    public GameObject prefab;
    public BuffType buffType;
}