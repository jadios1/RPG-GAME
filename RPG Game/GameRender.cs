using RPG_Game.Logger;

namespace RPG_Game;

public class GameRender
{
    public void Info(int width,Player player,Map map)
    {
        Console.ResetColor();
        Console.SetCursorPosition(width+1, 0);
        Console.WriteLine("Inventory:");
        for (int i = 0; i < 3; i++)
        {
            Console.SetCursorPosition(width+1, i+1);
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
        if (map.GetField(player.X,player.Y).IsEmpty()==false)
        {
            Console.SetCursorPosition(width+1, 5);

            Console.WriteLine("Currently standing on:");
            Console.SetCursorPosition(width+1, 6);
            Console.WriteLine(map.GetField(player.X,player.Y).GetItem()?.GetName().PadRight(20));
            Console.SetCursorPosition(width+1, 7);

            Console.WriteLine(map.GetField(player.X, player.Y).GetItem()?.Description().PadRight(20));
        }
        else
        {
            Console.SetCursorPosition(width+1, 5);

            Console.WriteLine("Currently standing on:".PadRight(30));
            Console.SetCursorPosition(width+1, 6);

            Console.WriteLine("".PadRight(30));
            Console.SetCursorPosition(width+1, 7);
            Console.WriteLine("".PadRight(30));

        }
        
        Console.SetCursorPosition(width+1, 10);
        Console.Write("Left hand: ");
        
        if(!player.LeftHand.IsEmpty()) Console.Write(player.LeftHand.HeldItem?.GetName().PadRight(35));
        else Console.Write("".PadRight(35));
        
        Console.SetCursorPosition(width+1, 11);
        Console.Write("Right hand: ");
        
        if(!player.RightHand.IsEmpty())Console.Write(player.RightHand.HeldItem?.GetName().PadRight(35));
        else Console.Write("".PadRight(35));

        Console.SetCursorPosition(width+1, 13);
        Console.WriteLine("Player stats:");
        
        Console.SetCursorPosition(width+1, 14);
        Console.WriteLine("Gold: " + player.Gold + "".PadRight(4));
        
        Console.SetCursorPosition(width+1, 15);
        Console.WriteLine("Coins: " + player.Coins + "".PadRight(4));
        
        Console.SetCursorPosition(width+1, 16);
        Console.WriteLine("Health: " + player.Health + "".PadRight(5));
        
        Console.SetCursorPosition(width+1, 17);
        Console.WriteLine("Dexterity: " + player.Dexterity + "".PadRight(4)) ;
        
        Console.SetCursorPosition(width+1, 18);
        Console.WriteLine("Aggresion: " + player.Aggression + "".PadRight(4));

        
        Console.SetCursorPosition(width+1, 19);
        Console.WriteLine("Luck: " + player.Luck + "".PadRight(3));
        
        Console.SetCursorPosition(width+1, 20);
        Console.WriteLine("Strength: " + player.Strength + "".PadRight(4));
        
        Console.SetCursorPosition(width+1, 21);
        Console.WriteLine("Wisdom: " + player.Wisdom + "".PadRight(4));
        Console.SetCursorPosition(0, map.Height+1);

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
        

        if (player.SelectedItem() !=null)
        {
            Console.WriteLine("R - drop");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;

            Console.WriteLine("R - drop");
            Console.ResetColor();
        }

        if (player.SelectedItem()!=null && player.SelectedItem()!.IsEquipable())
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

        if (player.SelectedItem()!=null && player.SelectedItem()!.IsEquipable())
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
    
    
    public void CannotUse(Map map,bool work)
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
    public void DrawMap(Map map,int height,int width,Player player)
    {
        Console.SetCursorPosition(0, 0);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (player.X == x && player.Y == y)
                {
                    Console.Write(player.GetSymbol());
                }
                else
                {
                    Console.Write(map.GetField(x, y).GetSymbol());
                }
            }
            Console.WriteLine();
        }
    }
    
    
    public void DrawCombat(Player player, Enemy enemy, int width)
    {
        DrawLog();
        Console.SetCursorPosition(width+1, 0);
        Console.WriteLine("=== COMBAT ===");
        Console.SetCursorPosition(width+1, 1);
        Console.WriteLine($"Enemy: {enemy.Name}".PadRight(10));
        Console.SetCursorPosition(width+1, 2);
        Console.WriteLine($"HP: {enemy.Health}  Armor: {enemy.Armor}".PadRight(25));
        Console.SetCursorPosition(width+1, 3);
        Console.WriteLine($"Attack: {enemy.Attack}".PadRight(20));
        Console.SetCursorPosition(width+1, 5);
        Console.WriteLine($"Your HP: {player.Health}".PadRight(25));
        Console.SetCursorPosition(width+1, 7);
        Console.WriteLine("1 - Normal attack");
        Console.SetCursorPosition(width+1, 8);
        Console.WriteLine("2 - Stealth attack");
        Console.SetCursorPosition(width+1, 9);
        Console.WriteLine("3 - Magical attack");

    }

    public void DrawLog()
    {
        var recentLogs = GameLog.Instance.GetRecent(3);
        Console.SetCursorPosition(41, 21);
        Console.WriteLine("Recent Logs:".PadRight(50));
        for (int i = 0; i < 3; i++)
        {
            Console.SetCursorPosition(41, i + 22);
            Console.WriteLine(i < recentLogs.Count ? recentLogs[i].PadRight(50) : "".PadRight(60));
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
}
