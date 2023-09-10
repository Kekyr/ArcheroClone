using UnityEngine;

public class PlayerTransition : MonoBehaviour
{
    [SerializeField] private PlayerState _targetState;

    protected Input InputManager;

    public PlayerState TargetState => _targetState;

    public bool NeedTransit { get; protected set; }

    public void Init(Input inputManager)
    {
        InputManager = inputManager;
    }

    private void OnEnable()
    {
        NeedTransit = false;
    }
}
