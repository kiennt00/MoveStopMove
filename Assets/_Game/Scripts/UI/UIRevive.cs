using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRevive : UICanvas
{
    [SerializeField] Button btnRevive, btnQuit;
    [SerializeField] TextMeshProUGUI textCountdown;

    [SerializeField] Transform ImageCountdown;

    int countdown;

    private void Awake()
    {
        btnRevive.onClick.AddListener(() =>
        {
            CloseDirectly();
            LevelManager.Ins.RevivePlayer();           
        });

        btnQuit.onClick.AddListener(() =>
        {
            CloseDirectly();
            UIManager.Ins.OpenUI<UIDefeat>();
        });
    }

    private void Update()
    {
        ImageCountdown.Rotate(0f, 0f, -360f * Time.deltaTime);
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
