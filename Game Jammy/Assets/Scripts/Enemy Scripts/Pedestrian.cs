using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor.AI;

public class Pedestrian : Actor
{
    public Vector3 BoundingBoxPos { get; private set; }
    public Vector3 boundingBoxSize;
    public float minDistance;

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
        if (
            Vector3.Distance(transform.position, WorldManager.Global.PlayerPos) < scaredDistance 
            && state != ActorState.Panic)
        { 
            state = ActorState.Panic;
            _isAtDestination = true;
        }
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
                agent.SetDestination(transform.position);
                transform.localScale = new Vector3(18, 1, 18);
                break;
        }
    }
    private IEnumerator CheckIfStuck(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        if (agent.velocity.magnitude < 0.5f)
        { _isAtDestination = true; }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(BoundingBoxPos, boundingBoxSize * 2);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(_randomWalkTowards, 0.1f);
    }
}
