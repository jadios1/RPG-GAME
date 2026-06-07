namespace RPG_Game;

public class GameStateDTO
{
    public string[] MapGrid { get; set; } = Array.Empty<string>();
    public Dictionary<int, PlayerDTO> Players { get; set; } = new();
    public List<EnemyDTO> Enemies { get; set; } = new();
    public List<string> RecentLogs { get; set; } = new();
    public bool IsGameOver { get; set; }
    
    public bool IsInCombat { get; set; }
    
    public EnemyDTO? CurrentEnemy { get; set; }
}