using RPG_Game.Items.Weapon;

namespace RPG_Game;

public class NormalDefenseVisitor : IDefenseVisitor
{
    public int VisitHeavyDefense(HeavyWeapon weapon, Player player)
    {
        return player.Strength + player.Luck;
    }

    public int VisitLightDefense(LightWeapon weapon, Player player)
    {
        return player.Dexterity + player.Luck;
    }

    public int VisitMagicalDefense(MagicalWeapon weapon, Player player)
    {
        return player.Dexterity + player.Luck;
    }

    public int VisitOtherDefense(Item item, Player player)
    {
        return player.Dexterity;
    }
}