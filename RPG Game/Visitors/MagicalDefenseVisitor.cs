using RPG_Game.Items;
using RPG_Game.Items.Weapon;

namespace RPG_Game.Visitors;

public class MagicalDefenseVisitor : IDefenseVisitor
{
    public int VisitHeavyDefense(HeavyWeapon weapon, Player player)
    {
        return player.Luck;
    }

    public int VisitLightDefense(LightWeapon weapon, Player player)
    {
        return player.Luck;
        
    }

    public int VisitMagicalDefense(MagicalWeapon weapon, Player player)
    {
        return player.Wisdom * 2;
    }

    public int VisitOtherDefense(Item item, Player player)
    {
        return player.Luck;
    }
}