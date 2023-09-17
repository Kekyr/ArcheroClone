public class DiagonalShurikenAddition : Ability
{
    public override void TurnOn()
    {
        PlayerStats.AddDiagonalShuriken();
    }
}