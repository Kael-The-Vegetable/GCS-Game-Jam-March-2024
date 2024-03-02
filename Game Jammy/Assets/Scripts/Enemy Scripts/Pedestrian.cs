using System.Collections;
using System.Collections.Generic;
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
            case ActorState.Panic: 

                break;
            case ActorState.Attack: 

                break;
            case ActorState.Dead:

                break;
        }
    }
}
