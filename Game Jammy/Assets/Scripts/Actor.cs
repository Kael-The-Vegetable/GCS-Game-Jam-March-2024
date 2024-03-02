using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour, IDamageable
{
    public int maxHealth;
    private int _currentHealth;
    public int speed;

    private CharacterController _charController;

    void Start()
    {
        _charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        
    }

    public void Move(Vector3 targetDestination)
    {
        Vector3 distanceToTravel = targetDestination - transform.position;
        Vector3 direction = distanceToTravel.normalized;
        _charController.Move(direction * speed * Time.deltaTime);
    }
    public void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }
}
