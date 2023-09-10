using UnityEngine;

public class BatMoveTransition : Transition
{
    [SerializeField] private float _delay;

    private float _timeAfterLastMove;

    private void Update()
    {
        _timeAfterLastMove += Time.deltaTime;

        if (_timeAfterLastMove >= _delay)
        {
            NeedTransit = true;
            _timeAfterLastMove = 0;
        }
    }
}