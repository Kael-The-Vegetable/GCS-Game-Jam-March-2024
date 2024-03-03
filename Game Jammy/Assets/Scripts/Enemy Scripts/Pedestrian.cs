using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor.AI;
using UnityEngine.AI;

public class Pedestrian : Actor
{
    public Vector3 BoundingBoxPos { get; private set; }
    public Vector3 boundingBoxSize;
    public float minDistance;

    private Vector3 _randomWalkTowards;
    private bool _isAtDestination;

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
                    StartCoroutine(CheckIfStuck(1));
                }
                if (Vector3.Distance(transform.position, _randomWalkTowards) < 1f)
                { _isAtDestination = true; }
                
                break;
            case ActorState.Falling:
                break;
            case ActorState.Panic: 

                break;
            case ActorState.Attack: 

                break;
            case ActorState.Dead:
                agent.SetDestination(transform.position);
                transform.localScale = new Vector3(transform.localScale.x, 0.1f, transform.localScale.z);
                break;
        }
    }
    private IEnumerator CheckIfStuck(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        if (agent.velocity.magnitude < 1)
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
