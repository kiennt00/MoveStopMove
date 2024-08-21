using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Level : MonoBehaviour
{
    [SerializeField] List<Transform> listNodeStart = new();
    [SerializeField] List<Transform> listNodeMove = new();

    [SerializeField] List<Bot> listBot = new();

    [SerializeField] int botAtSameTime;
    [SerializeField] int botTotal;
    public int alive;
    public int reviveCount;
    public NavMeshData navmeshData;

    private void Awake()
    {
        this.RegisterListener(EventID.OnCharacterDead, (param) =>
        {
            alive--;
            UIManager.Ins.GetUI<UIGameplay>().UpdateTextAlive(alive);
            
            Character character = (Character)param;
            if (character is Bot bot)
            {
                if (alive - botAtSameTime + (LevelManager.Ins.player.isDead ? 1 : 0) > 0)
                {
                    StartCoroutine(IEGenerateBot(Constants.WFS_2_S_5));
                }

                listBot.Remove(bot);

                if (alive == 1)
                {
                    LevelManager.Ins.Finish();
                }
            }
            else if (character is Player player)
            {
                player.rank = alive + 1;
            }     
        });
    }

    public void InitLevel()
    {
        reviveCount = 1;

        alive = botTotal + 1;

        for (int i = 0; i < botAtSameTime; i++)
        {
            StartCoroutine(IEGenerateBot(Constants.WFS_0_S));
        }
    }

    public Transform GetRandomNodeMove()
    {
        int index = Random.Range(0, listNodeMove.Count);
        return listNodeMove[index];
    }

    public Transform GetRandomNodeStart()
    {
        int index = Random.Range(0, listNodeStart.Count);
        Transform node = listNodeStart[index];
        listNodeStart.RemoveAt(index);
        StartCoroutine(IECountdownNodeReady(listNodeStart, node));
        return node;
    }

    IEnumerator IECountdownNodeReady(List<Transform> listNode, Transform node)
    {
        yield return Constants.WFS_0_S_5;
        listNode.Add(node);
    }

    IEnumerator IEGenerateBot(WaitForSeconds delay)
    {
        yield return delay;

        Transform nodeStart = GetRandomNodeStart();
        Bot bot = (Bot)SimplePool.Spawn(PoolType.Bot, nodeStart.position, Quaternion.identity);
        int playerLevel = LevelManager.Ins.player.Level;
        int botLevel = playerLevel + Random.Range(0, Mathf.FloorToInt(playerLevel * 0.2f) + 1);
        bot.InitCharacter();
        bot.LevelUp(botLevel);

        WeaponType weaponType = WeaponManager.Ins.GetRandomWeapon();
        HairType hairType = SkinManager.Ins.GetRandomHair();
        ShieldType shieldType = SkinManager.Ins.GetRandomShield();
        PantsType pantsType = SkinManager.Ins.GetRandomPants();

        bot.EquipWeapon(weaponType);
        bot.EquipHair(hairType);
        bot.EquipShield(shieldType);
        bot.EquipPants(pantsType);

        bot.ChangeColor(SkinManager.Ins.GetRandomColor());

        listBot.Add(bot);
    }

    public void ResetLevel()
    {
        StopAllCoroutines();
        for (int i = 0; i < listBot.Count; i++)
        {
            listBot[i].StopMove();
            listBot[i].ResetCharacter();
            listBot[i].RemoveAllTarget();
            SimplePool.Despawn(listBot[i]);
        }
        listBot.Clear();
    }
}
