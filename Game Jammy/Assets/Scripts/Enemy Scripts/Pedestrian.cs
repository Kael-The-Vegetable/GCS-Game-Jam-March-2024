using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pedestrian : Actor
{
    public List<Vector3> walkingLoop;
    private int walkLoopIndex = 0;
    private Vector3 velocity; 


    public override void Start()
    {
        velocity = Vector3.zero;
        base.Start();
    }
    void Update()
    {
        if (!charController.isGrounded)
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
                if (charController.isGrounded)
                { 
                    velocity.y = 0;
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
        if (!charController.isGrounded)
        {
            float gravity = WorldManager.Global.Gravity;
            velocity.y += gravity * Time.deltaTime;
            charController.Move(velocity * Time.deltaTime);
        }
    }
}
