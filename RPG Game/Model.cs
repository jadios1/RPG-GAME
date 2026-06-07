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

    public Dictionary<int, Player> Players { get; private set; } = new();

    public int ActivePlayerId { get; set; }
    public Player? ActivePlayer
    {
        get
        {
            return Players.ContainsKey(ActivePlayerId) ? Players[ActivePlayerId] : null;
        }
    }

    public bool IsGameOver { get; private set; } = false;
    public bool ShowFullLog { get; set; } = false;

    public Dictionary<int, Enemy> PlayerCombats { get; private set; } = new();

    public bool IsInCombat
    {
        get
        {
            if (PlayerCombats.ContainsKey(ActivePlayerId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public Enemy? CurrentEnemy
    {
        get
        {
            Enemy enemy;
            if (PlayerCombats.TryGetValue(ActivePlayerId, out enemy))
            {
                return enemy;
            }
            else
            {
                return null;
            }
        }
    }

    private int _turnCounter = 0;

    private int _spawnX;
    private int _spawnY;

    public Model()
    {
        IsGameOver = false;

        var themes = new List<IDungeonTheme> { new MagicalTheme(), new LibraryTheme(), new BossTheme() };
        Theme = themes[new Random().Next(themes.Count)];

        var dummyPlayerForSpawn = new Player("Dummy", 0);
        Map = new Map(20, 40, dummyPlayerForSpawn, Theme);

        _spawnX = dummyPlayerForSpawn.X;
        _spawnY = dummyPlayerForSpawn.Y;
    }

    public void AddNetworkPlayer(int id)
    {
        if (!Players.ContainsKey(id))
        {
            Players[id] = new Player($"Player {id}", id);
            Players[id].X = _spawnX;
            Players[id].Y = _spawnY;
            Players[id].CurrentMap = this.Map;
            Map.Subscribe(Players[id]);
        }
    }

    public void Update()
    {
        _turnCounter++;
        if (_turnCounter % 14 == 0)
        {
            Map.MoveEnemies(PlayerCombats.Values);
        }

    }

    public void SetGameOver()
    {
        IsGameOver = true;
    }

    public bool PickUpItem()
    {
        if (ActivePlayer != null)
        {
            bool picked = ActivePlayer.PickUpItem(Map);
            if (picked)
            {
                Map.Notify(new SoundEvent(ActivePlayer.X, ActivePlayer.Y, 8, "Item Pickup"));
            }
            return picked;
        }
        return false;
    }

    public bool MovePlayer(int dx, int dy)
    {
        if (ActivePlayer != null)
        {
            bool moved = Map.TryMovePlayer(ActivePlayer, dx, dy);

            return moved;
        }
        return false;
    }


    public bool DropItem()
    {
        if (ActivePlayer != null)
        {
            return ActivePlayer.DropItem(Map);
        }
        return false;
    }

    public bool EquipLeft()
    {
        if (ActivePlayer != null)
        {
            return ActivePlayer.LeftHandPickup();
        }
        return false;
    }

    public bool EquipRight()
    {
        if (ActivePlayer != null)
        {
            return ActivePlayer.RightHandPickup();
        }
        return false;
    }

    public bool ChangeSlot(int selected)
    {
        if (ActivePlayer == null) return false;
        ActivePlayer.SelectedSlot = selected;
        return true;
    }

    public bool StartCombat()
    {
        if (ActivePlayer == null) return false;

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
        if (!IsInCombat || CurrentEnemy == null || ActivePlayer == null) return;

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
        if (!IsInCombat || ActivePlayer == null) return;

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

        if (ActivePlayer != null && ActivePlayer.Health <= 0)
        {
            GameLog.Instance.Log($"Player ({ActivePlayer.Name}) Died!");
            Players.Remove(ActivePlayerId);
        }
    }
}