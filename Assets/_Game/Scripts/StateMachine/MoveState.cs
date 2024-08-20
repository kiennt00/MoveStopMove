using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IState<Bot>
{
    float timer;
    float randomTime;

    public void OnEnter(Bot bot)
    {
        bot.isMoving = true;
        bot.ChangeAnim(Constants.ANIM_RUN);
        timer = 0;
        randomTime = Random.Range(1f, 4f);
        bot.SetDestination(LevelManager.Ins.currentLevel.GetRandomNodeMove().position);
    }

    public void OnExecute(Bot bot)
    {
        timer += Time.deltaTime;

        if (timer > randomTime)
        {
            if (bot.TargetedCharacter != null)
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
