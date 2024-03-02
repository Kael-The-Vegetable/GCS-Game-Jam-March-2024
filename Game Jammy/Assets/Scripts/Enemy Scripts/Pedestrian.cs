using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pedestrian : Actor
{
    public List<Vector3> walkingLoop;
    private int walkLoopIndex = 0;


    public override void Start()
    {
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
                if (Vector3.Distance(transform.position, walkingLoop[walkLoopIndex]) < 1)
                { walkLoopIndex++; }
                if (walkLoopIndex == walkingLoop.Count)
                { walkLoopIndex = 0; }
                Move(walkingLoop[walkLoopIndex]);
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
        Vector3 downwardVel = Vector3.up * gravity * Time.deltaTime;
        controller.Move((controller.velocity + downwardVel) * Time.deltaTime);
    }
}
