using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRevive : UICanvas
{
    [SerializeField] Button btnRevive, btnQuit;
    [SerializeField] TextMeshProUGUI textCountdown, textGold, textPrice;
    [SerializeField] Transform ImageCountdown;
    [SerializeField] int price = 50;
    int countdown;

    private void Awake()
    {
        btnRevive.onClick.AddListener(() =>
        {
            if (DataManager.Ins.GetCurrentGold() >= price)
            {
                CloseDirectly();
                DataManager.Ins.AdjustGold(-price);
                LevelManager.Ins.RevivePlayer();
            }                      
        });

        btnQuit.onClick.AddListener(() =>
        {
            CloseDirectly();
            UIManager.Ins.OpenUI<UIDefeat>();
        });

        this.RegisterListener(EventID.OnGoldChanged, (param) =>
        {
            UpdateTextGold(DataManager.Ins.GetCurrentGold());
        });

        UpdateTextGold(DataManager.Ins.GetCurrentGold());
        textPrice.text = price.ToString();
    }

    private void Update()
    {
        ImageCountdown.Rotate(0f, 0f, -360f * Time.deltaTime);
    }

    private void UpdateTextGold(int gold)
    {
        textGold.text = gold.ToString();
    }

    public override void Open()
    {
        base.Open();
        GameManager.Ins.ChangeGameState(GameState.Revive);
        countdown = 5;
        Countdown();
    }

    IEnumerator IECountdown()
    {
        yield return Constants.WFS_1_S;
        Countdown();
    }

    void Countdown()
    {
        if (countdown < 0)
        {
            CloseDirectly();
            UIManager.Ins.OpenUI<UIDefeat>();
        }
        else
        {
            AudioManager.Ins.PlaySFX(SFXType.Countdown);
            textCountdown.text = countdown.ToString();
            countdown--;
            StartCoroutine(IECountdown());
        }
    }
}
