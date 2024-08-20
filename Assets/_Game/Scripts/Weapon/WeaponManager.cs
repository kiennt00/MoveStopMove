using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : Singleton<WeaponManager>
{
    private Dictionary<WeaponType, WeaponDataDetail> dictWeaponData = new();
    public List<WeaponType> listWeaponType = new();

    private void Awake()
    {
        InitWeaponData();
    }

    private void InitWeaponData()
    {
        List<WeaponDataDetail> weaponDataList = DataManager.Ins.weaponData.GetWeaponDataList();
        for (int i = 0; i < weaponDataList.Count; i++)
        {
            dictWeaponData.Add(weaponDataList[i].weaponType, weaponDataList[i]);
            listWeaponType.Add(weaponDataList[i].weaponType);
        }
    }

    public void InitWeapon(WeaponType weaponType, float levelScale, Character owner, float attackSpeed, Vector3 startPoint, Vector3 direction, float attackRange)
    {
        if(!dictWeaponData.TryGetValue(weaponType, out WeaponDataDetail weaponData))
        {
            return;
        }
        Weapon weapon = (Weapon)SimplePool.Spawn(weaponData.poolType, startPoint, Quaternion.identity);
        weapon.transform.localScale = new Vector3(levelScale, levelScale, levelScale);
        weapon.transform.rotation = Quaternion.LookRotation(direction);
        weapon.InitWeapon(owner, attackSpeed, startPoint, direction, attackRange);
    }

    public WeaponDataDetail GetWeaponData(WeaponType weaponType)
    {
        if (dictWeaponData.TryGetValue(weaponType, out WeaponDataDetail weaponData))
        {
            return weaponData;
        }
        else
        {
            return null;
        }
    }

    public WeaponType GetRandomWeapon()
    {
        return listWeaponType[Random.Range(0, listWeaponType.Count)];
    }
}