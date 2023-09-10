public class RabbitMoveTransition : Transition
{
    private RabbitAttackState _attack;

    private void Start()
    {
        _attack = GetComponent<RabbitAttackState>();
    }

    private void Update()
    {
        if (_attack.Completed)
            NeedTransit = true;
    }
}