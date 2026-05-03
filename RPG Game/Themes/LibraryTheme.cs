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
        builder.PlaceRoom(4,17,1,1);
        builder.PlaceRoom(4,17,7,1);
        builder.PlaceRoom(4,17,13,1);
        builder.PlaceRoom(4,17,19,1);
        builder.PlaceRoom(4,17,25,1);
        builder.PlaceRoom(4,17,31,1);

        
    }
}