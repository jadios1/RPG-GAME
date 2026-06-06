using RPG_Game.Items;
using RPG_Game.Items.Weapon;

namespace RPG_Game.Themes;


public interface IDungeonTheme
{
    string IntroMessage { get; }
    List<Item> GetItemPool();
    List<Item> GetWeaponPool();

    Item GetArtifact();
    List<Enemy> GetEnemies();
    void Generate(DungeonBuilder builder);
}