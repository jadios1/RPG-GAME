namespace RPG_Game;

public class Game
{
    private Map map;
    private Player player;
    private GameRender gameRender;

    public void Run()
    {
        while (true)
        {
            Console.SetCursorPosition(0, 0);
            gameRender.DrawMap(map,map.Width,map.Height,player);
            var pressedkey = Console.ReadKey().Key;
            if (pressedkey == ConsoleKey.W)
            {
                map.TryMovePlayer(player,-1,0);
            }
            else if (pressedkey == ConsoleKey.S)
            {
                map.TryMovePlayer(player,1,0);
            }
            else if (pressedkey == ConsoleKey.A)
            {
                map.TryMovePlayer(player,0,-1);
            }
            else if (pressedkey == ConsoleKey.D)
            {
                map.TryMovePlayer(player,0,1);
            }
        }
    }

    public Game()
    {
        map = new Map(40,20);
        player = new Player();
        gameRender = new GameRender();
    }
}