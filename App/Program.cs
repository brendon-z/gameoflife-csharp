public static class Conway
{
    private static int gridWidth;
    private static int gridHeight;

    private static int delayTime;
    private static int cellCount = 0;

    public static void Main(string[] args)
    {
        Console.Write("Grid width: ");
        gridWidth = Convert.ToInt32(Console.ReadLine());

        Console.Write("Grid height: ");
        gridHeight = Convert.ToInt32(Console.ReadLine());

        char[,] grid = new char[gridHeight, gridWidth];

        while (cellCount == 0 || cellCount >= gridWidth * gridHeight)
        {
            Console.Write("Number of starting cells: ");
            cellCount = Convert.ToInt32(Console.ReadLine());
        }

        Console.Write("Tick duration (ms): ");
        delayTime = Convert.ToInt32(Console.ReadLine());

        GenerateSeed(grid, cellCount);
        PrintGrid(grid);

        int generation = 1;
        while (cellCount > 0)
        {
            cellCount = Tick(grid, cellCount);
            Console.WriteLine("Cells alive: " + cellCount);
            Console.WriteLine("Generation: " + generation);
            generation += 1;
            Thread.Sleep(delayTime);
        }

        Console.WriteLine("Done!");
    }


    public static void GenerateSeed(char[,] grid, int quantity)
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                grid[i, j] = ' ';
            }
        }

        Random random = new();
        List<Tuple<int, int>> prevCoords = new();
        for (int i = 0; i < quantity; i++)
        {
            double meanX = gridWidth / 2.0;
            double meanY = gridHeight / 2.0;
            double stdDev = Math.Min(gridWidth, gridHeight) / 8.0;

            Tuple<int, int> coord;
            int x;
            int y;
            do
            {
                x = Clamp((int)Math.Round(NextGaussian(random, meanX, stdDev)), 0, gridWidth - 1);
                y = Clamp((int)Math.Round(NextGaussian(random, meanY, stdDev)), 0, gridHeight - 1);
                coord = new Tuple<int, int>(x, y);
            } while (prevCoords.Contains(coord));
            prevCoords.Add(coord);
            grid[x, y] = '■';
        }
    }

    public static void PrintGrid(char[,] grid)
    {
        Console.SetCursorPosition(0, 0);
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                Console.Write(grid[i, j].ToString() + ' ');
            }
            Console.WriteLine();
        }
    }

    public static int Tick(char[,] grid, int cellCount)
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                string neighbours = "";
                if (isValid(i - 1, j - 1, gridHeight, gridWidth))
                { neighbours += grid[i - 1, j - 1]; }
                if (isValid(i - 1, j, gridHeight, gridWidth))
                { neighbours += grid[i - 1, j]; }
                if (isValid(i - 1, j + 1, gridHeight, gridWidth))
                { neighbours += grid[i - 1, j + 1]; }
                if (isValid(i, j - 1, gridHeight, gridWidth))
                { neighbours += grid[i, j - 1]; }
                if (isValid(i, j + 1, gridHeight, gridWidth))
                { neighbours += grid[i, j + 1]; }
                if (isValid(i + 1, j - 1, gridHeight, gridWidth))
                { neighbours += grid[i + 1, j - 1]; }
                if (isValid(i + 1, j, gridHeight, gridWidth))
                { neighbours += grid[i + 1, j]; }
                if (isValid(i + 1, j + 1, gridHeight, gridWidth))
                { neighbours += grid[i + 1, j + 1]; }

                int neighbourCount = neighbours.Replace(" ", "").Length;

                if (grid[i, j] == '■' && neighbourCount != 3 && neighbourCount != 2)
                {
                    grid[i, j] = ' ';
                    cellCount--;
                }
                else if (grid[i, j] == ' ' && neighbourCount == 3)
                {
                    grid[i, j] = '■';
                    cellCount++;
                }
            }
        }
        PrintGrid(grid);
        return cellCount;
    }

    public static bool isValid(int i, int j, int iMax, int jMax)
    {
        if (i < 0 || j < 0 || i > iMax - 1 || j > jMax - 1)
        {
            return false;
        }
        return true;
    }
    
    // Gaussian distribution using Box-Muller transformation
    private static double NextGaussian(Random rng, double mean, double stdDev)
    {
        double u1 = 1.0 - rng.NextDouble();
        double u2 = 1.0 - rng.NextDouble();
        double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                            Math.Sin(2.0 * Math.PI * u2);
        return mean + stdDev * randStdNormal;
    }

    // Clamp to grid bounds
    private static int Clamp(int value, int min, int max)
    {
        return Math.Max(min, Math.Min(max, value));
    }

}