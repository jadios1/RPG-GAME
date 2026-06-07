using System;
using System.Collections.Concurrent;
using System.Threading;
using RPG_Game;
using RPG_Game.Logger;
using RPG_Game.Network;

namespace RPG_Game.Runners;

public class ServerRunner : IGameRunner
{
    private readonly string _playerName;
    private readonly int _port;
    
    private ConcurrentQueue<Action> _networkActions = new();

    public ServerRunner(string playerName, int port)
    {
        _playerName = playerName;
        _port = port;
    }

    public void Run()
    {
        Model model = new Model(_playerName);
        GameRender view = new GameRender();
        Controller controller = new Controller();

        var server = new GameServer(_port, (playerId, key) =>
        {
            _networkActions.Enqueue(() =>
            {
                model.ActivePlayerId = playerId; 
                controller.HandleInput(model, key);
            });
        });
        
        server.OnClientConnected = (playerId) => 
        {
            _networkActions.Enqueue(() => model.AddNetworkPlayer(playerId));
        };
        
        server.Start();
        model.ActivePlayerId = model.LocalPlayerId;

        Console.CursorVisible = false;
        Console.Clear();
        Console.WriteLine(model.Theme.IntroMessage);
        Console.WriteLine($"\n[Server started on port {_port}. Waiting for clients...]");
        Console.ReadKey(true);
        Console.Clear();

        while (!model.IsGameOver)
        {
            while (_networkActions.TryDequeue(out var networkAction))
            {
                networkAction.Invoke();
            }

            model.ActivePlayerId = model.LocalPlayerId;

            if (model.ShowFullLog) 
            {
                view.DrawFullLog();
            }
            else 
            {
                view.DrawMap(model.Map, model.Map.Height, model.Map.Width, model.Players);

                if (model.PlayerCombats.ContainsKey(model.LocalPlayerId))
                {
                    var myEnemy = model.PlayerCombats[model.LocalPlayerId];
                    view.DrawCombatInterface(
                        model.LocalPlayer.Name, 
                        model.LocalPlayer.Health, 
                        myEnemy.Name, 
                        myEnemy.Health, 
                        myEnemy.Armor, 
                        myEnemy.Attack, 
                        model.Map.Width,
                        GameLog.Instance.GetRecent(3)
                    );
                }
                else
                {
                    view.Info(model.Map.Width, model.LocalPlayer, model.Map);
                }
            }
            
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                model.ActivePlayerId = model.LocalPlayerId;
                controller.HandleInput(model, key);
            }

            model.Update(); 
            
            var gameState = StateMapper.MapToDTO(model);
            server.BroadcastState(gameState);

            Thread.Sleep(50);
        }

        view.DrawGameOver();
    }
}