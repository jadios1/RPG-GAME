using RPG_Game.Items.Weapon;

namespace RPG_Game;

public class StealthDefenseVisitor : IDefenseVisitor
{
    public int VisitHeavyDefense(HeavyWeapon weapon, Player player)
    {
        return player.Strength;
    }

    public int VisitLightDefense(LightWeapon weapon, Player player)
    {
        return player.Dexterity;
    }

    public int VisitMagicalDefense(MagicalWeapon weapon, Player player)
    {
        return 0;
    }

    public int VisitOtherDefense(Item item, Player player)
    {
        return 0;   
    }
}