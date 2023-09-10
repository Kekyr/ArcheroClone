using UnityEngine;

public class BossGhostIdleTransition : Transition
{
    private BossGhostAttackState _attack;

    private void Start()
    {
        _attack = GetComponent<BossGhostAttackState>();
    }

    private void Update()
    {
        if (_attack.Completed)
            NeedTransit = true;
    }
}
