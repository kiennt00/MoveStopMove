using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class LevelManager : Singleton<LevelManager>
{
    public List<Level> listLevel = new();
    public Level currentLevel;
    public int currentLevelIndex;

    public Player player;

    private void Start()
    {
        UIManager.Ins.OpenUI<UIMainmenu>();
        currentLevelIndex = DataManager.Ins.GetCurrentLevel();
        LoadLevel(currentLevelIndex);
        InitPlayer();
    }

    public void LoadLevel(int levelIndex)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        currentLevelIndex = levelIndex;
        currentLevel = Instantiate(listLevel[levelIndex]);

        NavMesh.RemoveAllNavMeshData();
        NavMesh.AddNavMeshData(currentLevel.navmeshData);

        currentLevel.InitLevel();
    }

    public void InitPlayer()
    {
        player.InitCharacter();
        player.tf.position = currentLevel.GetRandomNodeStart().position;
        player.tf.rotation = Quaternion.Euler(0, 180, 0);
    }

    public void PlayAgain()
    {
        currentLevel.ResetLevel();
        currentLevel.InitLevel();
        player.ResetCharacter();
        InitPlayer();
    }

    public void NextLevel()
    {
        if (currentLevelIndex < listLevel.Count - 1)
        {
            currentLevelIndex++;
            DataManager.Ins.SaveCurrentLevel(currentLevelIndex);
            LoadLevel(currentLevelIndex);
            player.ResetCharacter();
            InitPlayer();
        }
        else
        {
            PlayAgain();
        }
    }

    public void Finish()
    {
        if (GameManager.Ins.IsState(GameState.Finish) || GameManager.Ins.IsState(GameState.Revive))
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
            player.CancelAttack();
            player.StopMove();
            player.ChangeAnim(Constants.ANIM_VICTORY);
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

