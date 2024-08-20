using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainmenu : UICanvas
{
    [SerializeField] Button btnPlay, btnWeapon, btnSkin;
    [SerializeField] TMP_InputField inputName;
    [SerializeField] TextMeshProUGUI textGold;
    private void Awake()
    {
        btnPlay.onClick.AddListener(() =>
        {
            CloseDirectly();
            UIManager.Ins.OpenUI<UIGameplay>();

            DataManager.Ins.SavePlayerName(inputName.text);
            LevelManager.Ins.player.characterInfo.UpdateTextName(inputName.text);
        });

        btnWeapon.onClick.AddListener(() =>
        {
            CloseDirectly();
            UIManager.Ins.OpenUI<UIShopWeapon>();
        });

        btnSkin.onClick.AddListener(() =>
        {
            CloseDirectly();
            UIManager.Ins.OpenUI<UIShopSkin>();
        });

        this.RegisterListener(EventID.OnGoldChanged, (param) =>
        {
            UpdateTextGold(DataManager.Ins.GetCurrentGold());
        });

        UpdateTextGold(DataManager.Ins.GetCurrentGold());
        UpdateInputName(DataManager.Ins.GetPlayerName());
    }

    private void UpdateInputName(string name)
    {
        inputName.text = name;
    }

    private void UpdateTextGold(int gold)
    {
        textGold.text = gold.ToString();
    }

    public override void Open()
    {
        base.Open();
        GameManager.Ins.ChangeGameState(GameState.Mainmenu);      
    }
}
