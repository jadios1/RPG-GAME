namespace RPG_Game;

public class Map
{
    public Map(int _height,int _width)
    {
        Height = _height;
        Width = _width;
        fields = new Field[_width, _height];

        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                fields[i, j] = new EmptyField();
            }
        }

        for (int i = 0; i < 20; i++)
        {
            PlaceWall(4,i);
        }

        Coin hello = new Coin();
        SingleHandedWeapon bron2 = new SingleHandedWeapon();
        PlaceItem(10,10,hello);
        PlaceItem(10,12,bron2);
    }
    
    private Field[,] fields;
    public int Height { get; private set; }
    public int Width { get; private set; }

    public void TryMovePlayer(Player player,int dx,int dy)
    {
        if (fields[player.X + dx, player.Y + dy].IsPassable())
        {
            player.Move(dx,dy);
        }
 
    }

    public Field GetField(int x,int y)
    {
        return fields[x, y];
    }

    public void PlaceWall(int x,int y)
    {
        fields[x, y] = new Wall();
    }

    public void PlaceItem(int x, int y, Item item)
    {
        fields[x,y].PutItem(item);
    }
    
}