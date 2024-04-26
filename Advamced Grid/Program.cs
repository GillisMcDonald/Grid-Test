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

Vector2 bigGridPos = new Vector2(0, 0);
Vector2 charWorldPos = new Vector2(0, 0);
Vector2 prevWorldPos = new Vector2(0, 0);
Vector2 charGridPos = new Vector2(0, 0);
Vector2 prevGridPos = new Vector2(0, 0);

int bigGridCell = 0;

Console.WriteLine($"WSAD to move, Space to place/remove. E to reset position. Q to reset canvas\nPress enter to begin.");
while (true)
{
    input = Console.ReadKey();
    switch (input.Key.ToString())
    {
        case "W":
            moveUp();
            break;
        case "S":
            moveDown();
            break;
        case "A":
            moveLeft();
            break;
        case "D":
            moveRight();
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
    doMathStuff();
    drawGrid();
}

void moveUp()
{
    charWorldPos.X--;
    charWorldPos.X = Math.Max(charWorldPos.X, 0);
}

void moveDown()
{
    charWorldPos.X++;
    charWorldPos.X = Math.Min(charWorldPos.X, 49);
}

void moveLeft()
{
    charWorldPos.Y--;
    charWorldPos.Y = Math.Max(charWorldPos.Y, 0);
}

void moveRight()
{
    charWorldPos.Y++;
    charWorldPos.Y = Math.Min(charWorldPos.Y, 49);
}

void doMathStuff()
{
    bigGridPos.X = (int)charWorldPos.X / 10;
    bigGridPos.Y = (int)charWorldPos.Y / 10;
    bigGridCell = ((int)bigGridPos.X * 5) + (int)bigGridPos.Y;
    prevGridPos.X = prevWorldPos.X % 10;
    prevGridPos.Y = prevWorldPos.Y % 10;
    charGridPos.X = charWorldPos.X % 10;
    charGridPos.Y = charWorldPos.Y % 10;
    grid[(int)prevGridPos.X][(int)prevGridPos.Y] -= charValue;
    grid[(int)charGridPos.X][(int)charGridPos.Y] += charValue;
    prevWorldPos = charWorldPos;
}

void drawGrid()
{
    Console.Clear();
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
    Console.WriteLine(zoneTitles[(uint)bigGridPos.X][(uint)bigGridPos.Y]);
}

void placeBomb()
{
    grid[(int)charGridPos.X][(int)charGridPos.Y] = grid[(int)charGridPos.X][(int)charGridPos.Y] ^ (uint)1 << bigGridCell;
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
    charWorldPos.X = 0;
    charWorldPos.Y = 0;
}

void resetCanvas()
{
    for (int i = 0; i < grid.Length; i++)
    {
        for (int j = 0; j < grid[i].Length; j++)
        {
            if (grid[i][j] >= charValue)
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