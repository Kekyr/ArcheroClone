using UnityEngine;

public class RabbitAttackTransition : Transition
{
    [SerializeField] private float _minDelay;
    [SerializeField] private float _maxDelay;

    private float _randomDelay;
    private float _timeAfterLastAttack;

    private void Start()
    {
        _randomDelay = Random.Range(_minDelay, _maxDelay);
    }

    private void Update()
    {
        _timeAfterLastAttack += Time.deltaTime;

        if (_timeAfterLastAttack >= _randomDelay)
        {
            NeedTransit = true;
            _timeAfterLastAttack = 0;
        }
    }
}