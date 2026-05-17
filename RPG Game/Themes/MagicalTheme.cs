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
        return new List<Enemy>() { new Enemy("Wizard", 150, 15, 10, new SkeletonSpecies()), new Enemy("Mage", 100, 20, 5, new GoblinSpecies()) };
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