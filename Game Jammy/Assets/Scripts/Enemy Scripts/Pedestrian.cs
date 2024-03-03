using System.Collections;
using UnityEngine;

public class Pedestrian : Actor
{
    public Vector3 BoundingBoxPos { get; private set; }
    public Vector3 boundingBoxSize;
    public float minDistance;
    public AudioSource damageSound;
    private Vector3 _randomWalkTowards;
    private bool _isAtDestination;
    public float scaredDistance;

    public override void Start()
    {
        _isAtDestination = true;
        BoundingBoxPos = transform.position;
        base.Start();
        agent.speed = speed;
        agent.angularSpeed = rotateSpeed;
    }
    void Update()
    {
        switch (state)
        {
            case ActorState.Idle: 

                break;
            case ActorState.Walking:
                if (_isAtDestination)
                {
                    do
                    {
                        RandomPoint(BoundingBoxPos, boundingBoxSize, out _randomWalkTowards);
                    } while (Vector3.Distance(transform.position, _randomWalkTowards) < minDistance);
                    _isAtDestination = false;
                    agent.SetDestination(_randomWalkTowards);
                }
                if (Vector3.Distance(transform.position, _randomWalkTowards) < 1f)
                { _isAtDestination = true; }
                if (agent.velocity.magnitude < 0.5f) { StartCoroutine(CheckIfStuck(1)); }
                    break;
            case ActorState.Falling:
                break;
            case ActorState.Panic:
                float panicSpeed = speed * 2;
                agent.speed = panicSpeed;
                Vector3 normalDirection = (WorldManager.Global.PlayerPos - transform.position).normalized;
                normalDirection = Quaternion.AngleAxis(Random.Range(0, 15f), Vector3.up) * normalDirection;
                agent.SetDestination(transform.position - (normalDirection * scaredDistance));
                if ( Vector3.Distance(transform.position, WorldManager.Global.PlayerPos) > scaredDistance * 1.5f)
                {
                    _isAtDestination = true;
                    state = ActorState.Walking;
                    agent.speed = speed;
                }
                break;
            case ActorState.Attack: 
                break;
            case ActorState.Dead:
                if (isAlive)
                {
                    agent.SetDestination(transform.position);
                    transform.localScale = new Vector3(
                        transform.localScale.x * 3,
                        transform.localScale.y * 0.1f,
                        transform.localScale.z * 3);
                    isAlive = false;
                    agent.enabled = false;
                    transform.position += new Vector3(0, -0.4f, 0);
                    GameHUD hud = transform.parent.GetComponent<NavMeshEnemySpawner>().gameHUD;
                    hud.civiliansAlive--;
                    StartCoroutine(Death(4));
                }
                
                break;
        }
        if (Vector3.Distance(transform.position, WorldManager.Global.PlayerPos) < scaredDistance
            && state != ActorState.Panic && state != ActorState.Dead)
        {
            state = ActorState.Panic;
            _isAtDestination = true;
        }
    }
    private IEnumerator CheckIfStuck(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        if (agent.velocity.magnitude < 0.5f)
        { _isAtDestination = true; }
    }
    private IEnumerator Death(float length)
    {
        damageSound.clip = (AudioClip)Resources.Load("Scream");
        damageSound.pitch = Random.Range(0.5f, 1.5f);
        damageSound.Play();
        yield return new WaitForSeconds(length / 2);
        float timeRemaining = length / 2;
        do
        {
            transform.localScale *= 0.9f;
            yield return new WaitForSeconds(timeRemaining / 50);
            if (transform.localScale.x < 1)
            { Destroy(gameObject); }
        } while (true);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(agent.destination, 0.1f);
    }
}
