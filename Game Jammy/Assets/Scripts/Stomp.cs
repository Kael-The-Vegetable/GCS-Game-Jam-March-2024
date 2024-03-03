using System.Collections;
using UnityEngine;
using static Character;

public class Stomp : Attack
{
    public Character character;
    public AudioSource stompSound;
    public override void OnAttack()
    {
        if (_currentAttackCooldown == 0)
        {
            character.attacking = true;
        }
    }


    public void DealDamage()
    {
        stompSound.pitch = Random.Range(0.5f, 1.2f);
        stompSound.Play();
        CameraShaker.Invoke(new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 1f));
        base.OnAttack();
    }

    public void FinishedAnimation()
    {
        character.attacking = false;
        character.animator.SetBool("Stomp", false);
        character.currentState = CharacterStates.Idle;
    }
}
