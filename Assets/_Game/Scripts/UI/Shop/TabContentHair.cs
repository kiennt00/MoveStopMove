using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabContentHair : TabContent<HairType>
{
    protected override void GetListItemType(HairType itemType)
    {
        base.GetListItemType(itemType);
        listItemType = SkinManager.Ins.listHairType;
    }
}
