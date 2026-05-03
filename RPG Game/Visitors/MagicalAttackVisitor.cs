using RPG_Game.Items;
using RPG_Game.Items.Weapon;

namespace RPG_Game.Visitors;

public class MagicalAttackVisitor : IAttackVisitor
{
    public int VisitHeavy(HeavyWeapon weapon, Player player)
    {
        return 1;
    }

    public int VisitLight(LightWeapon weapon, Player player)
    {
        return 1;
    }

    public int VisitMagical(MagicalWeapon weapon, Player player)
    {
        return (weapon.Damage+player.Wisdom);
    }

    public int VisitOther(Item item, Player player)
    {
        return 0;
    }
}