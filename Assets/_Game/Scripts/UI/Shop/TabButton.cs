using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabButton : MonoBehaviour
{
    [SerializeField] Button btnTab;
    [SerializeField] Image imageIcon;
    [SerializeField] Image imageBG;

    private void Awake()
    {
        btnTab.onClick.AddListener(() =>
        {
            SelectTab();
        });
    }

    public void SelectTab()
    {
        this.PostEvent(EventID.OnTabSelected, this);
    }

    public void IsTabSelected(bool state)
    {
        Color imageIconColor = imageIcon.color;
        Color imageBGColor = imageBG.color;

        if (state)
        {
            imageIconColor.a = 255f / 255f;
            imageBGColor.a = 0f / 255f;
        }
        else
        {
            imageIconColor.a = 100f / 255f;
            imageBGColor.a = 150f / 255f;
        }

        imageIcon.color = imageIconColor;
        imageBG.color = imageBGColor;
    }
}
