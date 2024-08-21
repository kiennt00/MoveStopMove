using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IState<Bot>
{
    float timer;
    float randomTime;
    float maxTime;

    public void OnEnter(Bot bot)
    {
        bot.isMoving = true;
        bot.ChangeAnim(Constants.ANIM_RUN);
        timer = 0;
        randomTime = Random.Range(1f, 3f);
        maxTime = randomTime * 2;
        bot.SetDestination(LevelManager.Ins.currentLevel.GetRandomNodeMove().position);
    }

    public void OnExecute(Bot bot)
    {
        timer += Time.deltaTime;

        if (timer > randomTime)
        {
            if (bot.TargetedCharacter != null || timer > maxTime)
            {               
                bot.ChangeState(new IdleState());
            }
        }

        if (bot.IsDestionation)
        {
            bot.ChangeState(new IdleState());
        }
    }

    public void OnExit(Bot bot)
    {

    }
}
