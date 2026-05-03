using RPG_Game.Visitors;

namespace RPG_Game.Items.Weapon;

public abstract class HeavyWeapon : Weapon
{
    public override int AcceptAttack(IAttackVisitor visitor, Player player)
        => visitor.VisitHeavy(this, player);

    public override int AcceptDefense(IDefenseVisitor visitor, Player player)
        => visitor.VisitHeavyDefense(this, player);
}