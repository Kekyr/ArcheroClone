public class ForwardShurikenAddition : Ability
{
    public override void TurnOn()
    {
        PlayerStats.AddForwardShuriken();
    }
}