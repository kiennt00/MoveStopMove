using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem<ItemType> : MonoBehaviour, IShopItem where ItemType : Enum
{
    [SerializeField] protected ItemType itemType;
    [SerializeField] protected GameObject imageSelected, Lock, Equipped;
    [SerializeField] protected Button btnShopItem;
    [SerializeField] protected Image imageItem;

    protected bool isItemEquipped;
    protected bool isItemUnlocked;
    protected int price;
    public bool IsItemEquipped() => isItemEquipped;
    public bool IsItemUnlocked() => isItemUnlocked;
    public int GetPrice() => price;

    protected void Awake()
    {
        btnShopItem.onClick.AddListener(() =>
        {
            SelectItem();
        });

        this.RegisterListener(EventID.OnShopItemSelected, (param) =>
        {
            ShopItem<ItemType> selectedItem = param as ShopItem<ItemType>;
            if (selectedItem != this)
            {
                IsShopItemSelected(false);
            }
        });

        this.RegisterListener(EventID.OnItemEquipped, (param) =>
        {
            ShopItem<ItemType> equippedItem = param as ShopItem<ItemType>;
            if (equippedItem != this && isItemEquipped)
            {
                IsIteamEquipped(false);
            }
        });
    }

    public virtual void InitShopItem(ItemType itemType)
    {
        this.itemType = itemType;

        //------bring this to override---------------------
        //DataDetail<T> dataDetail = SkinManager.Ins.GetData<T>(t);
        //if (dataDetail != null)
        //{
        //    imageItem.sprite = dataDetail.imageSprite;
        //    price = dataDetail.price;
        //}
        //--------------------------------------------------

        isItemUnlocked = DataManager.Ins.IsItemUnlocked<ItemType>(itemType);
        Lock.SetActive(!isItemUnlocked);

        bool state = DataManager.Ins.GetCurrentItem<ItemType>().Equals(itemType);
        IsIteamEquipped(state);
    }

    public void IsShopItemSelected(bool state)
    {
        imageSelected.SetActive(state);
    }

    public void SelectItem()
    {
        IsShopItemSelected(true);
        PreviewItem();

        this.PostEvent(EventID.OnShopItemSelected, this);
    }

    public void EquipItem()
    {
        DataManager.Ins.SaveCurrentItem<ItemType>(itemType);
        IsIteamEquipped(true);
        LevelManager.Ins.player.RefreshItem<ItemType>();

        this.PostEvent(EventID.OnItemEquipped, this);
    }

    public void UnequipItem()
    {
        DataManager.Ins.SaveCurrentItem<ItemType>((ItemType)(object)0);
        IsIteamEquipped(false);
        LevelManager.Ins.player.RefreshItem<ItemType>();
    }

    protected void IsIteamEquipped(bool state)
    {
        isItemEquipped = state;
        Equipped.SetActive(isItemEquipped);
    }

    public void UnlockItem()
    {
        if (DataManager.Ins.GetCurrentGold() >= price)
        {
            DataManager.Ins.AdjustGold(-price);
            DataManager.Ins.UnlockItem<ItemType>(itemType);

            isItemUnlocked = true;
            Lock.SetActive(false);
        }
    }

    public virtual void PreviewItem()
    {

    }
}
