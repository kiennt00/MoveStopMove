using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIVictory : UICanvas
{
    [SerializeField] Button btnContinue;
    [SerializeField] TextMeshProUGUI textGold;

    int gold;

    private void Awake()
    {
        btnContinue.onClick.AddListener(() =>
        {
            CloseDirectly();
            DataManager.Ins.AdjustGold(gold);
            UIManager.Ins.OpenUI<UIMainmenu>();
            LevelManager.Ins.PlayAgain();
        });
    }

    private void UpdateTextGold(int gold)
    {
        textGold.text = gold.ToString();
    }

    public override void Open()
    {
        base.Open();
        GameManager.Ins.ChangeGameState(GameState.Finish);
        AudioManager.Ins.PlaySFX(SFXType.Finish);
        LevelManager.Ins.player.StopMove();
        LevelManager.Ins.player.ChangeAnim(Constants.ANIM_VICTORY);
        gold = LevelManager.Ins.player.Level * 5;
        UpdateTextGold(gold);
    }
}
