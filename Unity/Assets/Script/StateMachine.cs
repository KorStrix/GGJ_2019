using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{

    public IState currentlyRunningState;
    private IState previousState;
    private Stack<IState> stateStack;

    public void Awake()
    {
        this.currentlyRunningState = new EmptyState();
        this.previousState = new EmptyState();
    }
    
    public void ExecuteStateUpdate()
    {
        var runningState = this.currentlyRunningState;
        if (runningState != null)
        {
            this.currentlyRunningState.Execute();
        }
    }

    public void SwitchToPreviousState()
    {
        this.currentlyRunningState.Exit();
        this.currentlyRunningState = this.previousState;
        this.currentlyRunningState.Enter();
    }

    public void PushState(IState newState)
    {
        if (this.currentlyRunningState == null)
        {
            return;
        }

        this.currentlyRunningState.Exit();
        this.previousState = this.currentlyRunningState;

        this.currentlyRunningState = newState;
        this.currentlyRunningState.Enter();
        stateStack.Push(newState);
    }

    public void PopState()
    {
        if (stateStack.Count == 0)
        {
            Debug.Log("The Stack is already Empty");
            return;
        }

        stateStack.Pop();

        if (stateStack.Count != 0)
            this.previousState = stateStack.Peek();
        else
            this.previousState = new EmptyState();
        
        SwitchToPreviousState();
    }
}
