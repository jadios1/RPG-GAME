using RPG_Game.Decorators;
using RPG_Game.Events;
using RPG_Game.Fields;
using RPG_Game.Items;
using RPG_Game.Items.Weapon;
using RPG_Game.Logger;
using RPG_Game.Themes;

namespace RPG_Game;

public class Map : IObservable
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
            Subscribe(enemies);
        }
        builder.PlaceItemRandom(theme.GetArtifact());
    }


    private List<IObserver> _observers = new();

    public void Subscribe(IObserver observer) => _observers.Add(observer);
    public void Unsubscribe(IObserver observer) => _observers.Remove(observer);
    public void Notify(IEvent gameEvent)
    {
        foreach (var obs in _observers)
            obs.OnNotify(gameEvent);
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

    public int GetDistance(int x1, int y1, int x2, int y2)
    {
        if (!GetField(x1, y1).IsPassable() && !GetField(x1,y1).HasEnemy()) return int.MaxValue;
    
        var visited = new bool[Width, Height];
        var queue = new Queue<(int x, int y, int dist)>();
        queue.Enqueue((x1, y1, 0));
        visited[x1, y1] = true;

        int[] dx = { 0, 0, 1, -1 };
        int[] dy = { 1, -1, 0, 0 };

        while (queue.Count > 0)
        {
            var (cx, cy, dist) = queue.Dequeue();
            if (cx == x2 && cy == y2) return dist;

            for (int i = 0; i < 4; i++)
            {
                int nx = cx + dx[i];
                int ny = cy + dy[i];
                if (nx < 0 || nx >= Width || ny < 0 || ny >= Height) continue;
                if (visited[nx, ny]) continue;
                if (!GetField(nx, ny).IsPassable() && !(nx == x2 && ny == y2)) continue;
                visited[nx, ny] = true;
                queue.Enqueue((nx, ny, dist + 1));
            }
        }
        return int.MaxValue;
    }
}