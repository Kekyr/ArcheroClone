public class AddMultiShot : Ability
{
    public override void TurnOn()
    {
        PlayerStats.AddMultiShot();
    }
}
