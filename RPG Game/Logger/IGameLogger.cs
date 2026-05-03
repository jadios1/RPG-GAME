namespace RPG_Game.Logger;

public interface IGameLogger
{
    string FilePath { get; }

    void Log(string message);
    List<string> GetRecent(int count);
    List<string> GetAll();
}