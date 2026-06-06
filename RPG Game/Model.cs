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
        Players[LocalPlayerId] = new Player(playername, LocalPlayerId);
        
        var themes = new List<IDungeonTheme> { new MagicalTheme(), new LibraryTheme(), new BossTheme() };
        Theme = themes[new Random().Next(themes.Count)];
        
        Map = new Map(20, 40, LocalPlayer, Theme);
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

    public bool PickUpItem() => LocalPlayer.PickUpItem(Map);
    public bool MovePlayer(int dx, int dy) => Map.TryMovePlayer(LocalPlayer, dx, dy);
    public bool DropItem() => LocalPlayer.DropItem(Map);
    public bool EquipLeft() => LocalPlayer.LeftHandPickup();
    public bool EquipRight() => LocalPlayer.RightHandPickup();

    public bool ChangeSlot(int selected)
    {
        LocalPlayer.SelectedSlot = selected;
        return true;
    }

    public bool StartCombat()
    {
        var enemy = Map.GetAdjacentEnemy(LocalPlayer.X, LocalPlayer.Y);
        if (enemy != null) 
        {
            CurrentEnemy = enemy;
            _currentEnemyField = Map.GetAdjacentEnemyField(LocalPlayer.X, LocalPlayer.Y);
            IsInCombat = true;
            return true;
        }
        return false;
    }

    public void ResolveCombatRound(IAttackVisitor attackVisitor, IDefenseVisitor defenseVisitor)
    {
        if (!IsInCombat || CurrentEnemy == null) return;

        int playerDamage = LocalPlayer.CalculateAttackDamage(attackVisitor);
        int playerDefense = LocalPlayer.CalculateDefense(defenseVisitor);
        
        int damageToEnemy = Math.Max(0, playerDamage - CurrentEnemy.Armor);
        int damageToPlayer = Math.Max(0, CurrentEnemy.Attack - playerDefense);
        
        GameLog.Instance.Log($"Player dealt {damageToEnemy}DMG to enemy ({CurrentEnemy.Name})");
        GameLog.Instance.Log($"Enemy dealt {damageToPlayer}DMG to player ({LocalPlayer.Name})");

        CurrentEnemy.Health -= damageToEnemy;
        LocalPlayer.Health -= damageToPlayer;

        if (CurrentEnemy.Health <= 0 || LocalPlayer.Health <= 0)
        {
            EndCombat();
        }
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
        
        if (LocalPlayer.Health <= 0)
        {
            GameLog.Instance.Log($"Player ({LocalPlayer.Name}) Died!");
            SetGameOver();
        }
    }
    public void FleeCombat()
    {
        if (!IsInCombat) return;

        GameLog.Instance.Log($"Player ({LocalPlayer.Name}) fled from combat!");
        IsInCombat = false;
        CurrentEnemy = null;
    }
}