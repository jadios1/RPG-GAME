using System;
using System.IO;
using System.Text;
using RPG_Game;
using RPG_Game.Logger;
using RPG_Game.Runners;

Console.OutputEncoding = Encoding.UTF8;

var config = Config.Load("config.json");
string logFile = Path.Combine(config.LogPath, $"{config.PlayerName}_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.log");
GameLog.Instance = new GameLogger(logFile);

bool isServer = false;
string ip = "127.0.0.1";
int port = 5555;

if (args.Length > 0)
{
    if (args[0] == "--server")
    {
        isServer = true;
        if (args.Length > 1) int.TryParse(args[1], out port);
    }
    else if (args[0] == "--client")
    {
        isServer = false;
        if (args.Length > 1)
        {
            var parts = args[1].Split(':');
            ip = parts[0];
            if (parts.Length > 1) int.TryParse(parts[1], out port);
        }
    }
}
else
{
    Console.Clear();
    Console.WriteLine("Start as Server [S] or Client [C]?");
    var choice = Console.ReadKey(true).Key;
    isServer = (choice == ConsoleKey.S);
}

IGameRunner runner;

if (isServer)
{
    runner = new ServerRunner(config.PlayerName, port);
}
else
{
    runner = new ClientRunner(config.PlayerName, ip, port);
}

runner.Run();