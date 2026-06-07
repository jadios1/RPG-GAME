using RPG_Game.Events;
using RPG_Game.Fields;
using RPG_Game.Logger;
using RPG_Game.Themes;
using RPG_Game.Visitors;
using System;
using System.Collections.Generic;

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
    public bool IsInCombat { get; private set; } = false;
    
    public Enemy? CurrentEnemy { get; private set; }
    private Field? _currentEnemyField;
    
    private int _turnCounter = 0;

    public Model(string playername)
    {
        IsGameOver = false;
        
        Players = new Dictionary<int, Player>();
        LocalPlayerId = 1;
        ActivePlayerId = 1;
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
            Map.MoveEnemies();
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
        if (enemy != null) 
        {
            CurrentEnemy = enemy;
            _currentEnemyField = Map.GetAdjacentEnemyField(ActivePlayer.X, ActivePlayer.Y);
            IsInCombat = true;
            return true;
        }
        return false;
    }

    public void ResolveCombatRound(IAttackVisitor attackVisitor, IDefenseVisitor defenseVisitor)
    {
        if (!IsInCombat || CurrentEnemy == null) return;

        int playerDamage = ActivePlayer.CalculateAttackDamage(attackVisitor);
        int playerDefense = ActivePlayer.CalculateDefense(defenseVisitor);
        
        int damageToEnemy = Math.Max(0, playerDamage - CurrentEnemy.Armor);
        int damageToPlayer = Math.Max(0, CurrentEnemy.Attack - playerDefense);
        
        GameLog.Instance.Log($"{ActivePlayer.Name} dealt {damageToEnemy}DMG to enemy ({CurrentEnemy.Name})");
        GameLog.Instance.Log($"Enemy dealt {damageToPlayer}DMG to {ActivePlayer.Name}");

        CurrentEnemy.Health -= damageToEnemy;
        ActivePlayer.Health -= damageToPlayer;

        if (CurrentEnemy.Health <= 0 || ActivePlayer.Health <= 0)
        {
            EndCombat();
        }
    }

    public void FleeCombat()
    {
        if (!IsInCombat) return;

        GameLog.Instance.Log($"Player ({ActivePlayer.Name}) fled from combat!");
        IsInCombat = false;
        CurrentEnemy = null;
    }

    private void EndCombat()
    {
        if (CurrentEnemy != null && CurrentEnemy.Health <= 0)
        {
            CurrentEnemy.Notify(new EnemyDiedEvent(CurrentEnemy));
            CurrentEnemy.Unsubscribe(CurrentEnemy.Species);
            Map.RemoveEnemy(CurrentEnemy);
            CurrentEnemy.Species.RemoveMember(CurrentEnemy);
            Map.Unsubscribe(CurrentEnemy);
            _currentEnemyField?.SetEnemy(null);
            GameLog.Instance.Log($"Player defeated enemy ({CurrentEnemy.Name})");
        }
        
        IsInCombat = false;
        CurrentEnemy = null;
        _currentEnemyField = null;
        
        if (ActivePlayer.Health <= 0 && ActivePlayer.Id == LocalPlayerId)
        {
            GameLog.Instance.Log($"Player ({ActivePlayer.Name}) Died!");
            SetGameOver();
        }
    }
}