namespace RPG_Game;

public class Map
{
    public Map(int height,int width,Player player)
    {
        Height = height;
        Width = width;
        _fields = new Field[width, height];
        DungeonBuilder builder = new DungeonBuilder(this);
        

        builder.GenerateEmpty();
        PlaceItemRandom();
        PlaceItemRandom();
        PlaceItemRandom();
        PlaceItemRandom();
        PlaceWeaponRandom();
        PlaceWeaponRandom();
        PlaceWeaponRandom();

        SetField(player.X,player.Y,new EmptyField());

    }




    
    
    private Field[,] _fields;
    public int Height { get; private set; }
    public int Width { get; private set; }


    public bool TryMovePlayer(Player player,int dx,int dy)
    {
        if (_fields[player.X + dx, player.Y + dy].IsPassable())
        {
            player.Move(dx,dy);
            return true;
        }

        return false;

    }

    

    public Field GetField(int x,int y)
    {
        return _fields[x, y];
    }

    public void SetField(int x, int y,Field field)
    {
        _fields[x, y] = field;
    }

    public void PlaceWall(int x,int y)
    {
        _fields[x, y] = new Wall();
    }

    private void PlaceItem(int x, int y, Item item)
    {
        _fields[x,y].PutItem(item);
    }
    
    
    public void PlaceItemRandom()
    {
        Random rnd = new Random();
        List<Item> items = new List<Item>{ new Coin(), new Junk(), new Clock(), new Book(), new Gold() };
        Item randomItem = items[rnd.Next(5)];
        int x = rnd.Next(Width);
        int y = rnd.Next(Height);
        while (!GetField(x, y).IsEmpty())
        {
            x = rnd.Next(Width);
            y = rnd.Next(Height);
        }
        _fields[x,y].PutItem(randomItem);
        
    }
    
    public void PlaceWeaponRandom()
    {
        Random rnd = new Random();
        List<Weapon> weapons = new List<Weapon>{ new DoubleHandedWeapon(), new Axe(), new SingleHandedWeapon() };
        Item randomItem = weapons[rnd.Next(3)];
        int x = rnd.Next(Width);
        int y = rnd.Next(Height);
        while (!GetField(x, y).IsEmpty())
        {
            x = rnd.Next(Width);
            y = rnd.Next(Height);
        }
        _fields[x,y].PutItem(randomItem);
        
    }
}