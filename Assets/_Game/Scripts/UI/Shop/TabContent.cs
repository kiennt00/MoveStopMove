using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabContent<ItemType> : MonoBehaviour, ITabContent where ItemType : Enum
{
    [SerializeField] protected ItemType itemType;
    [SerializeField] protected TabButton tabButton;
    [SerializeField] protected Transform contentParent;
    [SerializeField] protected List<ItemType> listItemType = new();
    [SerializeField] protected ShopItem<ItemType> shopItemPrefab;
    [SerializeField] protected List<ShopItem<ItemType>> listShopItem = new();

    protected void Awake()
    {
        InitTabContent();

        this.RegisterListener(EventID.OnTabSelected, (param) =>
        {
            bool state = (TabButton)param == tabButton;
            tabButton.IsTabSelected(state);
            IsTabSelected(state);
        });
    }

    protected void InitTabContent()
    {
        GetListItemType(itemType);
        for (int i = 1; i < listItemType.Count; i++)
        {
            ShopItem<ItemType> shopItem = Instantiate(shopItemPrefab, contentParent);
            shopItem.InitShopItem(listItemType[i]);
            listShopItem.Add(shopItem);
        }
    }

    protected virtual void GetListItemType(ItemType itemType)
    {

    }

    public void IsTabSelected(bool state)
    {
        gameObject.SetActive(state);
        if (state)
        {
            bool haveCurrentItem = false;
            for (int i = 1; i < listItemType.Count; i++)
            {
                if (DataManager.Ins.GetCurrentItem<ItemType>().Equals(listItemType[i]))
                {
                    listShopItem[i - 1].SelectItem();
                    haveCurrentItem = true;
                    break;
                }  
            }
            
            if (!haveCurrentItem) 
            { 
                listShopItem[0].SelectItem();
            }
        }
    }
}
