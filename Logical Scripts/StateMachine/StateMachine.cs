using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected State state;

    public void SetState(State state)
    {
        //CONSIDER - adding onExit for states this.state.OnExitState();
        this.state = state;
        StartCoroutine(this.state.Start());
    }

}
