using System.Collections.Generic;
using System.Linq;
using RPG_Game;

namespace RPG_Game;

public static class StateMapper
{
    public static GameStateDTO MapToDTO(Model model)
    {
        var dto = new GameStateDTO
        {
            IsGameOver = model.IsGameOver,
            RecentLogs = Logger.GameLog.Instance.GetRecent(3)
        };

        dto.MapGrid = new string[model.Map.Height];
        for (int y = 0; y < model.Map.Height; y++)
        {
            var row = new System.Text.StringBuilder();
            for (int x = 0; x < model.Map.Width; x++)
            {
                row.Append(model.Map.GetField(x, y).GetSymbol());
            }
            dto.MapGrid[y] = row.ToString();
        }

        dto.Players = new Dictionary<int, PlayerDTO>();
        foreach (var kvp in model.Players)
        {
            var p = kvp.Value;
            dto.Players[kvp.Key] = new PlayerDTO
            {
                Id = p.Id,
                Name = p.Name,
                Symbol = p.GetSymbol(),
                X = p.X,
                Y = p.Y,
                Health = p.Health,
                Gold = p.Gold,
                SelectedSlot = p.SelectedSlot,
                LeftHandName = p.LeftHand.HeldItem?.GetName() ?? "",
                RightHandName = p.RightHand.HeldItem?.GetName() ?? "",
                InventoryNames = p.Inventory.Select(i => i.GetName()).ToList()
            };
        }

        dto.Enemies = new List<EnemyDTO>();
        foreach (var enemy in model.Map.Enemies) 
        {
            dto.Enemies.Add(new EnemyDTO
            {
                Name = enemy.Name,
                Symbol = enemy.GetSymbol(),
                X = enemy.X,
                Y = enemy.Y,
                Health = enemy.Health,
                Armor = enemy.Armor,
                Attack = enemy.Attack
            });
        }
        dto.IsInCombat = model.IsInCombat;
        dto.CurrentEnemy = model.CurrentEnemy != null ? new EnemyDTO 
        { 
            Name = model.CurrentEnemy.Name, 
            Health = model.CurrentEnemy.Health, 
            Armor = model.CurrentEnemy.Armor,
            Attack = model.CurrentEnemy.Attack
        } : null;

        return dto;
    }
}