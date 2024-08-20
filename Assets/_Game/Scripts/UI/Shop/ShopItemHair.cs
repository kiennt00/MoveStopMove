using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemHair : ShopItem<HairType>
{
    public override void InitShopItem(HairType itemType)
    {
        base.InitShopItem(itemType);
        HairDataDetail hairDataDetail = SkinManager.Ins.GetHairData(itemType);
        if (hairDataDetail != null)
        {
            imageItem.sprite = hairDataDetail.imageSprite;
            price = hairDataDetail.price;
        }
    }

    public override void PreviewItem()
    {
        base.PreviewItem();
        LevelManager.Ins.player.EquipHair(itemType);
    }
}
