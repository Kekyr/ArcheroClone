using UnityEngine;

public class MaxHealthBoost : Ability
{
    public override void TurnOn()
    {
        PlayerStats.MaxHealthBoost();
    }
}
