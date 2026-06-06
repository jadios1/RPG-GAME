using RPG_Game.Items;
using RPG_Game.Items.Weapon;

namespace RPG_Game.Visitors;

public class NormalAttackVisitor : IAttackVisitor
{
    public int VisitHeavy(HeavyWeapon weapon, Player player)
    {
        return weapon.Damage + player.Strength + player.Aggression;
    }

    public int VisitLight(LightWeapon weapon, Player player)
    {
        return weapon.Damage + player.Dexterity + player.Luck;
    }

    public int VisitMagical(MagicalWeapon weapon, Player player)
    {
        return 1;
    }

    public int VisitOther(Item item, Player player)
    {
        return 0;    }
}