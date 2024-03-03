using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{

    public BoxCollider _hitBox;
    public float attackCooldown;
    public bool canAttack;
    public int _attackDamage = 10;


    protected float _currentAttackCooldown;


    private void Start()
    {

    }


    private void Update()
    {
        if (_currentAttackCooldown > 0)
        {
            _currentAttackCooldown -= Time.deltaTime;
            _currentAttackCooldown = Mathf.Clamp(_currentAttackCooldown, 0, attackCooldown);
            canAttack = false;
        }
        else
        {
            canAttack = true;
        }
    }

    public void AttackFrame()
    {
        
    }


    public virtual void OnAttack()
    {
        if (_currentAttackCooldown == 0)
        {
            Collider[] hits = Physics.OverlapBox(_hitBox.transform.position, _hitBox.size, _hitBox.transform.rotation);

            for (int i = 0; i < hits.Length; i++)
            {
                IDamageable damageable = hits[i].GetComponent<IDamageable>();

                if (damageable != null && hits[i].gameObject != gameObject)
                {
                    damageable.TakeDamage(_attackDamage);
                }
            }

           

            _currentAttackCooldown = attackCooldown;
        }
    }
}