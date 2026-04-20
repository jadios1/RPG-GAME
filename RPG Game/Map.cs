using RPG_Game.Decorators;
using RPG_Game.Fields;
using RPG_Game.Items.Weapon;

namespace RPG_Game;

public class Map
{
    public Map(int height,int width,Player player)
    {
        Height = height;
        Width = width;
        _fields = new Field[width, height];
        DungeonBuilder builder = new DungeonBuilder(this);

        builder.GenerateFilled();
        builder.GenerateChamber(5);
        builder.GeneratePath(50);
        builder.GeneratePath(50);
        builder.GeneratePath(50);
        builder.GeneratePath(50);
        builder.GenerateChamber(5);
        builder.GenerateCentralRoom(5); 
        builder.PlaceItemRandom();
        builder.PlaceItemRandom();
        builder.PlaceItemRandom();
        builder.PlaceItemRandom();
        builder.PlaceWeaponRandom();
        builder.PlaceWeaponRandom();
        builder.PlaceWeaponRandom();
        builder.PlaceItem(10,10,new UnluckyDecorator(new Clock()));
        builder.PlaceItem(10,11,new UnluckyDecorator(new StrongDecorator(new Axe())));

        SetField(player.X,player.Y,new EmptyField());
        builder.PlaceEnemyRandom(new Enemy("bro",60, 10, 5));
        builder.PlaceEnemyRandom(new Enemy("hello",20, 10, 5));
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

    public Enemy? GetAdjacentEnemy(int x, int y)
    {
        int[] dx = { 0, 0, 1, -1 };
        int[] dy = { 1, -1, 0, 0 };
        for (int i = 0; i < 4; i++)
        {
            var field = GetField(x + dx[i], y + dy[i]);
            if (field.HasEnemy()) return field.GetEnemy();
        }
        return null;
    }
    public Field? GetAdjacentEnemyField(int x, int y)
    {
        int[] dx = { 0, 0, 1, -1 };
        int[] dy = { 1, -1, 0, 0 };
        for (int i = 0; i < 4; i++)
        {
            var field = GetField(x + dx[i], y + dy[i]);
            if (field.HasEnemy()) return field;
        }
        return null;
    }

}