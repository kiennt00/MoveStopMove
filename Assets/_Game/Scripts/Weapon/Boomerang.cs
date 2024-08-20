using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : Weapon
{
    protected override void SetPath(Vector3 startPoint, Vector3 moveDirection, float attackRange)
    {
        base.SetPath(startPoint, moveDirection, attackRange);
        listPaths.Add(startPoint);
    }
}
