using RPG_Game.Items.Weapon;

namespace RPG_Game;

public interface IDefenseVisitor
{
    int VisitHeavyDefense(HeavyWeapon weapon, Player player);
    int VisitLightDefense(LightWeapon weapon, Player player);
    int VisitMagicalDefense(MagicalWeapon weapon, Player player);
    int VisitOtherDefense(Item item, Player player);

}