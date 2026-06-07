using System.Collections.Generic;
using System.Linq;

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
            var field = model.Map.GetField(p.X, p.Y);
            var itemOnField = field.IsEmpty() ? null : field.GetItem();
            var adjacentEnemy = model.Map.GetAdjacentEnemy(p.X, p.Y);
            
            bool isFighting = model.PlayerCombats.ContainsKey(p.Id);
            var combatEnemy = isFighting ? model.PlayerCombats[p.Id] : null;

            dto.Players[kvp.Key] = new PlayerDTO
            {
                Id = p.Id,
                Name = p.Name,
                Symbol = p.GetSymbol(),
                X = p.X,
                Y = p.Y,
                Health = p.Health,
                Gold = p.Gold,
                Coins = p.Coins,
                Strength = p.Strength,
                Dexterity = p.Dexterity,
                Wisdom = p.Wisdom,
                Aggression = p.Aggression,
                Luck = p.Luck,
                SelectedSlot = p.SelectedSlot,
                LeftHandName = p.LeftHand.HeldItem?.GetName() ?? "",
                RightHandName = p.RightHand.HeldItem?.GetName() ?? "",
                InventoryNames = p.Inventory.Select(i => i.GetName()).ToList(),
                
                IsStandingOnItem = !field.IsEmpty(),
                StandingOnItemName = itemOnField?.GetName() ?? "",
                StandingOnItemDesc = itemOnField?.Description() ?? "",
                HasSelectedItem = p.SelectedItem() != null,
                CanEquipLeft = (p.SelectedItem() != null && p.SelectedItem()!.IsEquipable()) || !p.LeftHand.IsEmpty(),
                CanEquipRight = (p.SelectedItem() != null && p.SelectedItem()!.IsEquipable()) || !p.RightHand.IsEmpty(),
                CanFight = adjacentEnemy != null,
                
                IsInCombat = isFighting,
                CombatEnemy = combatEnemy != null ? new EnemyDTO 
                { 
                    Name = combatEnemy.Name, 
                    Health = combatEnemy.Health, 
                    Armor = combatEnemy.Armor,
                    Attack = combatEnemy.Attack
                } : null
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
        
        return dto;
    }
}