using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{

    public BoxCollider _hitBox;
    public float _attackCooldown;
    private List<Collider> _hitList = new List<Collider>();
    public int _attackDamage = 10;
    public float _attackSeconds;

    private bool _enabled;
    private float _currentAttackCooldown;


    private void Start()
    {
        _enabled = false;
    }


    private void Update()
    {
        if (_currentAttackCooldown > 0)
        {
            _currentAttackCooldown -= Time.deltaTime;
            _currentAttackCooldown = Mathf.Clamp(_currentAttackCooldown, 0, _currentAttackCooldown);
        }
        if (_enabled)
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
        }
    }



    private IEnumerator ActivateHitBox()
    {
        _currentAttackCooldown = _attackCooldown;
        _enabled = true;
        yield return new WaitForSeconds(_attackSeconds);

        _enabled = false;
        _hitList = new List<Collider>();
    }

    public virtual void OnAttack()
    {
        if (_currentAttackCooldown == 0)
        {
            StartCoroutine(ActivateHitBox());
        }
    }
}