using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabContentShield : TabContent<ShieldType>
{
    protected override void GetListItemType(ShieldType itemType)
    {
        base.GetListItemType(itemType);
        listItemType = SkinManager.Ins.listShieldType;
    }
}
