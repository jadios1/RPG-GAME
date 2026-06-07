using System;
using System.Collections.Concurrent;
using System.Threading;
using RPG_Game;
using RPG_Game.Logger;
using RPG_Game.Network;

namespace RPG_Game.Runners;

public class ServerRunner : IGameRunner
{
    private readonly int _port;
    private ConcurrentQueue<Action> _networkActions = new();

    public ServerRunner(int port)
    {
        _port = port;
    }

    public void Run()
    {
        Model model = new Model();
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

        Console.CursorVisible = false;
        Console.Clear();
        Console.WriteLine(model.Theme.IntroMessage);
        Console.WriteLine($"\n[Server started on port {_port}. Waiting for clients...]");
        Console.WriteLine("Clients should connect as 1-9.");
        Console.ReadKey(true);
        Console.Clear();

        while (!model.IsGameOver)
        {
            while (_networkActions.TryDequeue(out var networkAction))
            {
                networkAction.Invoke();
            }

            view.DrawMap(model.Map, model.Map.Height, model.Map.Width, model.Players);
            view.DrawServerUI(model);

            model.Update();

            var gameState = StateMapper.MapToDTO(model);
            server.BroadcastState(gameState);

            Thread.Sleep(50);
        }

        view.DrawGameOver();
    }
}