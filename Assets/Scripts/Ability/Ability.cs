using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    protected PlayerStatsSO PlayerStats;

    public void Init(PlayerStatsSO playerStats)
    {
        PlayerStats = playerStats;
    }

    public abstract void TurnOn();
}