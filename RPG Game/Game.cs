using RPG_Game.Events;
using RPG_Game.Fields;
using RPG_Game.Logger;
using RPG_Game.Themes;
using RPG_Game.Visitors;

namespace RPG_Game;

public class Game
{
    private Map _map;
    private Player _player;
    private GameRender _gameRender;
    private IDungeonTheme _theme;
    private Dictionary<ConsoleKey, Func<bool>> _keyActions;
    
    public Game(string playername)
    {
        _player = new Player(playername);
        _player = new Player(playername);
        
        var themes = new List<IDungeonTheme> { new MagicalTheme(),new LibraryTheme() };
        _theme = themes[new Random().Next(themes.Count)];
        
        
        _map = new Map(20, 40, _player, _theme);
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
            { ConsoleKey.J,() => _gameRender.DrawFullLog()},
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
            { ConsoleKey.F, () => {
                var enemy = _map.GetAdjacentEnemy(_player.X, _player.Y);
                if (enemy != null) StartCombat(enemy,_map.GetAdjacentEnemyField(_player.X,_player.Y)!);
                return enemy != null;
            }}
        };

        Console.CursorVisible = false;
        Console.Clear();
        
    }


    
    public void Run()
    { 
        Console.WriteLine(_theme.IntroMessage);
        Console.ReadKey(true);
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
                GameLog.Instance.Log("Pressed unknown key!");

            }
            
        }
        
        
    }
    
    private Dictionary<ConsoleKey, (IAttackVisitor, IDefenseVisitor)> _combatActions = new()
    {
        { ConsoleKey.D1, (new NormalAttackVisitor(), new NormalDefenseVisitor()) },
        { ConsoleKey.D2, (new StealthAttackVisitor(), new StealthDefenseVisitor()) },
        { ConsoleKey.D3, (new MagicalAttackVisitor(), new MagicalDefenseVisitor()) },
    };

    private void StartCombat(Enemy enemy, Field enemyField)
    {
        
        
        while (enemy.Health > 0 && _player.Health > 0)
        {
            _gameRender.DrawCombat(_player, enemy,_map.Width);
        
            var key = Console.ReadKey(true).Key;
        
            if (!_combatActions.TryGetValue(key, out var visitors)) continue;
        
            var (attackVisitor, defenseVisitor) = visitors;
        
            int playerDamage = _player.CalculateAttackDamage(attackVisitor);
            int playerDefense = _player.CalculateDefense(defenseVisitor);
        
            int damageToEnemy = Math.Max(0, playerDamage - enemy.Armor);
            int damageToPlayer = Math.Max(0, enemy.Attack - playerDefense);
            GameLog.Instance.Log("Player dealt " + damageToEnemy + "DMG to enemy (" + enemy.Name + ")");
            GameLog.Instance.Log("Enemy dealt " + damageToPlayer + "DMG to player (" + _player.Name + ")");

            enemy.Health -= damageToEnemy;
            _player.Health -= damageToPlayer;
        }
        Console.Clear();

        if (enemy.Health <= 0)
        {
            enemy.Notify(new EnemyDiedEvent(enemy));
            enemy.Unsubscribe(enemy.Species);
            enemy.Species.RemoveMember(enemy);
            enemyField.SetEnemy(null);
            GameLog.Instance.Log("Player defeated enemy (" + enemy.Name + ")");
        }
        if (_player.Health <= 0)
        {
            GameLog.Instance.Log("Player (" +_player.Name + ") Died!");
            _gameRender.DrawGameOver();
        }
    }

}