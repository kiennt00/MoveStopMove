using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IdleState : IState<Bot>
{
    float timer;
    float randomTime;
    float maxTime;

    public void OnEnter(Bot bot)
    {
        bot.isMoving = false;
        bot.ChangeAnim(Constants.ANIM_IDLE);
        timer = 0;
        randomTime = Random.Range(1f, 2f);
        maxTime = randomTime * 2;
        bot.SetDestination(bot.tf.position);
    }

    public void OnExecute(Bot bot)
    {
        timer += Time.deltaTime;

        if (timer > randomTime)
        {
            if (bot.TargetedCharacter == null || timer > maxTime)
            {
                bot.ChangeState(new MoveState());
            }
        }
    }

    public void OnExit(Bot bot)
    {
        
    }
}
