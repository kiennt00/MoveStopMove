using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDefeat : UICanvas
{
    [SerializeField] Button btnContinue;
    [SerializeField] TextMeshProUGUI textRank, textKilledBy, textGold;

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

    private void UpdateTextRank(int rank)
    {
        textRank.text = "#" + rank.ToString();
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
        gold = LevelManager.Ins.player.Level;
        UpdateTextGold(gold);
        UpdateTextRank(LevelManager.Ins.player.rank);
        textKilledBy.text = LevelManager.Ins.player.killedBy;
        textKilledBy.color = LevelManager.Ins.player.killedByMaterialColor.color;
    }
}
