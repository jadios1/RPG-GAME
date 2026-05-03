using RPG_Game.Fields;
using RPG_Game.Items;
using RPG_Game.Items.Weapon;

namespace RPG_Game;

public class DungeonBuilder
{
    private Map _map;
    private List<(int x, int y)> roomCenters = new();
    private Random _rnd = new Random();
    
    
    public DungeonBuilder(Map map)
    {
        _map = map;
    }
    
    public void GenerateEmpty()
    {
        for (int y = 0; y < _map.Height; y++)
        {
            for (int x = 0; x < _map.Width; x++)
            {
                _map.SetField(x,y,new EmptyField());
                if (y == 0 || x == 0 || y == _map.Height-1 || x == _map.Width-1)
                {
                    PlaceWall(x,y);
                }

            }
        }

    }
    
    public void GetPlayerSpawn(int x,int y)
    {
        roomCenters.Add((x, y));

    }
    
    public void GenerateCentralRoom(int size)
    {
        int roomHeight = size;
        int roomWidht = roomHeight*2;
        for (int y = _map.Height/2 - (roomHeight/2) ; y < _map.Height/2 + (roomHeight/2); y++)
        {
            for (int x = _map.Width/2 - (roomWidht/2); x < _map.Width/2 + (roomWidht/2); x++)
            {
                _map.SetField(x,y,new EmptyField());
            }
        }
        int centerX = _map.Width/2 + size / 2;
        int centerY = _map.Height/2 + size / 2;

        roomCenters.Add((centerX, centerY));


    }
    
    public DungeonBuilder GenerateFilled()
    {
        for (int y = 0; y < _map.Height; y++)
        {
            for (int x = 0; x < _map.Width; x++)
            {
                PlaceWall(x,y);
            }
        }

        return this;
    }

    public void GenerateChamber(int size)
    {
        int sizeX = size * 2;
        int sizeY = size;

        int startX = _rnd.Next(1, _map.Width - sizeX - 1);
        int startY = _rnd.Next(1, _map.Height - sizeY - 1);

        for (int y = startY; y < startY + sizeY; y++)
        {
            for (int x = startX; x < startX + sizeX; x++)
            {
                _map.SetField(x, y, new EmptyField());
            }
        }

        int centerX = startX + sizeX / 2;
        int centerY = startY + sizeY / 2;

        roomCenters.Add((centerX, centerY));
    }
    
    public void ConnectRooms()
    {
        for (int i = 0; i < roomCenters.Count - 1; i++)
        {
            var (x1, y1) = roomCenters[i];
            var (x2, y2) = roomCenters[i + 1];

            if (_rnd.Next(2) == 0)
            {
                CarveHorizontal(x1, x2, y1);
                CarveVertical(y1, y2, x2);
            }
            else
            {
                CarveVertical(y1, y2, x1);
                CarveHorizontal(x1, x2, y2);
            }
        }
    }
    
    private void CarveHorizontal(int x1, int x2, int y)
    {
        int from = Math.Min(x1, x2);
        int to = Math.Max(x1, x2);

        for (int x = from; x <= to; x++)
        {
            if (x > 0 && x < _map.Width - 1)
                _map.SetField(x, y, new EmptyField());
        }
    }

    private void CarveVertical(int y1, int y2, int x)
    {
        int from = Math.Min(y1, y2);
        int to = Math.Max(y1, y2);

        for (int y = from; y <= to; y++)
        {
            if (y > 0 && y < _map.Height - 1)
                _map.SetField(x, y, new EmptyField());
        }
    }
    
    public void PlaceRandomItem(List<Item>? itemPool = null)
    {
        List<Item> items = itemPool ?? new List<Item> { new Coin(), new Junk(), new Clock(), new Book(), new Gold() };
        Item? randomItem = items[_rnd.Next(5)];
        
        int x = _rnd.Next(_map.Width);
        int y = _rnd.Next(_map.Height);
        while (!_map.GetField(x, y).IsEmpty())
        {
            x = _rnd.Next(_map.Width);
            y = _rnd.Next(_map.Height);
        }
        _map.GetField(x,y).PutItem(randomItem);
        
    }

    public void PlaceItemRandom(Item item)
    {
        int x = _rnd.Next(_map.Width);
        int y = _rnd.Next(_map.Height);
        while (!_map.GetField(x, y).IsEmpty())
        {
            x = _rnd.Next(_map.Width);
            y = _rnd.Next(_map.Height);
        }
        _map.GetField(x,y).PutItem(item);

    }

    
    
    public void PlaceRandomWeapon(List<Weapon>? weaponPool = null)
    {
        List<Weapon> weapons = weaponPool ?? new List<Weapon> { new DoubleHandedWeapon(), new Axe(), new SingleHandedWeapon() };

        Item? randomItem = weapons[_rnd.Next(3)];
        int x = _rnd.Next(_map.Width);
        int y = _rnd.Next(_map.Height);
        while (!_map.GetField(x, y).IsEmpty())
        {
            x = _rnd.Next(_map.Width);
            y = _rnd.Next(_map.Height);
        }
        _map.GetField(x,y).PutItem(randomItem);
        
    }

    public void PlaceWeaponRandom(Weapon weapon)
    {
        int x = _rnd.Next(_map.Width);
        int y = _rnd.Next(_map.Height);
        while (!_map.GetField(x, y).IsEmpty())
        {
            x = _rnd.Next(_map.Width);
            y = _rnd.Next(_map.Height);
        }
        _map.GetField(x,y).PutItem(weapon);

    }

    
    public void PlaceEnemyRandom(Enemy enemy)
    {
        int x = _rnd.Next(_map.Width);
        int y = _rnd.Next(_map.Height);
        while (!_map.GetField(x, y).IsPassable() || _map.GetField(x,y).HasEnemy())
        {
            x = _rnd.Next(_map.Width);
            y = _rnd.Next(_map.Height);
        }
        PlaceEnemy(x, y, enemy);
    }
    
    public void PlaceWall(int x,int y)
    {
        _map.SetField(x, y, new Wall());
    }

    public void PlaceItem(int x, int y, Item? item)
    {
        _map.GetField(x,y).PutItem(item);
    }
    
    public void PlaceEnemy(int x, int y, Enemy e)
    {
        _map.GetField(x,y).SetEnemy(e);
    }
    
}