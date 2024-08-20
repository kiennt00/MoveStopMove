using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] List<Transform> listNodeStart = new();
    [SerializeField] List<Transform> listNodeMove = new();

    [SerializeField] List<Bot> listBot = new();

    private HashSet<Transform> setNodeStartReady = new();

    [SerializeField] int botAtSameTime = 6;
    [SerializeField] int botTotal = 49;
    public int alive;
    public int reviveCount;

    private void Awake()
    {
        this.RegisterListener(EventID.OnCharacterDead, (param) =>
        {
            Character character = (Character)param;
            if (character is Bot bot)
            {
                if (alive - botAtSameTime > 1)
                {
                    StartCoroutine(IEGenerateBot(Constants.WFS_2_S_5));
                }

                listBot.Remove(bot);
            }
            else if (character is Player player)
            {
                player.rank = alive;
            }

            alive--;
            UIManager.Ins.GetUI<UIGameplay>().UpdateTextAlive(alive);            

            if (alive == 1)
            {
                LevelManager.Ins.Finish();
            }
        });

        this.RegisterListener(EventID.OnNodeStartReady, (param) =>
        {
            setNodeStartReady.Add((Transform)param);
        });

        this.RegisterListener(EventID.OnNodeStartBusy, (param) =>
        {
            setNodeStartReady.Remove((Transform)param);
        });

        InitSetNodeStart();
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

    private void InitSetNodeStart()
    {
        for (int i = 0; i < listNodeStart.Count; i++)
        {
            setNodeStartReady.Add(listNodeStart[i]);
        }
    }

    public Transform GetRandomNodeMove()
    {
        int index = Random.Range(0, listNodeMove.Count);
        return listNodeMove[index];
    }

    public Transform GetRandomNodeStart()
    {
        int index = Random.Range(0, setNodeStartReady.Count);
        Transform node = setNodeStartReady.ElementAt(index);
        setNodeStartReady.Remove(node);
        return node;
    }

    IEnumerator IEGenerateBot(WaitForSeconds delay)
    {
        yield return delay;

        Transform NodeStart = GetRandomNodeStart();
        Bot bot = (Bot)SimplePool.Spawn(PoolType.Bot, NodeStart.position, Quaternion.identity);
        int playerLevel = LevelManager.Ins.player.Level;
        int botLevel = playerLevel + Random.Range(0, 2);
        bot.InitCharacter(botLevel);

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

        InitLevel();
    }
}
