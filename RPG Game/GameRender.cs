namespace RPG_Game;

public class GameRender
{
    public void Info(int width,Player player)
    {
        Console.SetCursorPosition(width, 0);
        Console.WriteLine("Inventory:");
        for (int i = 0; i < 3; i++)
        {
            Console.SetCursorPosition(width, i+1);
            if (player.Inventory.Count() > i)
            {
                Console.WriteLine(" - " + player.Inventory[i]);
            }
            else
            {
                Console.WriteLine(" - ");
            }
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
}