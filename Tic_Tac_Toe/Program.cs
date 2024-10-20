using System;

namespace Tic_Tac_Toe
{
    internal class Program
    {
        enum Cell
        {
            Cross,
            Zero,
            Null
        }

        enum Step
        {
            User,
            Comp
        }

        static void Main(string[] args)
        {
            #region Create game field and variables

            int fieldSize = 3;
            Cell[,] usageField = new Cell[fieldSize, fieldSize];

            
            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
                {
                    usageField[i, j] = Cell.Null;
                }
            }

            Random rnd = new Random();
            int coordI, coordJ;
            bool playGame = true;
            Step currentStep = Step.User;
            Step winner = Step.User;
            Cell lastSymbol = Cell.Null;

            #endregion

            #region Game cycle

            while (playGame)
            {
                Console.Clear();
                Console.WriteLine("Let's play Tic Tac Toe!");

                
                for (int i = 0; i < fieldSize; i++)
                {
                    for (int j = 0; j < fieldSize; j++)
                    {
                        switch (usageField[i, j])
                        {
                            case Cell.Null:
                                Console.Write("( )");
                                break;
                            case Cell.Cross:
                                Console.Write(" X ");
                                break;
                            case Cell.Zero:
                                Console.Write(" O ");
                                break;
                        }
                    }
                    Console.WriteLine();
                }

                
                if (CheckWinner(usageField, out winner))
                {
                    Console.WriteLine($"The last symbol was {(lastSymbol == Cell.Cross ? "X" : "O")}!");
                    Console.WriteLine($"The winner is {winner}!");
                    break;
                }

                switch (currentStep)
                {
                    case Step.User:
                        Console.WriteLine("User's turn...");

                        
                        do
                        {
                            Console.Write("Input row (1, 2, 3): ");
                            coordI = int.Parse(Console.ReadLine()) - 1; 

                            Console.Write("Input column (1, 2, 3): ");
                            coordJ = int.Parse(Console.ReadLine()) - 1; 

                        } while (coordI < 0 || coordI >= fieldSize || coordJ < 0 || coordJ >= fieldSize || usageField[coordI, coordJ] != Cell.Null);

                        
                        usageField[coordI, coordJ] = Cell.Cross;
                        lastSymbol = Cell.Cross; 
                        currentStep = Step.Comp;
                        break;

                    case Step.Comp:
                        Console.WriteLine("Computer's turn...");

                        
                        do
                        {
                            coordI = rnd.Next(0, fieldSize);
                            coordJ = rnd.Next(0, fieldSize);
                        } while (usageField[coordI, coordJ] != Cell.Null);

                        usageField[coordI, coordJ] = Cell.Zero;
                        lastSymbol = Cell.Zero; 
                        currentStep = Step.User;
                        break;
                }

                
                if (IsDraw(usageField))
                {
                    Console.WriteLine("It's a draw!");
                    break;
                }
            }

            Console.WriteLine("Game over!");
            Console.ReadLine();
            #endregion
        }

        #region All checks and method CheckWinner
        static bool CheckWinner(Cell[,] field, out Step winner)
        {
            winner = Step.User;

            
            for (int i = 0; i < 3; i++)
            {
                
                if (field[i, 0] == field[i, 1] && field[i, 1] == field[i, 2] && field[i, 0] != Cell.Null)
                {
                    winner = field[i, 0] == Cell.Cross ? Step.User : Step.Comp;
                    return true;
                }
                
                if (field[0, i] == field[1, i] && field[1, i] == field[2, i] && field[0, i] != Cell.Null)
                {
                    winner = field[0, i] == Cell.Cross ? Step.User : Step.Comp;
                    return true;
                }
            }

           
            if (field[0, 0] == field[1, 1] && field[1, 1] == field[2, 2] && field[0, 0] != Cell.Null)
            {
                winner = field[0, 0] == Cell.Cross ? Step.User : Step.Comp;
                return true;
            }
            if (field[0, 2] == field[1, 1] && field[1, 1] == field[2, 0] && field[0, 2] != Cell.Null)
            {
                winner = field[0, 2] == Cell.Cross ? Step.User : Step.Comp;
                return true;
            }

            return false;
        }
        #endregion


        #region Draw method
        static bool IsDraw(Cell[,] field)
        {
            foreach (var cell in field)
            {
                if (cell == Cell.Null)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}
