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

Console.WriteLine($"WSAD to move, Space to place/remove. E to reset position. Q to reset canvas\nPress enter to begin.");
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
        case "Spacebar":
            placeBomb();
            break;
        case "E":
            resetPosition();
            break;
        case "Q":
            resetCanvas();
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
    Console.WriteLine("▄▄▄▄▄▄▄▄▄▄▄▄");
    for (int i = 0; i < grid.Length; i++)
    {
        Console.Write("█");
        for (int j = 0; j < grid[i].Length; j++)
        {
            if (grid[i][j] >= charValue)
            {
                Console.Write("G");
            }
            else if (isBombHere(grid[i][j], bigGridCell))
            {
                Console.Write("█");
            }
            else
            {
                Console.Write(" ");
            }
        }
        Console.WriteLine("█");
    }
    Console.WriteLine("▀▀▀▀▀▀▀▀▀▀▀▀");
    Console.WriteLine(zoneTitles[(uint)bigGrid.X][(uint)bigGrid.Y]);
}

void placeBomb()
{
    grid[(int)charGrid.X][(int)charGrid.Y] = grid[(int)charGrid.X][(int)charGrid.Y] ^ (uint)1 << bigGridCell;
}

bool isBombHere(uint gridValue, int cell)
{
    if ((gridValue & (1 << (cell))) != 0)
    {
        return true;
    }
    return false;
}

void resetPosition()
{
    charPos.X = 0;
    charPos.Y = 0;
}

void resetCanvas()
{
    for (int i = 0; i < grid.Length; i++)
    {
        for (int j = 0; j < grid[i].Length; j++)
        {
            if(grid[i][j] >= charValue)
            {
                grid[i][j] = charValue;
            }
            else
            {
                grid[i][j] = 0;
            }
        }
    }
}