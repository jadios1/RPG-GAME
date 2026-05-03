using RPG_Game.Decorators;
using RPG_Game.Items;
using RPG_Game.Items.Weapon;

namespace RPG_Game.Themes;

public class LibraryTheme : IDungeonTheme
{
    public string IntroMessage { get; } = "The smell of old books fills the air\": the dungeon consists of many rooms, the item pool includes books, pens and Coins, the dungeon contains the \"Wisdom Book\" and library staff";
    
    public List<Item> GetItemPool()
    {
        List<Item> items = new List<Item>
        {
            new Pen(),
            new Clock(),
            new Coin()
        };
        return items;

    }

    public List<Item> GetWeaponPool()
    {
        List<Item> weapons = new List<Item>
        {
            new DoubleHandedWeapon(),
            new SingleHandedWeapon(),
            new Pen()
        };
        return weapons;
    }

    public Item GetArtifact()
    {
        return new WisdomDecorator(new Book());
    }

    public List<Enemy> GetEnemies()
    {
        List<Enemy> enemies = new List<Enemy>
        {
            new Enemy("Book Keeper", 50, 20, 0),
            new Enemy("Librarian", 100, 5, 10),
            new Enemy("Nerd", 20, 3, 10)
        };

        return enemies;
    }

    public void Generate(DungeonBuilder builder)
    {
        builder.GenerateFilled();
        builder.GenerateCentralRoom(6);
        builder.GenerateChamber(4);
        builder.GenerateChamber(4);
        builder.GenerateChamber(3);
    }
}