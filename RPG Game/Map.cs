using RPG_Game.Decorators;
using RPG_Game.Fields;
using RPG_Game.Items;
using RPG_Game.Items.Weapon;
using RPG_Game.Logger;
using RPG_Game.Themes;

namespace RPG_Game;

public class Map
{
    public Map(int height,int width,Player player,IDungeonTheme theme)
    {
        Height = height;
        Width = width;
        _fields = new Field[width, height];
        DungeonBuilder builder = new DungeonBuilder(this);
        SetField(player.X,player.Y,new EmptyField());
        builder.GetPlayerSpawn(player.X,player.Y);
        theme.Generate(builder);
        builder.ConnectRooms();
        foreach (var weapon in theme.GetWeaponPool())
        {
            builder.PlaceItemRandom(weapon);            
        }

        foreach (var item in theme.GetItemPool())
        {
            builder.PlaceItemRandom(item);

        }

        foreach (var enemies in theme.GetEnemies())
        {
            builder.PlaceEnemyRandom(enemies);
        }
        builder.PlaceItemRandom(theme.GetArtifact());

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

        if(!_fields[player.X + dx, player.Y + dy].HasEnemy()) GameLog.Instance.Log("Walking into wall!");
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