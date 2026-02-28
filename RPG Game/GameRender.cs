namespace RPG_Game;

public class GameRender
{
    public void DrawMap(Map map,int Xsize,int Ysize,Player player)
    {
        
        for (int i = 0; i < Xsize; i++)
        {
            for (int j = 0; j < Ysize; j++)
            {
                if (player.X == i && player.Y == j)
                {
                    Console.Write(player.GetSymbol());
                }
                else
                {
                    Console.Write(map.GetField(i, j).GetSymbol());
                }
            }


            Console.WriteLine();

        }
        Console.WriteLine("inventory");
        for (int i = 0; i < player.Inventory.Count; i++)
        {
            Console.WriteLine(player.Inventory[i]);
        }
        Console.WriteLine(player.X + " " + player.Y);
    }
}