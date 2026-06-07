using System.Collections.Generic;

namespace RPG_Game;

public class PlayerDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int X { get; set; }
    public int Y { get; set; }
    public int Health { get; set; }
    public int Gold { get; set; }
    public int Coins { get; set; }
    public int Strength { get; set; }
    public int Dexterity { get; set; }
    public int Wisdom { get; set; }
    public int Aggression { get; set; }
    public int Luck { get; set; }
    public char Symbol { get; set; }
    
    public List<string> InventoryNames { get; set; } = new();
    public string LeftHandName { get; set; } = string.Empty;
    public string RightHandName { get; set; } = string.Empty;
    public int SelectedSlot { get; set; }

    public bool IsStandingOnItem { get; set; }
    public string StandingOnItemName { get; set; } = string.Empty;
    public string StandingOnItemDesc { get; set; } = string.Empty;
    public bool HasSelectedItem { get; set; }
    public bool CanEquipLeft { get; set; }
    public bool CanEquipRight { get; set; }
    public bool CanFight { get; set; }
    
    public bool IsInCombat { get; set; }
    public EnemyDTO? CombatEnemy { get; set; }
}