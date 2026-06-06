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
        var goblinspecies = new GoblinSpecies();
        var sceletonspecies = new SkeletonSpecies();
        List<Enemy> enemies = new List<Enemy>
        {
            new Enemy("BOSS", 150, 20, 20,goblinspecies),
            new Enemy("Little Sceleton", 30, 20, 0,sceletonspecies),
            new Enemy("Little Goblin", 30, 5, 10,goblinspecies),
            new Enemy("Little Sceleton", 20, 3, 10 ,sceletonspecies)
        };

        return enemies;
    }

    public void Generate(DungeonBuilder builder)
    {
        builder.GenerateFilled();
        builder.GenerateCentralRoom(10);
    }
}