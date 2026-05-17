using RPG_Game.Decorators;
using RPG_Game.Items;
using RPG_Game.Items.Weapon;
using RPG_Game.Species;

namespace RPG_Game.Themes;

public class BossTheme : IDungeonTheme
{
    public string IntroMessage { get; } = "You enter into a dungeon with a special item and a dangerous Boss";
    public List<Item> GetItemPool()
    {
        return new List<Item>() {};
    }

    public List<Item> GetWeaponPool()
    {
        return new List<Item>() {new StrongDecorator(new DoubleHandedWeapon()) };
    }

    public Item GetArtifact()
    {
        return new Skull();
    }

    public List<Enemy> GetEnemies()
    {
        return new List<Enemy>() { new Enemy("Boss", 500, 50, 5, new GoblinSpecies()) };
    }

    public void Generate(DungeonBuilder builder)
    {
        builder.GenerateFilled();
        builder.GenerateCentralRoom(10);
    }
}