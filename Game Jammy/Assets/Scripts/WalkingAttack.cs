using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingAttack : Attack
{
    public AudioSource stepSound;
    public override void OnAttack()
    {
        base.OnAttack();
    }

    public void OnStep()
    {
        stepSound.pitch = Random.Range(0.5f,1.2f);
        stepSound.Play();
        CameraShaker.Invoke(new Vector3(0.2f, 0.2f, 0.2f), new Vector3(0.2f, 0.2f, 0.2f));
    }
}
