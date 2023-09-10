public class MajorDamageBoost : Ability
{
    public override void TurnOn()
    {
        PlayerStats.MajorDamageBoost();
    }
}