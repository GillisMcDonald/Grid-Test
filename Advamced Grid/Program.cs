using System.Numerics;

ConsoleKeyInfo input;
uint charValue = 0b10000000000000000000000000000000;

uint[][] grid =
[
    [charValue,0,0,0,0,0,0,0,0,0],
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

Vector2 bigGrid = new Vector2(0, 0);
Vector2 charPos = new Vector2(0, 0);
Vector2 prevPos;
Vector2 charGrid = new Vector2(0, 0);
Vector2 prevGrid = new Vector2(0, 0);

int bigGridCell = 0;

Console.WriteLine($"WSAD to move, E to place/remove.\nPress enter to begin.");
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
            grid[(int)charGrid.X][(int)charGrid.Y] = placeBomb(grid[(int)charGrid.X][(int)charGrid.Y], bigGridCell);
            break;
    }

    charPos.X = Math.Min(charPos.X, 49);
    charPos.Y = Math.Min(charPos.Y, 49);
    charPos.X = Math.Max(charPos.X, 0);
    charPos.Y = Math.Max(charPos.Y, 0);

    bigGrid.X = (int)charPos.X / 10;
    bigGrid.Y = (int)charPos.Y / 10;

    bigGridCell = ((int)bigGrid.X * 5) + (int)bigGrid.Y;

    prevGrid.X = prevPos.X % 10;
    prevGrid.Y = prevPos.Y % 10;

    charGrid.X = charPos.X % 10;
    charGrid.Y = charPos.Y % 10;

    grid[(int)prevGrid.X][(int)prevGrid.Y] -= charValue;
    grid[(int)charGrid.X][(int)charGrid.Y] += charValue;

    for (int i = 0; i < grid.Length; i++)
    {
        for (int j = 0; j < grid[i].Length; j++)
        {
            if (grid[i][j] >= charValue)
            {
                Console.Write("A");
            }
            else if (isBombHere(grid[i][j], bigGridCell))
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
    Console.WriteLine(zoneTitles[(uint)bigGrid.X][(uint)bigGrid.Y]);
    Console.WriteLine(grid[(int)charGrid.X][(int)charGrid.Y]);
}

uint placeBomb(uint gridValue, int cell)
{
    return gridValue ^ (uint)1 << (cell);
}

bool isBombHere(uint gridValue, int cell)
{
    if ((gridValue & (1 << (cell))) != 0)
    {
        return true;
    }
    return false;
}
