using System.Numerics;

int[][] grid =
[
    [10000,0,0,0,0,0,0,0,0,0],
    [0,0,0,0,0,0,0,0,0,0],
    [0,0,0,0,0,0,0,0,0,0],
    [0,0,0,0,0,0,0,0,0,0],
    [0,0,0,0,0,0,0,0,0,0],
    [0,0,0,0,0,0,0,0,0,0],
    [0,0,0,0,0,0,0,0,0,0],
    [0,0,0,0,0,0,0,0,0,0],
    [0,0,0,0,0,0,0,0,0,0],
    [0,0,0,0,0,0,0,0,0,0],
];

string[][] zoneTitles =
[
    ["Staring Zone","Zone to the Right","New York","Maine","Atlantic Ocean"],
    ["The Down Zone","Mole Tunnel","Sewers","Minecraft Cave","Flooded Ravine"],
    ["Deep Cave","Flooded Cave","Mario Water Level","Beach","Ocean Floor"],
    ["Deeper Cave","Super Alloy Cave","Hades","Abandoned Mine","Hard Rock"],
    ["Hell","Hell 2","Elesium","Styx","Core"],
];

Vector2 encode = new Vector2(0, 0);
Vector2 bigGrid = new Vector2(0, 0);
Vector2 charPos = new Vector2(0, 0);
Vector2 prevPos;
Vector2 charGrid = new Vector2(0, 0);
Vector2 prevGrid = new Vector2(0, 0);
ConsoleKeyInfo input;

int key1 = 3;
int key2 = 77;

Console.WriteLine("WSAD to move, E to drop an item.");
Console.WriteLine("Press enter to begin.");
while (true)
{
    prevPos = charPos;

    input = Console.ReadKey();
    Console.Clear();

    switch (input.Key.ToString())
    {
        case "W":
            charPos.X--;
            break;
        case "S":
            charPos.X++;
            break;
        case "A":
            charPos.Y--;
            break;
        case "D":
            charPos.Y++;
            break;
        case "E":
            if (grid[(int)charGrid.X][(int)charGrid.Y] == 10000)
            {
                grid[(int)charGrid.X][(int)charGrid.Y] += (int)encode.X + (int)encode.Y;
            }
            break;
    }

    if (charPos.X == -1)
        charPos.X = 0;
    if (charPos.Y == -1)
        charPos.Y = 0;
    if (charPos.X == 50)
        charPos.X = 49;
    if (charPos.Y == 50)
        charPos.Y = 49;

    bigGrid.X = (int)charPos.X / 10;
    bigGrid.Y = (int)charPos.Y / 10;

    encode.X = (bigGrid.X + 1) * key1;
    encode.Y = (bigGrid.Y + 1) * key2;

    prevGrid.X = prevPos.X % 10;
    prevGrid.Y = prevPos.Y % 10;

    charGrid.X = charPos.X % 10;
    charGrid.Y = charPos.Y % 10;

    grid[(int)charGrid.X][(int)charGrid.Y] += 10000;
    grid[(int)prevGrid.X][(int)prevGrid.Y] -= 10000;

    for (int i = 0; i < grid.Length; i++)
    {
        for (int j = 0; j < grid[i].Length; j++)
        {
            if (grid[i][j] > 9999)
            {
                Console.Write("X");
            }
            else if (grid[i][j] - encode.X - encode.Y == 0)
            {
                Console.Write("+");
            }
            else
            {
                Console.Write("_");
            }
        }
        Console.WriteLine();
    }
    Console.WriteLine(zoneTitles[(int)bigGrid.X][(int)bigGrid.Y]);
}
