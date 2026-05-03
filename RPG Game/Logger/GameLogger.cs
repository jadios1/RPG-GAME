using System.Text;

namespace RPG_Game.Logger;

public class GameLogger : IGameLogger
{
    private List<string> _logs = new();
    public string FilePath { get; private set; }

    public GameLogger(string filePath)
    {
        FilePath = filePath;

        if (File.Exists(filePath))
        {
            throw new Exception("Log file already exists!");
        }
        
        string? dir = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(dir))
            Directory.CreateDirectory(dir);

    }

    public void Log(string message)
    {
        string entry = $"[{DateTime.Now:HH:mm:ss}] {message}";
        _logs.Add(entry);

        File.AppendAllText(FilePath, entry + Environment.NewLine, Encoding.UTF8);
    }

    public List<string> GetRecent(int count)
    {
        return _logs.TakeLast(count).ToList();
    }

    public List<string> GetAll()
    {
        return new List<string>(_logs);
    }
}