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
            Console.SetCursorPosition(0, 0);
            _gameRender.DrawMap(_map,_map.Width,_map.Height,_player);
            var pressedkey = Console.ReadKey().Key;
            var currentfield = _map.GetField(_player.X, _player.Y);
            if (pressedkey == ConsoleKey.W)
            {
                _map.TryMovePlayer(_player,-1,0);
            }
            else if (pressedkey == ConsoleKey.S)
            {
                _map.TryMovePlayer(_player,1,0);
            }
            else if (pressedkey == ConsoleKey.A)
            {
                _map.TryMovePlayer(_player,0,-1);
            }
            else if (pressedkey == ConsoleKey.D)
            {
                _map.TryMovePlayer(_player,0,1);
            }
            else if (pressedkey == ConsoleKey.E)
            {
                
                if (currentfield.IsEmpty() == false)
                {
                    _player.PutIntoInventory(currentfield.GetItem());
                }
            }
        }
    }

    public Game()
    {
        _map = new Map(40,20);
        _player = new Player();
        _gameRender = new GameRender();
    }
}