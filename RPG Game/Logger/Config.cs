namespace RPG_Game.Logger;

public class Config
{
    public string PlayerName { get; set; } = "";
    public string LogPath { get; set; } = "";
    
    public static Config Load(string path)
    {
        string json = File.ReadAllText(path);
        return System.Text.Json.JsonSerializer.Deserialize<Config>(json)!;
    }
}