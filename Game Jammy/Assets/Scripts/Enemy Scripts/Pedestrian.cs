using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor.AI;
using UnityEngine.AI;

public class Pedestrian : Actor
{
    public Vector3 BoundingBoxPos { get; private set; }
    public float boundingBoxRadius;
    public float minDistance;

    public NavMeshAgent agent;

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
        if (!controller.isGrounded)
        {
            state = ActorState.Falling;
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
                        RandomPoint(BoundingBoxPos, boundingBoxRadius, out _randomWalkTowards);
                    } while (Vector3.Distance(transform.position, _randomWalkTowards) < minDistance);
                    _isAtDestination = false;
                }
                if (Vector3.Distance(transform.position, _randomWalkTowards) < 1f)
                { _isAtDestination = true; }
                agent.SetDestination(_randomWalkTowards);
                break;
            case ActorState.Falling:
                Gravity();
                if (controller.isGrounded)
                { 
                    state = ActorState.Walking; 
                }
                break;
            case ActorState.Panic: 

                break;
            case ActorState.Attack: 

                break;
            case ActorState.Dead:
                transform.localScale = new Vector3(transform.localScale.x, 0.1f, transform.localScale.z);
                break;
        }
    }
    private void Gravity()
    {
        float gravity = WorldManager.Global.Gravity;
        Vector3 downwardVel = new Vector3(0, gravity * Time.deltaTime);
        controller.Move((controller.velocity + downwardVel) * Time.deltaTime);
    }
    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
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
    private bool RandomPoint(Vector3 center, Vector3 size, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            
        }
        result = Vector3.zero;
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(BoundingBoxPos, boundingBoxRadius);
        //Gizmos.DrawWireSphere(transform.position, minDistance);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(_randomWalkTowards, 0.1f);
    }
}
