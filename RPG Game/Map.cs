namespace RPG_Game;

public class Map
{
    public Map(int _height,int _width)
    {
        Height = _height;
        Width = _width;
        fields = new Field[20, 40];
        
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                if (i % 4==0)
                {
                    fields[i, j] = new Wall();
                }
                else
                {
                    fields[i, j] = new EmptyField();
                }
            }
            
        }
        
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
    
    
    
}