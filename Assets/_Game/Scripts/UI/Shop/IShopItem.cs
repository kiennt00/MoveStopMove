using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopItem
{
    void SelectItem();
    void EquipItem();
    void UnequipItem();
    void UnlockItem();
    bool IsItemEquipped();
    bool IsItemUnlocked();
    int GetPrice();
}
