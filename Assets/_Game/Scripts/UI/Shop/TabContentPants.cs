using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabContentPants : TabContent<PantsType>
{
    protected override void GetListItemType(PantsType itemType)
    {
        base.GetListItemType(itemType);
        listItemType = SkinManager.Ins.listPantsType;
    }
}
