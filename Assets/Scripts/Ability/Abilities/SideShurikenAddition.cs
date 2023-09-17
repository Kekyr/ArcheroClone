public class SideShurikenAddition : Ability
{
    public override void TurnOn()
    {
        PlayerStats.AddSideShuriken();
    }
}