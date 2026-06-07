using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace RPG_Game.Runners;

public class ClientRunner : IGameRunner
{
    private readonly string _playerName;
    private readonly string _ip;
    private readonly int _port;
    private GameStateDTO? _latestState;

    public ClientRunner(string playerName, string ip, int port)
    {
        _playerName = playerName;
        _ip = ip;
        _port = port;
    }

    public void Run()
    {
        Console.Clear();
        Console.WriteLine($"Connecting to {_ip}:{_port} as {_playerName}...");

        using var tcpClient = new TcpClient();
        try
        {
            tcpClient.Connect(_ip, _port);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to connect: {ex.Message}");
            Console.ReadKey();
            return;
        }

        var stream = tcpClient.GetStream();
        var reader = new StreamReader(stream);
        var writer = new StreamWriter(stream) { AutoFlush = true };
        var view = new GameRender();
        
        int myNetworkId = 2;

        Console.CursorVisible = false;
        Console.Clear();

        var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        Task.Run(async () =>
        {
            try
            {
                while (tcpClient.Connected)
                {
                    var json = await reader.ReadLineAsync();
                    if (!string.IsNullOrEmpty(json))
                    {
                        _latestState = JsonSerializer.Deserialize<GameStateDTO>(json, jsonOptions);
                        
                        if (_latestState != null && _latestState.Players.Keys.Any(id => id != 1))
                        {
                            myNetworkId = _latestState.Players.Keys.First(id => id != 1);
                        }
                    }
                }
            }
            catch {}
        });

        while (true)
        {
            if (_latestState != null)
            {
                if (_latestState.IsGameOver) break;

                if (_latestState.IsInCombat)
                {
                    view.DrawCombatInterface(
                        "Player 2", // lub nazwa z DTO
                        _latestState.Players[myNetworkId].Health,
                        _latestState.CurrentEnemy.Name,
                        _latestState.CurrentEnemy.Health,
                        _latestState.CurrentEnemy.Armor,
                        _latestState.CurrentEnemy.Attack,
                        _latestState.MapGrid[0].Length // Szerokość mapy z DTO
                    );                
                }
                else
                {
                    view.DrawNetworkState(_latestState, myNetworkId);
                }
            }

            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                writer.WriteLine(key.ToString()); 
            }

            Thread.Sleep(50);
        }

        Console.Clear();
        Console.WriteLine("DISCONNECTED OR GAME OVER");
        Console.ReadKey();
    }
}