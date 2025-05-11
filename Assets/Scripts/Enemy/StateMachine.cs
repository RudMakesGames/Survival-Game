using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;
    public PatrolState patrolState;
    void Start()
    {
        
    }
    public void Initialise()
    {
        patrolState = new PatrolState();
        ChangeState(patrolState);
    }

    // Update is called once per frame
    void Update()
    {
        if (activeState != null)
        {
            activeState.Perform();
        }
    }
    public void ChangeState(BaseState Newstate)
    {
        if(activeState!= null)
        {
            activeState.Exit();
        }
        activeState = Newstate;

        if(activeState != null )
        {
            activeState.stateMachine = this;
            activeState.enemy = GetComponent<Enemy>();
            activeState.Enter();
        }
    }
}
