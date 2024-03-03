using System.Collections;
using UnityEngine;
using static Character;

public class Stomp : Attack
{
    public Character character;
    public override void OnAttack()
    {
        character.attacking = true;
    }


    public void DealDamage()
    {
        CameraShaker.Invoke();
        base.OnAttack();
    }

    public void FinishedAnimation()
    {
        character.attacking = false;
        character.animator.SetBool("Stomp", false);
        character.currentState = CharacterStates.Idle;
    }
}
