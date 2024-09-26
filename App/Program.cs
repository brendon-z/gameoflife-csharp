public static class Conway
{
    private static int gridWidth;
    private static int gridHeight;
    private static int cellCount = 0;

    public static void Main(string[] args)
    {
        Console.WriteLine("Grid width: ");
        gridWidth = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Grid height: ");
        gridHeight = Convert.ToInt32(Console.ReadLine());

        char[,] grid = new char[gridHeight, gridWidth];

        while (cellCount == 0 || cellCount >= gridWidth * gridHeight)
        {
            Console.WriteLine("Number of starting cells: ");
            cellCount = Convert.ToInt32(Console.ReadLine());
        }
        GenerateSeed(grid, cellCount);
        PrintGrid(grid);

        while (cellCount > 0)
        {
            cellCount = tick(grid, cellCount);
            Thread.Sleep(500);
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
            Tuple<int, int> coord;
            int x;
            int y;
            do {
                x = random.Next(gridWidth - 1);
                y = random.Next(gridHeight - 1);
                coord = new Tuple<int, int>(x, y);
            } while (prevCoords.Contains(coord));
            prevCoords.Add(coord);
            grid[x, y] = 'X';
        }
    }

    public static void PrintGrid(char[,] grid)
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                Console.Write(grid[i, j].ToString() + ' ');
            }
            Console.WriteLine();
        }
    }

    public static int tick(char[,] grid, int cellCount)
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
                
                if (grid[i,j] == 'X' && neighbourCount != 3 && neighbourCount != 2)
                {
                    grid[i,j] = ' ';
                    cellCount--;
                } else if (grid[i,j] == ' ' && neighbourCount == 3)
                {
                    grid[i,j] = 'X';
                    cellCount++;
                }
            }
        }
        PrintGrid(grid);
        Console.WriteLine("Cells alive: " + cellCount);
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
}