using RPG_Game.Items.Weapon;

namespace RPG_Game;

public interface IAttackVisitor
{
    int VisitHeavy(HeavyWeapon weapon, Player player);
    int VisitLight(LightWeapon weapon, Player player);
    int VisitMagical(MagicalWeapon weapon, Player player);
    int VisitOther(Item item, Player player);
}