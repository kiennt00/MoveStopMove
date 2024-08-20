using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelManager : Singleton<LevelManager>
{
    public List<Level> levels = new();
    public Level currentLevel;
    public int currentLevelIndex;

    public Player player;

    public void OnLoadLevel(int levelIndex)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        if (levelIndex < levels.Count)
        {
            currentLevelIndex = levelIndex;
            currentLevel = Instantiate(levels[levelIndex]);
            currentLevel.InitLevel();
        }

        InitPlayer();
    }

    public void InitPlayer()
    {
        player.InitCharacter(0);
        player.tf.position = currentLevel.GetRandomNodeStart().position;
        player.tf.rotation = Quaternion.Euler(0, 180, 0);
    }

    public void PlayAgain()
    {
        player.ResetCharacter();
        currentLevel.ResetLevel();
        InitPlayer();
    }

    public void Finish()
    {
        if (GameManager.Ins.IsState(GameState.Finish))
        {
            return;
        }

        UIManager.Ins.CloseUI<UIGameplay>();

        if (player.isDead)
        {
            if (currentLevel.reviveCount > 0)
            {
                currentLevel.reviveCount--;
                UIManager.Ins.OpenUI<UIRevive>();
            }
            else
            {
                UIManager.Ins.OpenUI<UIDefeat>();
            }
        }
        else
        {
            UIManager.Ins.OpenUI<UIVictory>();
        }      
    }

    public void Victory()
    {
        UIManager.Ins.CloseUI<UIGameplay>();
        UIManager.Ins.OpenUI<UIVictory>();
    }

    public void RevivePlayer()
    {
        player.Revive();
        currentLevel.alive++;
        player.tf.position = currentLevel.GetRandomNodeStart().position;
        if (currentLevel.alive == 1)
        {
            UIManager.Ins.OpenUI<UIVictory>();
        }
        else
        {
            UIManager.Ins.OpenUI<UIGameplay>();
        }    
    }
}

