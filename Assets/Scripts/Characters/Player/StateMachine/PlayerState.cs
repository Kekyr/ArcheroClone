using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : MonoBehaviour
{
    [SerializeField] private List<PlayerTransition> _transitions;

    protected Input InputManager { get; set; }

    public void Enter(Input inputManager)
    {
        if (enabled == false)
        {
            InputManager = inputManager;
            enabled = true;

            foreach (var transition in _transitions)
            {
                transition.enabled = true;
                transition.Init(InputManager);
            }
        }
    }

    public void Exit()
    {
        if (enabled == true)
        {
            foreach (var transition in _transitions)
                transition.enabled = false;

            enabled = false;
        }
    }

    public PlayerState GetNextState()
    {
        foreach (var transition in _transitions)
        {
            if (transition.NeedTransit)
                return transition.TargetState;
        }

        return null;
    }
}
