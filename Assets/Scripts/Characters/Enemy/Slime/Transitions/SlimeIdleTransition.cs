using UnityEngine;

public class SlimeIdleTransition : Transition
{
    [SerializeField] private float _delay;

    private float _timeAfterLastIdle;

    private void Update()
    {
        _timeAfterLastIdle += Time.deltaTime;

        if (_timeAfterLastIdle >= _delay)
        {
            NeedTransit = true;
            _timeAfterLastIdle = 0;
        }
    }
}
