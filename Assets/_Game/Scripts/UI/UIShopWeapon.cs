using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShopWeapon : UICanvas
{
    [SerializeField] Button btnQuit, btnBuy, btnEquip, btnPrevious, btnNext;
    [SerializeField] GameObject Equipped;
    [SerializeField] TextMeshProUGUI textGold, textName, textPrice;
    [SerializeField] Transform weaponParent;
    [SerializeField] List<WeaponType> listWeaponType = new();

    GameObject currentWeapon;
    string weaponName;
    int price;
    int currentWeaponIndex;

    private void Awake()
    {
        btnQuit.onClick.AddListener(() =>
        {
            CloseDirectly();
            UIManager.Ins.OpenUI<UIMainmenu>();

            LevelManager.Ins.player.gameObject.SetActive(true);
        });

        btnBuy.onClick.AddListener(() =>
        {
            UnlockItem();
        });

        btnEquip.onClick.AddListener(() =>
        {
            EquipItem();
        });

        btnPrevious.onClick.AddListener(() =>
        {
            PreviousItem();
        });

        btnNext.onClick.AddListener(() =>
        {
            NextItem();
        });

        this.RegisterListener(EventID.OnGoldChanged, (param) =>
        {
            UpdateTextGold(DataManager.Ins.GetCurrentGold());
        });

        UpdateTextGold(DataManager.Ins.GetCurrentGold());
        InitShopWeapon();
    }

    private void InitShopWeapon()
    {
        listWeaponType = WeaponManager.Ins.listWeaponType;
        for (int i = 0; i < listWeaponType.Count; i++)
        {
            if (DataManager.Ins.GetCurrentItem<WeaponType>() == listWeaponType[i])
            {
                currentWeaponIndex = i;
                InitItem(currentWeaponIndex);
                break;
            }
        }
    }

    private void InitItem(int index)
    {
        WeaponDataDetail weaponDataDetail = WeaponManager.Ins.GetWeaponData(listWeaponType[index]);
        if (weaponDataDetail != null)
        {
            Destroy(currentWeapon);
            currentWeapon = Instantiate(weaponDataDetail.weaponShopPrefab, weaponParent);
            weaponName = weaponDataDetail.name;
            price = weaponDataDetail.price;
            textName.text = weaponName;
            textPrice.text = price.ToString();
        }
        RefreshButton();
    }

    private void NextItem()
    {
        int tempIndex = currentWeaponIndex + 1;
        if (tempIndex < listWeaponType.Count)
        {
            currentWeaponIndex = tempIndex;
            InitItem(currentWeaponIndex);
        }
    }

    private void PreviousItem()
    {
        int tempIndex = currentWeaponIndex - 1;
        if (tempIndex >= 0)
        {
            currentWeaponIndex = tempIndex;
            InitItem(currentWeaponIndex);
        }
    }

    public void UnlockItem()
    {
        if (DataManager.Ins.GetCurrentGold() >= price)
        {
            DataManager.Ins.AdjustGold(-price);
            DataManager.Ins.UnlockItem<WeaponType>(listWeaponType[currentWeaponIndex]);

            RefreshButton();
        }
    }

    public void EquipItem()
    {
        DataManager.Ins.SaveCurrentItem<WeaponType>(listWeaponType[currentWeaponIndex]);
        LevelManager.Ins.player.RefreshItem<WeaponType>();

        RefreshButton();
    }

    private void RefreshButton()
    {
        btnBuy.gameObject.SetActive(false);
        btnEquip.gameObject.SetActive(false);
        Equipped.SetActive(false);

        if (DataManager.Ins.GetCurrentItem<WeaponType>() == listWeaponType[currentWeaponIndex])
        {
            Equipped.SetActive(true);
            return;
        }
        if (DataManager.Ins.IsItemUnlocked<WeaponType>(listWeaponType[currentWeaponIndex]))
        {
            btnEquip.gameObject.SetActive(true);
            return;
        }
        btnBuy.gameObject.SetActive(true);
    }

    private void UpdateTextGold(int gold)
    {
        textGold.text = gold.ToString();
    }

    public override void Open()
    {
        base.Open();
        GameManager.Ins.ChangeGameState(GameState.ShopWeapon);

        LevelManager.Ins.player.gameObject.SetActive(false);
    }
}
