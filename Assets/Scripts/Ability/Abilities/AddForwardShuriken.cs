public class AddForwardShuriken : Ability
{
    public override void TurnOn()
    {
        PlayerStats.AddForwardShuriken();
    }
}