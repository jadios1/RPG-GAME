using System;
using System.Collections.Generic;
using System.Linq;
using RPG_Game.Logger;

namespace RPG_Game;

public class GameRender
{
    public void Info(int width, Player player, Map map)
    {
        Console.ResetColor();
        Console.SetCursorPosition(width + 1, 0);
        Console.WriteLine("Inventory:");
        for (int i = 0; i < 3; i++)
        {
            Console.SetCursorPosition(width + 1, i + 1);
            if (player.Inventory.Count > i)
            {
                Console.Write(" - " + player.Inventory[i].GetName());
                if (player.SelectedSlot == i)
                {
                    Console.Write(" >".PadRight(35));
                }
                else
                {
                    Console.Write("".PadRight(35));
                }
            }
            else
            {
                Console.Write(" - ".PadRight(35));
            }
        }
        
        if (map.GetField(player.X, player.Y).IsEmpty() == false)
        {
            Console.SetCursorPosition(width + 1, 5);
            Console.WriteLine("Currently standing on:");
            Console.SetCursorPosition(width + 1, 6);
            
            string itemName = map.GetField(player.X, player.Y).GetItem()?.GetName() ?? "";
            Console.WriteLine(itemName.PadRight(20));
            
            Console.SetCursorPosition(width + 1, 7);
            string itemDesc = map.GetField(player.X, player.Y).GetItem()?.Description() ?? "";
            Console.WriteLine(itemDesc.PadRight(20));
        }
        else
        {
            Console.SetCursorPosition(width + 1, 5);
            Console.WriteLine("Currently standing on:".PadRight(30));
            Console.SetCursorPosition(width + 1, 6);
            Console.WriteLine("".PadRight(30));
            Console.SetCursorPosition(width + 1, 7);
            Console.WriteLine("".PadRight(30));
        }
        
        Console.SetCursorPosition(width + 1, 10);
        Console.Write("Left hand: ");
        
        string leftHand = player.LeftHand.HeldItem?.GetName() ?? "";
        if (!player.LeftHand.IsEmpty()) Console.Write(leftHand.PadRight(35));
        else Console.Write("".PadRight(35));
        
        Console.SetCursorPosition(width + 1, 11);
        Console.Write("Right hand: ");
        
        string rightHand = player.RightHand.HeldItem?.GetName() ?? "";
        if (!player.RightHand.IsEmpty()) Console.Write(rightHand.PadRight(35));
        else Console.Write("".PadRight(35));

        Console.SetCursorPosition(width + 1, 13);
        Console.WriteLine("Player stats:");
        
        Console.SetCursorPosition(width + 1, 14);
        Console.WriteLine("Gold: " + player.Gold + "".PadRight(4));
        
        Console.SetCursorPosition(width + 1, 15);
        Console.WriteLine("Coins: " + player.Coins + "".PadRight(4));
        
        Console.SetCursorPosition(width + 1, 16);
        Console.WriteLine("Health: " + player.Health + "".PadRight(5));
        
        Console.SetCursorPosition(width + 1, 17);
        Console.WriteLine("Dexterity: " + player.Dexterity + "".PadRight(4));
        
        Console.SetCursorPosition(width + 1, 18);
        Console.WriteLine("Aggresion: " + player.Aggression + "".PadRight(4));
        
        Console.SetCursorPosition(width + 1, 19);
        Console.WriteLine("Luck: " + player.Luck + "".PadRight(3));
        
        Console.SetCursorPosition(width + 1, 20);
        Console.WriteLine("Strength: " + player.Strength + "".PadRight(4));
        
        Console.SetCursorPosition(width + 1, 21);
        Console.WriteLine("Wisdom: " + player.Wisdom + "".PadRight(4));
        Console.SetCursorPosition(0, map.Height + 1);

        Console.WriteLine("W/A/S/D - move");
        if (!map.GetField(player.X, player.Y).IsEmpty())
        {
            Console.WriteLine("E - pick up");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("E - pick up");
            Console.ResetColor();
        }
        
        if (player.SelectedItem() != null)
        {
            Console.WriteLine("R - drop");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("R - drop");
            Console.ResetColor();
        }

        if (player.SelectedItem() != null && player.SelectedItem()!.IsEquipable())
        {
            Console.WriteLine("Z - equip left");
        }
        else if (!player.LeftHand.IsEmpty())
        {
            Console.WriteLine("Z - equip left");
        }
        else
        {            
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Z - equip left");
            Console.ResetColor();
        }

        if (player.SelectedItem() != null && player.SelectedItem()!.IsEquipable())
        {
            Console.WriteLine("X - equip right");
        }
        else if (!player.RightHand.IsEmpty())
        {
            Console.WriteLine("X - equip right");
        }
        else
        {            
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("X - equip right");
            Console.ResetColor();
        }
        
        var adjacentEnemy = map.GetAdjacentEnemy(player.X, player.Y);
        if (adjacentEnemy != null)
        {
            Console.WriteLine("F - fight");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("F - fight");
            Console.ResetColor();
        }
        Console.WriteLine("1/2/3 - select slot");
        DrawLog();
    }
    
    public void CannotUse(Map map, bool work)
    {
        if (work)
        {
            Console.SetCursorPosition(0, map.Height);
            Console.WriteLine("Cannot use that key!");   
        }
        else
        {
            Console.SetCursorPosition(0, map.Height);
            Console.WriteLine("".PadRight(25));   
        }
    }

    public void DrawMap(Map map, int height, int width, Dictionary<int, Player> players)
    {
        Console.SetCursorPosition(0, 0);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var playerOnTile = players.Values.FirstOrDefault(p => p.X == x && p.Y == y);
            
                if (playerOnTile != null)
                {
                    if (playerOnTile.Id == 0) Console.ForegroundColor = ConsoleColor.Green;
                    else Console.ForegroundColor = ConsoleColor.Blue;
                    
                    Console.Write(playerOnTile.GetSymbol());
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(map.GetField(x, y).GetSymbol());
                }
            }
            Console.WriteLine();
        }
    }

    public void DrawServerUI(Model model)
    {
        Console.SetCursorPosition(model.Map.Width + 1, 0);
        Console.WriteLine(" SERVER MONITOR ".PadRight(30));
        Console.SetCursorPosition(model.Map.Width + 1, 1);
        Console.WriteLine($"Connected Players: {model.Players.Count}/9".PadRight(30));

        int yOffset = 3;
        foreach (var p in model.Players.Values)
        {
            Console.SetCursorPosition(model.Map.Width + 1, yOffset++);
            string combatStatus = model.PlayerCombats.ContainsKey(p.Id) ? "FIGHTING" : "IDLE";
            Console.WriteLine($"P{p.Id}: {p.Name} | HP: {p.Health} | Status: {combatStatus}".PadRight(45));
        }

        Console.SetCursorPosition(41, 23);
        Console.WriteLine("Recent Logs:".PadRight(50));
        var recentLogs = GameLog.Instance.GetRecent(3);
        for (int i = 0; i < 3; i++)
        {
            Console.SetCursorPosition(41, i + 24);
            string logLine = i < recentLogs.Count ? recentLogs[i] : "";
            Console.WriteLine(logLine.PadRight(60));
        }
    }

    public void DrawCombatInterface(string playerName, int playerHp, string enemyName, int enemyHp, int enemyArmor, int enemyAttack, int width, List<string> recentLogs)
    {
        for (int i = 0; i < 22; i++)
        {
            Console.SetCursorPosition(width + 1, i);
            Console.Write("".PadRight(35));
        }

        Console.SetCursorPosition(width + 1, 0);
        Console.WriteLine("=== COMBAT ===");
        
        Console.SetCursorPosition(width + 1, 1);
        Console.WriteLine($"Enemy: {enemyName}".PadRight(35));
        
        Console.SetCursorPosition(width + 1, 2);
        Console.WriteLine($"HP: {enemyHp}  Armor: {enemyArmor}".PadRight(35));
        
        Console.SetCursorPosition(width + 1, 3);
        Console.WriteLine($"Attack: {enemyAttack}".PadRight(35));
        
        Console.SetCursorPosition(width + 1, 5);
        Console.WriteLine($"Your HP: {playerHp}".PadRight(35));
        
        Console.SetCursorPosition(width + 1, 7);
        Console.WriteLine("1 - Normal attack".PadRight(35));
        Console.SetCursorPosition(width + 1, 8);
        Console.WriteLine("2 - Stealth attack".PadRight(35));
        Console.SetCursorPosition(width + 1, 9);
        Console.WriteLine("3 - Magical attack".PadRight(35));
        Console.SetCursorPosition(width + 1, 10);
        Console.WriteLine("F - Flee".PadRight(35)); 

        Console.SetCursorPosition(41, 23);
        Console.WriteLine("Recent Logs:".PadRight(50));
        for (int i = 0; i < 3; i++)
        {
            Console.SetCursorPosition(41, i + 24);
            string logLine = i < recentLogs.Count ? recentLogs[i] : "";
            Console.WriteLine(logLine.PadRight(60));
        }
    }
    public void DrawLog()
    {
        var recentLogs = GameLog.Instance.GetRecent(3);
        Console.SetCursorPosition(41, 23);
        Console.WriteLine("Recent Logs:".PadRight(50));
        for (int i = 0; i < 3; i++)
        {
            Console.SetCursorPosition(41, i + 24);
            string logText = i < recentLogs.Count ? recentLogs[i] : "";
            Console.WriteLine(logText.PadRight(60));
        }
    }

    public bool DrawFullLog()
    {
        Console.Clear();
        var fullLog = GameLog.Instance.GetAll();
        foreach (var log in fullLog)
            Console.WriteLine(log);
        Console.WriteLine("\nPress any key to return...");
        Console.ReadKey(true);
        Console.Clear();
        return true;
    }

    public void DrawGameOver()
    {
        Console.Clear();
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("GAME OVER");
        Console.WriteLine(GameLog.Instance.FilePath);
        Console.ReadKey(true);
        Environment.Exit(0);
    }
    
    
public void DrawNetworkState(GameStateDTO state, int localPlayerId)
    {
        Console.SetCursorPosition(0, 0);

        for (int y = 0; y < state.MapGrid.Length; y++)
        {
            Console.WriteLine(state.MapGrid[y]);
        }

        foreach (var enemy in state.Enemies)
        {
            Console.SetCursorPosition(enemy.X, enemy.Y);
            Console.Write(enemy.Symbol);
        }

        foreach (var playerKvp in state.Players)
        {
            var p = playerKvp.Value;
            Console.SetCursorPosition(p.X, p.Y);
            if (p.Id == localPlayerId) Console.ForegroundColor = ConsoleColor.Green;
            else Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(p.Symbol);
            Console.ResetColor();
        }

        int width = state.MapGrid[0].Length;
        
        if (state.Players.TryGetValue(localPlayerId, out var player))
        {
            Console.ResetColor();
            Console.SetCursorPosition(width + 1, 0);
            Console.WriteLine("Inventory:");
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(width + 1, i + 1);
                if (player.InventoryNames.Count > i)
                {
                    Console.Write(" - " + player.InventoryNames[i]);
                    if (player.SelectedSlot == i) Console.Write(" >".PadRight(35));
                    else Console.Write("".PadRight(35));
                }
                else
                {
                    Console.Write(" - ".PadRight(35));
                }
            }
            
            if (player.IsStandingOnItem)
            {
                Console.SetCursorPosition(width + 1, 5);
                Console.WriteLine("Currently standing on:");
                Console.SetCursorPosition(width + 1, 6);
                Console.WriteLine(player.StandingOnItemName.PadRight(20));
                Console.SetCursorPosition(width + 1, 7);
                Console.WriteLine(player.StandingOnItemDesc.PadRight(20));
            }
            else
            {
                Console.SetCursorPosition(width + 1, 5);
                Console.WriteLine("Currently standing on:".PadRight(30));
                Console.SetCursorPosition(width + 1, 6);
                Console.WriteLine("".PadRight(30));
                Console.SetCursorPosition(width + 1, 7);
                Console.WriteLine("".PadRight(30));
            }
            
            Console.SetCursorPosition(width + 1, 10);
            Console.Write("Left hand: ");
            if (!string.IsNullOrEmpty(player.LeftHandName)) Console.Write(player.LeftHandName.PadRight(35));
            else Console.Write("".PadRight(35));
            
            Console.SetCursorPosition(width + 1, 11);
            Console.Write("Right hand: ");
            if (!string.IsNullOrEmpty(player.RightHandName)) Console.Write(player.RightHandName.PadRight(35));
            else Console.Write("".PadRight(35));

            Console.SetCursorPosition(width + 1, 13);
            Console.WriteLine("Player stats:");
            
            Console.SetCursorPosition(width + 1, 14);
            Console.WriteLine("Gold: " + player.Gold + "".PadRight(4));
            Console.SetCursorPosition(width + 1, 15);
            Console.WriteLine("Coins: " + player.Coins + "".PadRight(4));
            Console.SetCursorPosition(width + 1, 16);
            Console.WriteLine("Health: " + player.Health + "".PadRight(5));
            Console.SetCursorPosition(width + 1, 17);
            Console.WriteLine("Dexterity: " + player.Dexterity + "".PadRight(4));
            Console.SetCursorPosition(width + 1, 18);
            Console.WriteLine("Aggresion: " + player.Aggression + "".PadRight(4));
            Console.SetCursorPosition(width + 1, 19);
            Console.WriteLine("Luck: " + player.Luck + "".PadRight(3));
            Console.SetCursorPosition(width + 1, 20);
            Console.WriteLine("Strength: " + player.Strength + "".PadRight(4));
            Console.SetCursorPosition(width + 1, 21);
            Console.WriteLine("Wisdom: " + player.Wisdom + "".PadRight(4));
            
            Console.SetCursorPosition(0, state.MapGrid.Length + 1);

            Console.WriteLine("W/A/S/D - move");
            
            if (player.IsStandingOnItem) Console.WriteLine("E - pick up");
            else { Console.ForegroundColor = ConsoleColor.DarkGray; Console.WriteLine("E - pick up"); Console.ResetColor(); }
            
            if (player.HasSelectedItem) Console.WriteLine("R - drop");
            else { Console.ForegroundColor = ConsoleColor.DarkGray; Console.WriteLine("R - drop"); Console.ResetColor(); }

            if (player.CanEquipLeft) Console.WriteLine("Z - equip left");
            else { Console.ForegroundColor = ConsoleColor.DarkGray; Console.WriteLine("Z - equip left"); Console.ResetColor(); }

            if (player.CanEquipRight) Console.WriteLine("X - equip right");
            else { Console.ForegroundColor = ConsoleColor.DarkGray; Console.WriteLine("X - equip right"); Console.ResetColor(); }
            
            if (player.CanFight) Console.WriteLine("F - fight");
            else { Console.ForegroundColor = ConsoleColor.DarkGray; Console.WriteLine("F - fight"); Console.ResetColor(); }
            
            Console.WriteLine("1/2/3 - select slot");
        }

        Console.SetCursorPosition(41, 23);
        Console.WriteLine("Recent Logs:".PadRight(50));
        for (int i = 0; i < 3; i++)
        {
            Console.SetCursorPosition(41, i + 24);
            string logLine = i < state.RecentLogs.Count ? state.RecentLogs[i] : "";
            Console.WriteLine(logLine.PadRight(60));
        }
    }
    
}