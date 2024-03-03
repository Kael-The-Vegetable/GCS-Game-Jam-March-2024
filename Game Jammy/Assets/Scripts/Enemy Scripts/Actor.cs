using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Actor : MonoBehaviour, IDamageable
{
    public int maxHealth;
    private int _currentHealth;
    public int speed;
    public float rotateSpeed;
    public NavMeshAgent agent;
    public enum ActorState
    {
        Idle,
        Walking,
        Falling,
        Panic,
        Attack,
        Dead
    }
    public ActorState state;

    public virtual void Start()
    {
        float gravity = WorldManager.Global.Gravity;
        _currentHealth = maxHealth;
    }

    //public void Move(Vector3 targetDestination)
    //{
    //    Vector3 distanceToTravel = targetDestination - transform.position;
    //    Vector3 direction = distanceToTravel.normalized;
    //    Quaternion rotation = Quaternion.LookRotation(direction);
    //    transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
    //    controller.Move(direction * speed * Time.deltaTime);
    //}
    public bool RandomPoint(Vector3 center, Vector3 size, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomLocation = new Vector3(Random.Range(-size.x, size.x), 0, Random.Range(-size.z, size.z)) + center;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomLocation, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        { state = ActorState.Dead; }
    }
}
