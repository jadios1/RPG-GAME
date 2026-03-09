namespace RPG_Game;

public class Map
{
    public Map(int height,int width)
    {
        Height = height;
        Width = width;
        _fields = new Field[width, height];

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                _fields[x, y] = new EmptyField();
                if (y == 0 || x == 0 || y == Height-1 || x == Width-1)
                {
                    PlaceWall(x,y);
                }

                if (y == 10 && x != 3)
                {
                    PlaceWall(x,y);
                }
            }
        }

        
        PlaceItem(11,12,new Coin());
        PlaceItem(10,12,new SingleHandedWeapon());
        PlaceItem(14,14,new DoubleHandedWeapon(12));
        PlaceItem(20,5,new Axe());
        PlaceItem(10,18,new Junk());
        PlaceItem(18, 16, new Gold());
        PlaceItem(19,2,new Clock());
    }
    
    private Field[,] _fields;
    public int Height { get; private set; }
    public int Width { get; private set; }

    public void TryMovePlayer(Player player,int dx,int dy)
    {
        if (_fields[player.X + dx, player.Y + dy].IsPassable())
        {
            player.Move(dx,dy);
        }
 
    }

    

    public Field GetField(int x,int y)
    {
        return _fields[x, y];
    }

    private void PlaceWall(int x,int y)
    {
        _fields[x, y] = new Wall();
    }

    private void PlaceItem(int x, int y, Item item)
    {
        _fields[x,y].PutItem(item);
    }
}