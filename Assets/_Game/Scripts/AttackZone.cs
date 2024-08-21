using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    [SerializeField] int characterLayer;
    [SerializeField] Character owner;

    private void OnTriggerEnter(Collider other)
    {
        if (!owner.isDead && other.gameObject.layer == characterLayer)
        {
            Character character = Cache.Ins.GetCachedComponent<Character>(other);
            if (!character.isDead)
            {
                owner.AddTarget(character);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!owner.isDead && other.gameObject.layer == characterLayer)
        {
            owner.RemoveTarget(Cache.Ins.GetCachedComponent<Character>(other));
        }
    }
}
