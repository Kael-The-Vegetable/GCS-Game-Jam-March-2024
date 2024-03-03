using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Character;
using UnityEngine.TextCore.Text;

public class Punch : Attack
{
    public Character character;
    public override void OnAttack()
    {
        if (_currentAttackCooldown == 0)
        {
            character.attacking = true;
        }
    }


    public void DealDamagePunch()
    {
        base.OnAttack();
    }

    public void FinishedAnimationPunch()
    {
        character.attacking = false;
        character.animator.SetBool("Punch", false);
        character.currentState = CharacterStates.Idle;
    }
}
