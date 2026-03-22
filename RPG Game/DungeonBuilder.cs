namespace RPG_Game;

public class DungeonBuilder
{
    private Map map;

    public DungeonBuilder(Map map_)
    {
        map = map_;
    }
    
    public DungeonBuilder GenerateEmpty()
    {
        for (int y = 0; y < map.Height; y++)
        {
            for (int x = 0; x < map.Width; x++)
            {
                map.SetField(x,y,new EmptyField());
                if (y == 0 || x == 0 || y == map.Height-1 || x == map.Width-1)
                {
                    map.PlaceWall(x,y);
                }

            }
        }

        return this;
    }
    

    public DungeonBuilder GenerateCentralRoom(int size)
    {
        int roomHeight = size;
        int roomWidht = roomHeight*2;
        for (int y = map.Height/2 - (roomHeight/2) ; y < map.Height/2 + (roomHeight/2); y++)
        {
            for (int x = map.Width/2 - (roomWidht/2); x < map.Width/2 + (roomWidht/2); x++)
            {
                map.SetField(x,y,new EmptyField());
            }
        }

        return this;
    }
    
    public DungeonBuilder GenerateFilled()
    {
        for (int y = 0; y < map.Height; y++)
        {
            for (int x = 0; x < map.Width; x++)
            {
                map.PlaceWall(x,y);
            }
        }

        return this;
    }


    public DungeonBuilder GeneratePath(int length)
    {
        Random rnd = new Random();
        int randomx = rnd.Next(1,map.Width-1); 
        int randomy = rnd.Next(1,map.Height-1);

        int steps = rnd.Next(1,(length/2));
        int takensteps = 0;
        int direction = 0;
        while (takensteps < length)
        {
            direction = rnd.Next(4);
            
            if (length - takensteps <= 1) break;
            steps = rnd.Next(1,(length - takensteps) + 1);
            
            for (int i = 0; i < steps; i++)
            {
                if (direction == 0)
                {
                    if (randomx >= map.Width-2 || randomx <= 1)
                    {
                        takensteps++;
                        break;

                    }
                    else
                    {
                        randomx++;    
                    }
                    
                }
                else if (direction == 1)
                {
                    if (randomx >= map.Width-2 || randomx <= 1)
                    {
                        takensteps++;
                        break;

                    }
                    else
                    {
                        randomx--;
                    }
                }
                else if(direction == 2)
                {
                    if (randomy >= map.Height-2 || randomy <= 1)
                    {
                        takensteps++;
                        break;

                    }
                    else
                    {
                        randomy++;
                    }
                    
                }
                else if (direction == 3)
                {
                    if (randomy >= map.Height-2 || randomy <= 1)
                    {
                        takensteps++;
                        break;
                    }
                    else
                    {
                        randomy--;
                    }
                }

                takensteps++;
                map.SetField(randomx,randomy,new EmptyField());
            }


        }

        return this;
    }
}