using System.Collections;
using UnityEngine;
using static Character;

public class Stomp : Attack
{
    public Character character;
    public override void OnAttack()
    {
        if (_currentAttackCooldown == 0)
        {
            character.attacking = true;
        }
    }


    public void DealDamage()
    {
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
