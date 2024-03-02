using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour, IDamageable
{
    public int maxHealth;
    private int _currentHealth;
    public int speed;
    public float rotateSpeed;
    public enum ActorState
    {
        Idle,
        Walking,
        Panic,
        Attack,
        Dead
    }
    public ActorState state;

    internal CharacterController charController;

    public virtual void Start()
    {
        float gravity = WorldManager.Global.Gravity;
        charController = GetComponent<CharacterController>();
        _currentHealth = maxHealth;
    }

    public void Move(Vector3 targetDestination)
    {
        Vector3 distanceToTravel = targetDestination - transform.position;
        Vector3 direction = distanceToTravel.normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
        charController.Move(direction * speed * Time.deltaTime);
    }
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        { state = ActorState.Dead; }
    }
}
