using System.Diagnostics.Contracts;

namespace RPG_Game;

public class Game
{
    private Map _map;
    private Player _player;
    private GameRender _gameRender;

    private Dictionary<ConsoleKey, Func<bool>> _keyActions;
    
    public Game()
    {
        _player = new Player();
        _map = new Map(20,40,_player);

        _gameRender = new GameRender();

        _keyActions = new Dictionary<ConsoleKey, Func<bool>>
        {
            { ConsoleKey.W,() => _map.TryMovePlayer(_player, 0, -1) },
            { ConsoleKey.A,() => _map.TryMovePlayer(_player, -1, 0) },
            { ConsoleKey.S,() => _map.TryMovePlayer(_player, 0, 1) },
            { ConsoleKey.D,() => _map.TryMovePlayer(_player, 1, 0) },
            {
                ConsoleKey.E,() => _player.PickUpItem(_map)
            },
            { ConsoleKey.R,() => _player.DropItem(_map) },
            { ConsoleKey.Z,() => _player.LeftHandPickup() },
            { ConsoleKey.X,() => _player.RightHandPickup() },
            { ConsoleKey.D1, () =>
                {
                    _player.SelectedSlot = 0; 
                    return true;   
                }
                 },
            { ConsoleKey.D2,() =>
                {
                    _player.SelectedSlot = 1;
                    return true;
                }
            },
            { ConsoleKey.D3,() =>
                {
                    _player.SelectedSlot = 2;
                    return true;
                }
            },
        };

        Console.CursorVisible = false;
        Console.Clear();
        
    }


    
    public void Run()
    {
        Console.Clear();
        while (true)
        {
            _gameRender.DrawMap(_map,_map.Height,_map.Width,_player);
            _gameRender.Info(_map.Width,_player,_map);

            var pressedkey = Console.ReadKey(true).Key;
            
            if (_keyActions.TryGetValue(pressedkey, out var action))
            {
                if (action() == false)
                {
                    _gameRender.CannotUse(_map,true);
                }
                else
                {
                    _gameRender.CannotUse(_map,false);
                }
            }
            else
            {
                _gameRender.CannotUse(_map,true);
            }
            
        }
        
        
    }


}