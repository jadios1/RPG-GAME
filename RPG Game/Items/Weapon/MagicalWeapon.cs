namespace RPG_Game.Items.Weapon;

public abstract class MagicalWeapon :Weapon
{
    public override int AcceptAttack(IAttackVisitor visitor, Player player)
    {
        return visitor.VisitMagical(this,player);
    }

    public override int AcceptDefense(IDefenseVisitor visitor, Player player)
    {
        return visitor.VisitMagicalDefense(this,player);

    }
}