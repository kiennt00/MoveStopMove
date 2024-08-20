using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShopSkin : UICanvas
{
    [SerializeField] Button btnQuit, btnBuy, btnEquip, btnUnequip;
    [SerializeField] List<TabButton> listTabButton;
    [SerializeField] TextMeshProUGUI textGold, textPrice;

    IShopItem currentShopItem;
    private void Awake()
    {
        btnQuit.onClick.AddListener(() =>
        {
            CloseDirectly();
            LevelManager.Ins.player.RefreshItem<HairType>();
            LevelManager.Ins.player.RefreshItem<PantsType>();
            LevelManager.Ins.player.RefreshItem<ShieldType>();
            UIManager.Ins.OpenUI<UIMainmenu>();
        });

        btnBuy.onClick.AddListener(() =>
        {
            currentShopItem.UnlockItem();
            RefreshButton();
        });

        btnEquip.onClick.AddListener(() =>
        {
            currentShopItem.EquipItem();
            RefreshButton();
        });

        btnUnequip.onClick.AddListener(() =>
        {
            currentShopItem.UnequipItem();
            RefreshButton();
        });

        this.RegisterListener(EventID.OnShopItemSelected, (param) =>
        {
            currentShopItem = (IShopItem)param;
            UpdateTextPrice(currentShopItem.GetPrice());
            RefreshButton();
        });

        this.RegisterListener(EventID.OnGoldChanged, (param) =>
        {
            UpdateTextGold(DataManager.Ins.GetCurrentGold());
        });

        UpdateTextGold(DataManager.Ins.GetCurrentGold());
    }

    private void UpdateTextGold(int gold)
    {
        textGold.text = gold.ToString();
    }

    private void UpdateTextPrice(int price)
    {
        textPrice.text = price.ToString();
    }

    private void RefreshButton()
    {
        btnBuy.gameObject.SetActive(false);
        btnEquip.gameObject.SetActive(false);
        btnUnequip.gameObject.SetActive(false);

        if (currentShopItem.IsItemEquipped())
        {
            btnUnequip.gameObject.SetActive(true);
            return;
        }
        if (currentShopItem.IsItemUnlocked())
        {
            btnEquip.gameObject.SetActive(true);
            return;
        }
        btnBuy.gameObject.SetActive(true);
    }

    public override void Open()
    {
        base.Open();
        GameManager.Ins.ChangeGameState(GameState.ShopSkin);
        
        listTabButton[0].SelectTab();
    }
}
