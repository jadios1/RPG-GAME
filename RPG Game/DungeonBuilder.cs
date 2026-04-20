using RPG_Game.Fields;
using RPG_Game.Items.Weapon;

namespace RPG_Game;

public class DungeonBuilder
{
    private Map map;

    public DungeonBuilder(Map map_)
    {
        map = map_;
    }
    
    public void GenerateEmpty()
    {
        for (int y = 0; y < map.Height; y++)
        {
            for (int x = 0; x < map.Width; x++)
            {
                map.SetField(x,y,new EmptyField());
                if (y == 0 || x == 0 || y == map.Height-1 || x == map.Width-1)
                {
                    PlaceWall(x,y);
                }

            }
        }

    }
    

    public void GenerateCentralRoom(int size)
    {
        int roomHeight = size;
        int roomWidht = roomHeight*2;
        for (int y = map.Height/2 - (roomHeight/2) ; y < map.Height/2 + (roomHeight/2); y++)
        {
            for (int x = map.Width/2 - (roomWidht/2); x < map.Width/2 + (roomWidht/2); x++)
            {
                map.SetField(x,y,new EmptyField());
            }
        }

    }
    
    public DungeonBuilder GenerateFilled()
    {
        for (int y = 0; y < map.Height; y++)
        {
            for (int x = 0; x < map.Width; x++)
            {
                PlaceWall(x,y);
            }
        }

        return this;
    }

    public void GenerateChamber(int size)
    {
        Random random = new Random();
        int sizeX = size * 2;
        int sizeY = size;
        int cordsX = random.Next(1, map.Width - sizeX);
        int cordsY = random.Next(1, map.Height - sizeY);
        
        
        for (int y = cordsY; y < cordsY+sizeY; y++)
        {
            for (int x = cordsX; x < cordsX+sizeX; x++)
            {
                map.SetField(x,y,new EmptyField());
            }
        }
        
    }

    public void GeneratePath(int length)
    {
        Random rnd = new Random();
        int randomx = rnd.Next(1,map.Width-1); 
        int randomy = rnd.Next(1,map.Height-1);

        int steps;
        int takensteps = 0;
        int direction = 0;
        while (takensteps < length)
        {
            if (length - takensteps <= 1) break;
            steps = rnd.Next(1,(length - takensteps) + 1);
            int dx = 0, dy = 0;
            while (true)
            {
                direction = rnd.Next(4);
                dx = direction == 0 ? 1 : direction == 1 ? -1 : 0;
                dy = direction == 2 ? 1 : direction == 3 ? -1 : 0;
                int newx = randomx + dx;
                int newy = randomy + dy;
                if (newx >= 1 && newx <= map.Width-2 && newy >= 1 && newy <= map.Height-2)
                    break;
            }
            for (int i = 0; i < steps; i++)
            {
                if (randomx + dx < 1 || randomx + dx > map.Width-2 || randomy + dy < 1 || randomy + dy > map.Height-2) break;
                if (direction == 0)
                {
                        randomx++;       
                }
                else if (direction == 1)
                {
                        randomx--;
                }
                else if(direction == 2)
                {
                    randomy++;
                }
                else if (direction == 3)
                {
                    randomy--;
                    
                }

                takensteps++;
                map.SetField(randomx,randomy,new EmptyField());
            }


        }

    }
    
    
    public void PlaceItemRandom()
    {
        Random rnd = new Random();
        List<Item?> items = new List<Item?> {new Coin(), new Junk(), new Clock(), new Book(), new Gold() };
        Item? randomItem = items[rnd.Next(5)];
        
        int x = rnd.Next(map.Width);
        int y = rnd.Next(map.Height);
        while (!map.GetField(x, y).IsEmpty())
        {
            x = rnd.Next(map.Width);
            y = rnd.Next(map.Height);
        }
        map.GetField(x,y).PutItem(randomItem);
        
    }
    
    
    public void PlaceWeaponRandom()
    {
        Random rnd = new Random();
        List<Weapon?> weapons = new List<Weapon?> { new DoubleHandedWeapon(), new Axe(), new SingleHandedWeapon() };
        Item? randomItem = weapons[rnd.Next(3)];
        int x = rnd.Next(map.Width);
        int y = rnd.Next(map.Height);
        while (!map.GetField(x, y).IsEmpty())
        {
            x = rnd.Next(map.Width);
            y = rnd.Next(map.Height);
        }
        map.GetField(x,y).PutItem(randomItem);
        
    }
    
    public void PlaceEnemyRandom(Enemy enemy)
    {
        Random rnd = new Random();
        int x = rnd.Next(map.Width);
        int y = rnd.Next(map.Height);
        while (!map.GetField(x, y).IsPassable() || map.GetField(x,y).HasEnemy())
        {
            x = rnd.Next(map.Width);
            y = rnd.Next(map.Height);
        }
        PlaceEnemy(x, y, enemy);
    }
    
    public void PlaceWall(int x,int y)
    {
        map.SetField(x, y, new Wall());
    }

    public void PlaceItem(int x, int y, Item? item)
    {
        map.GetField(x,y).PutItem(item);
    }
    
    public void PlaceEnemy(int x, int y, Enemy e)
    {
        map.GetField(x,y).SetEnemy(e);
    }
    
}