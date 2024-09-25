using System.Security.Cryptography.X509Certificates;

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

        PrintArray(grid);
    }


    public static void GenerateSeed(char[,] grid, int quantity)
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                grid[i,j] = 'O';
            }
            Console.WriteLine();
        }

        Random random = new();

        for (int i = 0; i < quantity; i++)
        {
            int x = random.Next(gridWidth - 1);
            int y = random.Next(gridHeight - 1);
            Console.WriteLine(x + " " + y);
            grid[x, y] = 'X';
        }
    }

    public static void PrintArray(char[,] grid)
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                Console.Write(grid[i,j].ToString() + '\t');
            }
            Console.WriteLine();
        }
    }
}