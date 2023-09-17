public class MultiShotAddition : Ability
{
    public override void TurnOn()
    {
        PlayerStats.AddMultiShot();
    }
}
