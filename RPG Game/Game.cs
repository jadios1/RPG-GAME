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
            _gameRender.Info(_map.Width,_player,_map);
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
                    _player.PutIntoInventory(currentfield.GetItem(),currentfield);

                }
                else
                {
                    var itemToDrop = _player.SelectedItem();
                    
                    if (itemToDrop != null)
                    {
                        currentfield.PutItem(itemToDrop);
                        _player.RemoveFromInventory(_player.SelectedSlot);
                    }
                }

            }
            else if(pressedkey == ConsoleKey.Escape)
            {
                return;
            }
            else if (pressedkey == ConsoleKey.Z)
            {
                if (_player.LeftHand.IsEmpty())
                {
                    var item = _player.SelectedItem();
                    if (item != null)
                    {
                        item.TryEquip(_player,_player.LeftHand);
                    }
                }
                else
                {
                    _player.LeftHand.HeldItem?.TryRemove(_player,_player.LeftHand);
                }
            }
            else if (pressedkey == ConsoleKey.X)
            {
                if (_player.RightHand.IsEmpty())
                {
                    var item = _player.SelectedItem();
                    if (item != null)
                    {
                        item.TryEquip(_player,_player.RightHand);
                    }
                }
                else
                {
                    _player.RightHand.HeldItem?.TryRemove(_player,_player.RightHand);
                }
            }
            else if (pressedkey == ConsoleKey.D1)
            {
                _player.SelectedSlot = 0;
            }
            else if (pressedkey == ConsoleKey.D2)
            {
                _player.SelectedSlot = 1;
            }
            else if (pressedkey == ConsoleKey.D3)
            {
                _player.SelectedSlot = 2;
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