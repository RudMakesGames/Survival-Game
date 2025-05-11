using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState :BaseState
{
    public int waypointIndex;
    public float WaitTimer;
    public override void Enter()
    {
        
    }
    public override void Perform()
    {
        PatrolCycle();
    }
    public override void Exit()
    {

    }
    public void PatrolCycle()
    {
        if(enemy.Agent.remainingDistance < 0.2F )
        {
            WaitTimer += Time.deltaTime;
            if(WaitTimer > 3)
            {
                if (waypointIndex < enemy.path.Waypoints.Count - 1)
                {
                    waypointIndex++;
                }
                else
                {
                    waypointIndex = 0;
                }
                enemy.Agent.SetDestination(enemy.path.Waypoints[waypointIndex].position);
                WaitTimer = 0;
            }
           
        }
    }

}
