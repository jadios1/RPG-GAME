namespace RPG_Game;

public class GameRender
{
    public void Info(int width,Player player,Map map)
    {
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
                    Console.Write(" >".PadRight(40));
                }
                else
                {
                    Console.Write("".PadRight(40));
                }
            }
            else
            {
                Console.Write(" - ".PadRight(50));
            }
        }
        Console.SetCursorPosition(width+1, 5);
        if (map.GetField(player.X,player.Y).IsEmpty()==false)
        {
            Console.WriteLine("Currently standing on:" + map.GetField(player.X,player.Y).GetItem().Description().PadRight(50));
        }
        else
        {
            Console.WriteLine("Currently standing on:".PadRight(60));
        }
        
        Console.SetCursorPosition(width+1, 7);
        Console.Write("Left hand: ");
        
        if(!player.LeftHand.IsEmpty()) Console.Write(player.LeftHand.HeldItem?.GetName().PadRight(40));
        else Console.Write("".PadRight(40));
        
        Console.SetCursorPosition(width+1, 8);
        Console.Write("Right hand: ");
        
        if(!player.RightHand.IsEmpty())Console.Write(player.RightHand.HeldItem?.GetName().PadRight(40));
        else Console.Write("".PadRight(40));

        Console.SetCursorPosition(width+1, 10);
        Console.WriteLine("Player stats:");
        
        Console.SetCursorPosition(width+1, 11);
        Console.WriteLine("Gold: " + player.Gold);
        
        Console.SetCursorPosition(width+1, 12);
        Console.WriteLine("Coins: " + player.Coins);
        
        Console.SetCursorPosition(width+1, 13);
        Console.WriteLine("Health: " + player.Health);
        
        Console.SetCursorPosition(width+1, 14);
        Console.WriteLine("Dexterity: " + player.Dexterity);
        
        Console.SetCursorPosition(width+1, 15);
        Console.WriteLine("Aggresion: " + player.Aggression);

        
        Console.SetCursorPosition(width+1, 16);
        Console.WriteLine("Luck: " + player.Luck);
        
        Console.SetCursorPosition(width+1, 17);
        Console.WriteLine("Strength: " + player.Strength);
        
        Console.SetCursorPosition(width+1, 18);
        Console.WriteLine("Wisdom: " + player.Wisdom);



        
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
}