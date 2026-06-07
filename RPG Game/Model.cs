using RPG_Game.Events;
using RPG_Game.Fields;
using RPG_Game.Logger;
using RPG_Game.Themes;
using RPG_Game.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RPG_Game;

public class Model
{
    public Map Map { get; private set; }
    public IDungeonTheme Theme { get; private set; }
    
    public Dictionary<int, Player> Players { get; private set; }
    public int LocalPlayerId { get; private set; }
    public Player LocalPlayer => Players[LocalPlayerId];
    
    public int ActivePlayerId { get; set; }
    public Player ActivePlayer => Players[ActivePlayerId];
    
    public bool IsGameOver { get; private set; } = false;
    public bool ShowFullLog { get; set; } = false;
    
    public Dictionary<int, Enemy> PlayerCombats { get; private set; } = new();
    
    public bool IsInCombat => PlayerCombats.ContainsKey(ActivePlayerId);
    public Enemy? CurrentEnemy => PlayerCombats.TryGetValue(ActivePlayerId, out var e) ? e : null;
    
    private int _turnCounter = 0;

    public Model(string playername)
    {
        IsGameOver = false;
        
        Players = new Dictionary<int, Player>();
        LocalPlayerId = 0;
        ActivePlayerId = 0;
        Players[LocalPlayerId] = new Player(playername, LocalPlayerId);
        
        var themes = new List<IDungeonTheme> { new MagicalTheme(), new LibraryTheme(), new BossTheme() };
        Theme = themes[new Random().Next(themes.Count)];
        
        Map = new Map(20, 40, LocalPlayer, Theme);
    }

    public void AddNetworkPlayer(int id)
    {
        if (!Players.ContainsKey(id))
        {
            Players[id] = new Player($"Player {id}", id);
            Players[id].X = LocalPlayer.X + 1;
            Players[id].Y = LocalPlayer.Y;
        }
    }
    
    public void Update()
    {
        _turnCounter++;
        if (_turnCounter % 3 == 0)
        {
            Map.MoveEnemies(PlayerCombats.Values);
        }

        if (LocalPlayer.Health <= 0)
        {
            GameLog.Instance.Log($"Player ({LocalPlayer.Name}) Died!");
            SetGameOver();
        }
    }

    public void SetGameOver()
    {
        IsGameOver = true;
    }

    public bool PickUpItem() => ActivePlayer.PickUpItem(Map);
    public bool MovePlayer(int dx, int dy) => Map.TryMovePlayer(ActivePlayer, dx, dy);
    public bool DropItem() => ActivePlayer.DropItem(Map);
    public bool EquipLeft() => ActivePlayer.LeftHandPickup();
    public bool EquipRight() => ActivePlayer.RightHandPickup();

    public bool ChangeSlot(int selected)
    {
        ActivePlayer.SelectedSlot = selected;
        return true;
    }

    public bool StartCombat()
    {
        var enemy = Map.GetAdjacentEnemy(ActivePlayer.X, ActivePlayer.Y);
        if (enemy != null && !PlayerCombats.ContainsValue(enemy)) 
        {
            PlayerCombats[ActivePlayerId] = enemy;
            return true;
        }
        return false;
    }

    public void ResolveCombatRound(IAttackVisitor attackVisitor, IDefenseVisitor defenseVisitor)
    {
        if (!IsInCombat || CurrentEnemy == null) return;

        var enemy = CurrentEnemy;

        int playerDamage = ActivePlayer.CalculateAttackDamage(attackVisitor);
        int playerDefense = ActivePlayer.CalculateDefense(defenseVisitor);
        
        int damageToEnemy = Math.Max(0, playerDamage - enemy.Armor);
        int damageToPlayer = Math.Max(0, enemy.Attack - playerDefense);
        
        GameLog.Instance.Log($"{ActivePlayer.Name} dealt {damageToEnemy}DMG to enemy ({enemy.Name})");
        GameLog.Instance.Log($"Enemy dealt {damageToPlayer}DMG to {ActivePlayer.Name}");

        enemy.Health -= damageToEnemy;
        ActivePlayer.Health -= damageToPlayer;

        if (enemy.Health <= 0 || ActivePlayer.Health <= 0)
        {
            EndCombat();
        }
    }

    public void FleeCombat()
    {
        if (!IsInCombat) return;

        GameLog.Instance.Log($"Player ({ActivePlayer.Name}) fled from combat!");
        PlayerCombats.Remove(ActivePlayerId);
    }

    private void EndCombat()
    {
        var enemy = CurrentEnemy;
        if (enemy != null && enemy.Health <= 0)
        {
            enemy.Notify(new EnemyDiedEvent(enemy));
            enemy.Unsubscribe(enemy.Species);
            Map.RemoveEnemy(enemy);
            enemy.Species.RemoveMember(enemy);
            Map.Unsubscribe(enemy);
            
            Map.GetField(enemy.X, enemy.Y).SetEnemy(null);
            
            GameLog.Instance.Log($"Player defeated enemy ({enemy.Name})");
        }
        
        PlayerCombats.Remove(ActivePlayerId);
        
        if (ActivePlayer.Health <= 0 && ActivePlayer.Id == LocalPlayerId)
        {
            GameLog.Instance.Log($"Player ({ActivePlayer.Name}) Died!");
            SetGameOver();
        }
    }
}