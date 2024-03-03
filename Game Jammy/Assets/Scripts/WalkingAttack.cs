using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingAttack : Attack
{
    public override void OnAttack()
    {
        base.OnAttack();
    }

    public void OnStep()
    {
        CameraShaker.Invoke(new Vector3(0.2f, 0.2f, 0.2f), new Vector3(0.2f, 0.2f, 0.2f));
    }
}
