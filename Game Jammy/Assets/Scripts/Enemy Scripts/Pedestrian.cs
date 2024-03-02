using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pedestrian : Actor
{
    public Vector3 boundingBoxPos;
    public Vector3 boundingBoxSize;
    public float minDistance;


    private Vector3 _randomWalkTowards;
    private bool _isAtDestination;

    public override void Start()
    {
        _isAtDestination = true;
        base.Start();
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
                        _randomWalkTowards = new Vector3(
                            Random.Range(-boundingBoxSize.x, boundingBoxSize.x) / 2, 0,
                            Random.Range(-boundingBoxSize.z, boundingBoxSize.z) / 2) + boundingBoxPos;
                    } while (Vector3.Distance(transform.position, _randomWalkTowards) < minDistance);
                    _isAtDestination = false;
                }
                if (Vector3.Distance(transform.position, _randomWalkTowards) < 0.5f)
                { _isAtDestination = true; }
                Move(_randomWalkTowards);
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

                break;
        }
    }
    private void Gravity()
    {
        float gravity = WorldManager.Global.Gravity;
        Vector3 downwardVel = new Vector3(0, gravity * Time.deltaTime);
        controller.Move((controller.velocity + downwardVel) * Time.deltaTime);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boundingBoxPos, boundingBoxSize);
        //Gizmos.DrawWireSphere(transform.position, minDistance);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(_randomWalkTowards, 0.1f);
    }
}
