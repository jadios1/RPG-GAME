using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RPG_Game.Network;

public class GameServer
{
    private readonly int _port;
    private TcpListener _listener;
    private List<TcpClient> _clients = new();
    private Action<int, ConsoleKey> _onInputReceived;
    
    public Action<int> OnClientConnected { get; set; } 
    
    private int _nextPlayerId = 1; 
    
    public GameServer(int port, Action<int, ConsoleKey> onInputReceived)
    {
        _port = port;
        _onInputReceived = onInputReceived;
    }

    public void Start()
    {
        _listener = new TcpListener(IPAddress.Any, _port);
        _listener.Start();
        Task.Run(AcceptClientsAsync);
    }

    private async Task AcceptClientsAsync()
    {
        while (true)
        {
            if (_clients.Count >= 9)
            {
                await Task.Delay(1000);
                continue;
            }

            var client = await _listener.AcceptTcpClientAsync();
            _clients.Add(client);
            int playerId = _nextPlayerId++;

            var writer = new StreamWriter(client.GetStream()) { AutoFlush = true };
            await writer.WriteLineAsync($"ID:{playerId}");

            OnClientConnected?.Invoke(playerId);

            Task.Run(() => HandleClientAsync(client, playerId));
        }
    }

    private async Task HandleClientAsync(TcpClient client, int playerId)
    {
        var stream = client.GetStream();
        var buffer = new byte[1024];

        try
        {
            while (client.Connected)
            {
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                if (bytesRead == 0) break;

                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                
                if (Enum.TryParse<ConsoleKey>(message, true, out var key))
                {
                    _onInputReceived?.Invoke(playerId, key);
                }
            }
        }
        finally
        {
            _clients.Remove(client);
            client.Close();
        }
    }

    public void BroadcastState(GameStateDTO state)
    {
        var json = JsonSerializer.Serialize(state);
        var data = Encoding.UTF8.GetBytes(json + "\n"); 

        foreach (var client in _clients.ToArray())
        {
            try
            {
                if (client.Connected)
                {
                    client.GetStream().Write(data, 0, data.Length);
                }
            }
            catch { }
        }
    }
}