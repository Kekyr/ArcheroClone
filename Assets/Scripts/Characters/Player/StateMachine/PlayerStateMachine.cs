using UnityEngine;

[RequireComponent(typeof(Input))]
public class PlayerStateMachine : MonoBehaviour
{
    [SerializeField] private PlayerState _firstState;

    private Input _inputManager;
    private PlayerState _currentState;

    public PlayerState Current => _currentState;

    private void Start()
    {
        _inputManager = GetComponent<Input>();
        Reset(_firstState);
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        var nextState = _currentState.GetNextState();

        if (nextState != null)
            Transit(nextState);
    }

    private void Reset(PlayerState startState)
    {
        _currentState = startState;

        if (_currentState != null)
            _currentState.Enter(_inputManager);
    }

    private void Transit(PlayerState nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter(_inputManager);
    }

    public void Stop()
    {
        if (_currentState != null)
            _currentState.Exit();

        enabled = false;
    }

    public void Launch()
    {
        if (_currentState != null)
            _currentState.Enter(_inputManager);
    }
}