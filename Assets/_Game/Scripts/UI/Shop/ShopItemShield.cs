using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemShield : ShopItem<ShieldType>
{
    public override void InitShopItem(ShieldType itemType)
    {
        base.InitShopItem(itemType);
        ShieldDataDetail shieldDataDetail = SkinManager.Ins.GetShieldData(itemType);
        if (shieldDataDetail != null)
        {
            imageItem.sprite = shieldDataDetail.imageSprite;
            price = shieldDataDetail.price;
        }
    }

    public override void PreviewItem()
    {
        base.PreviewItem();
        LevelManager.Ins.player.EquipShield(itemType);
    }
}
