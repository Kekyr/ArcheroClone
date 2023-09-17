public class HealthRestoration : Ability
{
    public override void TurnOn()
    {
        PlayerStats.RestoreHealth();
    }
}