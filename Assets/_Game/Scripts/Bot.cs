using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    private IState<Bot> currentState;

    [SerializeField] NavMeshAgent agent;

    private Vector3 destination;
    public bool IsDestionation => Vector3.Distance(tf.position, destination + (tf.position.y - destination.y) * Vector3.up) < 0.1f;

    private string[] arrayFirstName = {
        "John", "Jane", "Michael", "Emily", "Chris",
        "Jessica", "Matthew", "Ashley", "Daniel", "Sarah",
        "David", "Amanda", "James", "Laura", "Robert",
        "Sophia", "William", "Olivia", "Alexander", "Mia"
    };
    private string[] arrayLastName = {
        "Smith", "Johnson", "Williams", "Brown", "Jones",
        "Garcia", "Miller", "Davis", "Rodriguez", "Martinez",
        "Hernandez", "Lopez", "Wilson", "Anderson", "Thomas",
        "Taylor", "Moore", "Jackson", "Martin", "Lee"
    };

    private string RandomName() => arrayFirstName[Random.Range(0, arrayFirstName.Length)] + " " + arrayLastName[Random.Range(0, arrayLastName.Length)];

    protected override void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }

        base.Update();
    }

    public void ChangeState(IState<Bot> newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public override void InitCharacter()
    {
        base.InitCharacter();
        characterInfo.UpdateTextName(RandomName());       
        ChangeState(new WaitState());
    }

    public override void LevelUp(int level)
    {
        base.LevelUp(level);
        agent.speed = MoveSpeed;
    }

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        agent.SetDestination(destination);
    }

    public override void StopMove()
    {
        base.StopMove();
        ChangeState(null);
        SetDestination(tf.position);
    }

    protected override IEnumerator IEDead()
    {
        yield return StartCoroutine(base.IEDead());

        ResetCharacter();
        SimplePool.Despawn(this);
    }
}
