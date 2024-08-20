using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGameplay : UICanvas
{
    [SerializeField] TextMeshProUGUI textAlive;

    public void UpdateTextAlive(int alive)
    {
        textAlive.text = alive.ToString();
    }

    public override void Open()
    {
        base.Open();
        GameManager.Ins.ChangeGameState(GameState.Gameplay);
        UpdateTextAlive(LevelManager.Ins.currentLevel.alive);
    }
}
