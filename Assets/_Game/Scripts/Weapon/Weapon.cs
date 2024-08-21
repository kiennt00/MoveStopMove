using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Weapon : GameUnit
{
    [SerializeField] protected int characterLayer;
    [SerializeField] protected int propLayer;
    [SerializeField] protected Character owner;
    [SerializeField] protected List<Vector3> listPaths = new();
    [SerializeField] protected float rotateSpeed;

    protected float attackSpeed;
    protected float Duration => Mathf.Max(1 - attackSpeed / 200, 0.25f);

    protected Sequence sequence;

    protected void Update()
    {
        tf.Rotate(rotateSpeed * Time.deltaTime * Vector3.up);
    }

    public void InitWeapon(Character owner, float attackSpeed, Vector3 startPoint, Vector3 direction, float attackRange)
    {
        this.owner = owner;
        this.attackSpeed = attackSpeed;
        SetPath(startPoint, direction, attackRange);
        Move();
    }

    protected virtual void SetPath(Vector3 startPoint, Vector3 direction, float attackRange)
    {
        Vector3 targetPoint = startPoint + (direction.normalized * attackRange);

        listPaths.Add(targetPoint);
    }

    protected void Move()
    {
        sequence = DOTween.Sequence();

        for (int i = 0; i < listPaths.Count; i++)
        {
            sequence.Append(transform.DOMove(listPaths[i], Duration).SetEase(Ease.Linear));
        }

        sequence.OnComplete(() =>
        {
            OnDespawn();
        });

        sequence.Play();
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == characterLayer)
        {
            Character character = Cache.Ins.GetCachedComponent<Character>(other);

            if (character != owner && !character.isDead)
            {
                character.OnDead();
                owner.LevelUp(1);
                OnDespawn();

                if (character is Player player)
                {
                    player.killedBy = owner.characterInfo.GetTextName();
                    player.killedByMaterialColor = owner.characterInfo.GetMaterialColor();
                }
            }
        }

        if (other.gameObject.layer == propLayer)
        {
            OnDespawn();
        }
    }

    protected void OnDespawn()
    {
        listPaths.Clear();
        sequence.Kill();
        SimplePool.Despawn(this);
    }
}
