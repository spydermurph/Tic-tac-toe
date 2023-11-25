// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

//Console.WriteLine("─ │ ┌ ┐ └ ┘ ├ ┤ ┬ ┴ ┼ ");

//var board = new string[3, 3];
string[,] board = null!;
ResetBoard(ref board);
string[,] instructions = { { "1", "2", "3" }, { "4", "5", "6" }, { "7", "8", "9" } };
string pattern = "[1-9qQrR]";
var moves = 0;
var gameOver = false;
var regex = new Regex(pattern);
var player = "X";
DrawBoard(instructions);
Console.WriteLine($"Player {player}'s turn.");
Console.WriteLine("Enter cell or q to quit");

while (true)
{
    // ReadKey reads a single key from the console without having to press enter.
    // To read a whole line, use Console.ReadLine()
    var input = Console.ReadKey().KeyChar.ToString();
    if (!regex.IsMatch(input))
    {
        //continue;
    }
    Console.WriteLine();
    if(input.ToLower() == "q")
    {
        break;
    } else if(input.ToLower() == "r")
    {
        ResetGame(ref board);
        //continue;
    } else if (gameOver || moves == 9)
    {
        //continue;
    }
    else 
    { 
        var cell = int.Parse(input);
        if(SetCell(board, cell, player))
        {
            gameOver = IsGameOver(board);
            if(!gameOver && moves < 9)
            {
                player = player == "X" ? "O" : "X";
            }
        }
    }
    Console.Clear();
    DrawBoard(board);
    if (gameOver)
    {
        Console.WriteLine($"Player {player} wins.");
        Console.WriteLine("Press r to reset or q to quit");
    }
    else if (moves == 9)
    {
        Console.WriteLine("Game over. Press r to reset or q to quit");
    }
    else
    {
        Console.WriteLine($"Player {player}'s turn.");
        Console.WriteLine("Enter cell or q to quit");
    }
}

/// <summary>Resets the game.</summary>
void ResetGame(ref string[,] board)
{
    ResetBoard(ref board);
    moves = 0;
    gameOver = false;
    player = "X";
}

/// <summary>Resets the board.</summary>
void ResetBoard(ref string[,] board)
{
    board = new string[3, 3]  { { " ", " ", " " }, { " ", " ", " " }, { " ", " ", " " } };
}

/// <summary>Checks if the game is over.</summary>
bool IsGameOver(string[,] board)
{
    return (
        (board[0, 0] == board[0, 1] && board[0, 1] == board[0, 2] && board[0, 0] != " ") ||
        (board[1, 0] == board[1, 1] && board[1, 1] == board[1, 2] && board[1, 0] != " ") ||
        (board[2, 0] == board[2, 1] && board[2, 1] == board[2, 2] && board[2, 0] != " ") ||
        (board[0, 0] == board[1, 0] && board[1, 0] == board[2, 0] && board[0, 0] != " ") ||
        (board[0, 1] == board[1, 1] && board[1, 1] == board[2, 1] && board[0, 1] != " ") ||
        (board[0, 2] == board[1, 2] && board[1, 2] == board[2, 2] && board[0, 2] != " ") ||
        (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2] && board[0, 0] != " ") ||
        (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0] && board[0, 2] != " ")
        );
}

/// <summary>Sets the cell value if empty.</summary>
bool SetCell(string[,] board, int cell, string player)
{
    var row = (cell - 1) / 3;
    var col = (cell - 1) % 3;
    if (board[row, col] != " ")
    {
        return false;
    }
    moves++;
    board[row, col] = player;
    Console.WriteLine($"row: {row}, col: {col}, moves: {moves}");
    return true;
}

/// <summary>Draws the board to the console.</summary>
void DrawBoard(string[,] board)
{
    ColorWrite($"┌─┬─┬─┐");
    ColorWrite($"│{GetCell(0, 0)}│{GetCell(0, 1)}│{GetCell(0, 2)}│");
    ColorWrite($"├─┼─┼─┤");
    ColorWrite($"│{GetCell(1, 0)}│{GetCell(1, 1)}│{GetCell(1, 2)}│");
    ColorWrite($"├─┼─┼─┤");
    ColorWrite($"│{GetCell(2, 0)}│{GetCell(2, 1)}│{GetCell(2, 2)}│");
    ColorWrite($"└─┴─┴─┘");
}

/// <summary>Gets the cell value if set or instruction if cell is empty.</summary>
string GetCell(int row, int col)
{
    return board[row, col] == " " ? instructions[row, col] : board[row, col];
}

/// <summary>Writes text to the console with color.</summary>
void ColorWrite(string text)
{
    foreach (var c in text)
    {
        if (c == 'X')
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }
        else if (c == 'O')
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.White;
        }
        Console.Write(c);
    }
    Console.WriteLine("");
    Console.ResetColor();
}


