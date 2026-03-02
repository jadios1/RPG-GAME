namespace RPG_Game;

public class Game
{
    private Map _map;
    private Player _player;
    private GameRender _gameRender;

    public void Run()
    {
        while (true)
        {
            _gameRender.DrawMap(_map,_map.Height,_map.Width,_player);
            _gameRender.Info(_map.Width,_player);
            var pressedkey = Console.ReadKey(true).Key;
            var currentfield = _map.GetField(_player.X, _player.Y);
            
            if (pressedkey == ConsoleKey.W)
            {
                _map.TryMovePlayer(_player,0,-1);
            }
            else if (pressedkey == ConsoleKey.S)
            {
                _map.TryMovePlayer(_player,0,1);
            }
            else if (pressedkey == ConsoleKey.A)
            {
                _map.TryMovePlayer(_player,-1,0);
            }
            else if (pressedkey == ConsoleKey.D)
            {
                _map.TryMovePlayer(_player,1,0);
            }
            else if (pressedkey == ConsoleKey.E)
            {
                if (currentfield.IsEmpty() == false)
                {
                    _player.PutIntoInventory(currentfield.GetItem());
                }
            }
            else if(pressedkey == ConsoleKey.Escape)
            {
                return;
            }
        }
    }

    public Game()
    {
        _map = new Map(20,40);
        _player = new Player();
        _gameRender = new GameRender();
        Console.CursorVisible = false;
        Console.Clear();
    }
}