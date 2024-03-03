using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{

    public BoxCollider _hitBox;
    public float attackCooldown;
    public bool canAttack;
    public int _attackDamage = 10;


    private float _currentAttackCooldown;
    private List<Collider> _hitList = new List<Collider>();


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
        Collider[] hits = Physics.OverlapBox(_hitBox.transform.position, _hitBox.size / 2, _hitBox.transform.rotation);

        for (int i = 0; i < hits.Length; i++)
        {
            if (_hitList.Contains(hits[i]) == false)
            {
                IDamageable damageable = hits[i].GetComponent<IDamageable>();

                if (damageable != null)
                {
                    _hitList.Add(hits[i]);
                    damageable.TakeDamage(_attackDamage);
                }
            }
        }
        _currentAttackCooldown = attackCooldown;
    }


    public virtual void OnAttack()
    {
        if (_currentAttackCooldown == 0)
        {
            
        }
    }
}