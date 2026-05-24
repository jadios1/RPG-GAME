using RPG_Game.Items;
using RPG_Game.Items.Weapon;
using RPG_Game.Species;
namespace RPG_Game.Themes;

public class MagicalTheme : IDungeonTheme
{
    public string IntroMessage { get; } =
        "The air crackles with magical energy, find the special Eye on the map and fight a Wizard and a mage!";
    public List<Item> GetItemPool()
    {
        return new List<Item>() { new Eye() };
    }

    public List<Item> GetWeaponPool()
    {
        return new List<Item>() {new Magic_Stick(),new Magic_Double_Handed_Stick()};
    }

    public Item GetArtifact()
    {
        return new Eye();
    }

    public List<Enemy> GetEnemies()
    {
        var goblinspecies = new GoblinSpecies();
        var sceletonspecies = new SkeletonSpecies();
        List<Enemy> enemies = new List<Enemy>
        {
            new Enemy("Goblin Mage", 50, 20, 0,goblinspecies),
            new Enemy("Sceleton Mage", 50, 20, 0,sceletonspecies),
            new Enemy("Sceleton Wizard", 100, 5, 10,goblinspecies),
            new Enemy("Sceleton Wizard", 20, 3, 10 ,sceletonspecies)
        };

        return enemies;
    }

    public void Generate(DungeonBuilder builder)
    {
        builder.GenerateFilled();
        builder.GenerateCentralRoom(2);
        builder.GenerateChamber(5);
        builder.GenerateChamber(5);
        builder.GenerateChamber(5);
        builder.GenerateChamber(5);
            
    }
}