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
    public bool RandomPoint(Vector3 center, Vector3 size, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * size.x;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
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
        Debug.Log("TOOK DAMAGE");
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        { state = ActorState.Dead; }
    }
}
