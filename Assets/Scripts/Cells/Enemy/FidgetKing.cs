using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FidgetKing : EnemyCell
{
    protected override void Rotate()
    {
        rigidBody.angularVelocity = 360f;
    }
}
