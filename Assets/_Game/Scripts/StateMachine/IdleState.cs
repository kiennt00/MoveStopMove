using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IdleState : IState<Bot>
{
    float timer;
    float randomTime;

    public void OnEnter(Bot bot)
    {
        bot.isMoving = false;
        bot.ChangeAnim(Constants.ANIM_IDLE);
        timer = 0;
        randomTime = Random.Range(1f, 2f);
        bot.SetDestination(bot.tf.position);
    }

    public void OnExecute(Bot bot)
    {
        timer += Time.deltaTime;

        if (timer > randomTime)
        {
            if (bot.TargetedCharacter == null || timer > 5f)
            {
                bot.ChangeState(new MoveState());
            }
        }
    }

    public void OnExit(Bot bot)
    {
        
    }
}
