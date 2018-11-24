using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace My_BattleShip
{

    /// <summary>
    ///            i Have many issue with Variable scope
    /// </summary>




    static class BattleShip
    {


        static print_board(board)
        {

            for row in board {  // how to set this for in c#
                Console.WriteLine(" ".join(row)); // maybe concat in C#
            }
        }


        // Boats Constructor :
        // Only Create a size 1 boat for the moment

        Random random1 = new Random(); // issue with Static class
        


        static int random_row(char[] board)
        {
            int randint = random1.Next(0, board.Count() - 1); // issue with object reference

            return randint;
        }


        static int random_col(char[] board)
        {
            int randint2 = random1.Next(0, board.Count() - 1);

            return randint2;
        }











        /// <summary>
        /// i translate my python battleship into c#
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());





            char[] board = { 'O' }; //it will be a [[OOOOO],[OOOOO]] so how it work on c#
            int BoardSize = 5; //default setting
            int[] Boat = { 0 }; // dont remanber if boat is a char[]
            int rounds = 4; //default setting
            int total = 0;

            //int GameMode = int(raw_input("game difficulty (1, 2, 3, 0 for custom): ")); //ask Write the game difficulty on the console : Console.ReadLine() // Int32.Parse()
            Console.WriteLine("game difficulty (1, 2, 3, 0 for custom): ");
            int GameMode = Int32.Parse(Console.ReadLine());
            Console.WriteLine(" ");

            if (GameMode == 1) {
                Console.WriteLine("Easy Mode Selected : 5*5 Board");
                Console.WriteLine("2 Boats of size 2");
                Console.WriteLine(" ");


                BoardSize = 5;
                rounds = 4;
            }

            else if (GameMode == 2) {
                Console.WriteLine("Medium Mode Selected : 10*10 Board");
                Console.WriteLine("1 Boat of size 2");
                Console.WriteLine("2 Boats of size 3");
                Console.WriteLine("2 Boats of size 4");
                Console.WriteLine("1 Boat of size 5");
                Console.WriteLine(" ");
                BoardSize = 10;
                rounds = 50;
            }


            else if (GameMode == 3) {
                Console.WriteLine("Hard Mode selected : 26*26 Board");
                Console.WriteLine("1 Boat of size 2");
                Console.WriteLine("2 Boats of zize 3");
                Console.WriteLine("3 Boats of size 4");
                Console.WriteLine("3 Boats of size 5");
                Console.WriteLine("2 Boats of size 6");
                Console.WriteLine("1 Boat of size 7");
                Console.WriteLine(" ");
                BoardSize = 26;
                rounds = 100;

            }


            else if (GameMode == 0) {
                Console.WriteLine("Custom Mode Selected :");
                Console.WriteLine("BoardSize : ");
                BoardSize = Int32.Parse(Console.ReadLine()); // ask to write Boardsize

            }

            if (BoardSize < 2 || BoardSize > 26) {
                Console.WriteLine("your BoardSize is too small or too big : ");
                BoardSize = Int32.Parse(Console.ReadLine()); // ask to write correct BoardSize



                int NumbersOfSize = BoardSize / 2;
                Console.WriteLine("you choose " + BoardSize.ToString() + "*" + BoardSize.ToString() + " board so it could have " + NumbersOfSize.ToString() + " sizes of boat"); // int into char/string for print it : .ToString
                Console.WriteLine(" ");

            }



            //for n in range(NumbersOfSize, 1, -1)  // how set up this for in c# with with parameter ? <------- help

            for (int n = NumbersOfSize; n == 1; n--  ) {

                Console.WriteLine("-> max " + ((Math.Pow(BoardSize,2) - total) / n).ToString() + " of size " + n.ToString() + " :");
                Console.WriteLine("Numbers of size's " + n.ToString() + " boat : ");
                int number = Int32.Parse(Console.ReadLine()); // ask to write numbers of size boats


                if (((Math.Pow(BoardSize,2) - total) - number * n) < 0) {
                    Console.WriteLine("Please enter a input less than " + ((Math.Pow(BoardSize,2) - total) / n).Tostring() + " : ");
                    int number = Int32.Parse(Console.ReadLine()); // ask to write // variable scope issue
                }

                else {
                    Boat.append(number); //for the boat generator .append() = .Add() in c# ??
                    int total =+ number * (n); // Variable scope issue

                    Console.WriteLine(" ");


                    Console.WriteLine("number of rounds (more than " + total.ToString() + " and less than " + (Math.Pow(BoardSize,2)).ToString() + "): ");
                    rounds = Int32.Parse(Console.ReadLine()); // ask to write 

                }

            }

            if ((rounds < total + 1) || (rounds > Math.Pow(BoardSize,2))) { 
                Console.WriteLine("your number of rounds must be beetween " + total.ToString() + " and " + (Math.Pow(BoardSize,2)).ToString() + " : ");
                total = Int32.Parse(Console.ReadLine()); // ask to write 
            }


            else {
                Console.WriteLine("Please enter a good input (1,2,3,0) : ");
                GameMode = Int32.Parse(Console.ReadLine()); // ask to write
            }




            //for x in range(BoardSize) { // how to set this for in c#
            for(int x = 0; x < BoardSize; x++) {

                //board.append(["O"] * BoardSize); // create a OOOOOOO... colum and row of size of BoardSize // said miss a { or } so i put in commentary
            }





            print_board(board);
            Console.WriteLine(" ");





            int ship_row = random_row(board);
            int ship_col = random_col(board);

            //debug info :
            Console.WriteLine(ship_row + 1);
            Console.WriteLine(ship_col + 1);


            // Main : 
            // Analyze only a size 1 ship for the moment




            //for turn in range(rounds) // how to set this for in c#
            for (int turn = 0; turn < rounds; turn++)
            { } // dont remenber where this for end ...............
                Console.WriteLine("___________");


                if (turn == rounds - 1) {
                    Console.WriteLine("Last Turn :");
                    Console.WriteLine(" ");
                }


                else {
                    Console.WriteLine("Turn " + (turn + 1).ToString + " :");
                    Console.WriteLine(" ");

                    Console.WriteLine("Guess Row: ");
                    int guess_row = Int32.Parse(Console.ReadLine()); // ask to Guess Row

                    Console.WriteLine("Guess Col: ");
                    int guess_col = Int32.Parse(Console.ReadLine()); // ask to Guess col
                    Console.WriteLine(" ");

                }

                // maybe the for end here

           if (guess_row - 1 == ship_row && guess_col-1 == ship_col) // variable scope issue
            {
                Console.WriteLine("Congratulations! You sunk my battleship!");
                board[guess_row - 1][guess_col - 1] = "V"); // the [[]; []]
                print_board(board); //why i can't call the method ?
                break;
            }
    
           else {

                if ((guess_row - 1 < 0 || guess_row-1 > BoardSize - 1) || (guess_col - 1 < 0 ||  guess_col - 1 > BoardSize - 1) ) {
                    Console.WriteLine("Oops, that's not even in the ocean.");
                    Console.WriteLine(" ");

                    if (turn == rounds - 1) {
                        Console.WriteLine("Game Over");
                    }

                }

                


                else if ((board[guess_row - 1][guess_col - 1] == "X")) {
                    Console.WriteLine("You guessed that one already.");
                    Console.WriteLine(" ");

                    if (turn == rounds - 1) {
                        Console.WriteLine("Game Over");
                    }
                }

                else {
                    Console.WriteLine("You missed my battleship!");
                    board[guess_row - 1][guess_col - 1] = "X");

                    if (turn == rounds - 1) {
                        Console.WriteLine("Game Over");
                    }

                }

                print_board(board); //why i can't call the method ?
                Console.WriteLine(" ");

            }

           //Maybe the for end here

        

         


















        }
    }
}
