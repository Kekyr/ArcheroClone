public class BackwardShurikenAddition : Ability
{
    public override void TurnOn()
    {
        PlayerStats.AddBackwardShuriken();
    }
}