using RPG_Game.Items.Weapon;

namespace RPG_Game;

public class StealthAttackVisitor : IAttackVisitor
{
    public int VisitHeavy(HeavyWeapon weapon, Player player)
    {
        return (weapon.Damage+player.Strength+player.Aggression) / 2;   
    }

    public int VisitLight(LightWeapon weapon, Player player)
    {
        return (weapon.Damage+player.Dexterity+player.Luck) *2;
        
    }

    public int VisitMagical(MagicalWeapon weapon, Player player)
    {
        return 1;
    }

    public int VisitOther(Item item, Player player)
    {
        return 0;
    }
}