using System.Text;
using RPG_Game;
using RPG_Game.Logger;

Console.OutputEncoding = Encoding.UTF8;


var config = Config.Load("config.json");
string logFile = Path.Combine(config.LogPath, $"{config.PlayerName}_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.log");
GameLog.Instance = new GameLogger(logFile);
Game game = new Game(config.PlayerName);

game.Run();

