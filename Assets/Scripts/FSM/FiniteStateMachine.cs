using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine : MonoBehaviour
{
    protected State currentState;

    protected delegate void UpdateState(float dt);
    protected UpdateState updateState;

    protected virtual void SwitchState(State newState)
    {
        if (currentState != null && currentState.GetType() == newState.GetType())
        {
            return;
        }

        currentState?.ExitState();
        currentState = newState;
        currentState.InitState();
        updateState = currentState.UpdateState;
    }

    protected virtual void SwitchState<T>(State newState, T param)
    {
        if (currentState != null && currentState.GetType() == newState.GetType())
        {
            return;
        }

        currentState?.ExitState();
        currentState = newState;
        currentState.InitState(param);
        updateState = currentState.UpdateState;
    }

    protected virtual void ResetInitValues<T>(T param)
    {
        if (currentState == null)
        {
            return;
        }

        currentState.InitState(param);
        updateState = currentState.UpdateState;
    }

    protected virtual void Update()
    {
        if(currentState == null)
        {
            return;
        }

        updateState(Time.deltaTime);
    }


    protected bool IsStateRunning(Type state)
    {
        return (currentState.GetType() == state);
    }
}
