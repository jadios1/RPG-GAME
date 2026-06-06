using RPG_Game.Logger;

namespace RPG_Game;

public static class StateMapper
{
    public static GameStateDTO MapToDTO(Model model)
    {
        var dto = new GameStateDTO
        {
            IsGameOver = model.IsGameOver,
            RecentLogs = GameLog.Instance.GetRecent(3)
        };

        foreach (var kvp in model.Players)
        {
            var p = kvp.Value;
            dto.Players[p.Id] = new PlayerDTO
            {
                Id = p.Id,
                Name = p.Name,
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
                Symbol = p.GetSymbol(),
                SelectedSlot = p.SelectedSlot,
                
                LeftHandName = p.LeftHand.HeldItem?.GetName() ?? "",
                RightHandName = p.RightHand.HeldItem?.GetName() ?? "",
                InventoryNames = p.Inventory.Select(item => item.GetName()).ToList()
            };
        }

        foreach (var enemy in model.Map.Enemies)
        {
            dto.Enemies.Add(new EnemyDTO
            {
                Name = enemy.Name,
                X = enemy.X,
                Y = enemy.Y,
                Health = enemy.Health,
                Armor = enemy.Armor,
                Attack = enemy.Attack,
                Symbol = enemy.GetSymbol()
            });
        }

        return dto;
    }
}