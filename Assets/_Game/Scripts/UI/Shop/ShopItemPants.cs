using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemPants : ShopItem<PantsType>
{
    public override void InitShopItem(PantsType itemType)
    {
        base.InitShopItem(itemType);
        PantsDataDetail pantsDataDetail = SkinManager.Ins.GetPantsData(itemType);
        if (pantsDataDetail != null)
        {
            imageItem.sprite = pantsDataDetail.imageSprite;
            price = pantsDataDetail.price;
        }
    }

    public override void PreviewItem()
    {
        base.PreviewItem();
        LevelManager.Ins.player.EquipPants(itemType);
    }
}
