public class RestoreHealth : Ability
{
    public override void TurnOn()
    {
        PlayerStats.RestoreHealth();
    }
}