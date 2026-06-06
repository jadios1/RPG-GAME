using System.Text;
using RPG_Game;
using RPG_Game.Logger;

Console.OutputEncoding = Encoding.UTF8;

var config = Config.Load("config.json");
string logFile = Path.Combine(config.LogPath, $"{config.PlayerName}_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.log");
GameLog.Instance = new GameLogger(logFile);

Model model = new Model(config.PlayerName);
GameRender view = new GameRender();
Controller controller = new Controller();

Console.CursorVisible = false;
Console.Clear();

Console.WriteLine(model.Theme.IntroMessage);
Console.ReadKey(true);
Console.Clear();

while (!model.IsGameOver)
{
    if (model.ShowFullLog)
    {
        view.DrawFullLog();
    }
    else if (model.IsInCombat)
    {
        view.DrawCombat(model.LocalPlayer, model.CurrentEnemy!, model.Map.Width);
        
    }
    else
    {
        view.DrawMap(model.Map, model.Map.Height, model.Map.Width, model.Players);
        view.Info(model.Map.Width, model.LocalPlayer, model.Map);
    }

    controller.HandleInput(model);

    model.Update(); 
}

view.DrawGameOver();