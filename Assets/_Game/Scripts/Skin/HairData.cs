using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HairData", menuName = "ScriptableObjects/HairData", order = 3)]
public class HairData : ScriptableObject
{
    [SerializeField] List<HairDataDetail> hairDataList;

    public List<HairDataDetail> GetHairDataList()
    {
        return new List<HairDataDetail>(hairDataList);
    }
}


[System.Serializable]
public class HairDataDetail
{
    public HairType hairType;
    public int price;
    public Sprite imageSprite;
    public GameObject prefab;
    public BuffType buffType;
}