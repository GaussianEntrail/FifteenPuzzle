using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Fifteen
    {
        static string symbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        int[] grid;
        int w, sq;
        int pos;
        int moves;
        string bar;
        public bool runGame;
        public bool win;
        public int gameState;
        public Fifteen(int w) {
            runGame = true;
            this.w = w;
            sq = w * w;
            bar = new string('═', w * 3);
            grid = Enumerable.Range(0, sq).ToArray();
            pos = getPosition();
            moves = 0;
            win = false;
            shuffle();
            int gameState = 0;
        }
        private int getPosition() { return Array.FindIndex(grid, x => x == sq - 1); }
        private bool solved()
        {
            int i = grid.Length - 1;
            if (i <= 0) { return true; }
            if ((i & 1) > 0) { if (grid[i] < grid[i - 1]) return false; i--; }
            for (int ai = grid[i]; i > 0; i -= 2)
                if (ai < (ai = grid[i - 1]) || ai < (ai = grid[i - 2])) return false;
            return grid[0] <= grid[1];
        }
        public void shuffle(int shuffleAmount = 10000)
        {
            Random r = new Random();
            int choose;
            for (int m= 0; m < shuffleAmount; m++)
            {
                choose = r.Next() % 4;
                switch (choose)
                {
                    case 0: moveUP(); break;
                    case 1: moveLEFT(); break;
                    case 2: moveDOWN(); break;
                    case 3: moveRIGHT(); break;
                }
            }
            moves = 0;
        }
        private bool withinBounds(int x) { return (x >= 0 && x < sq); }
        private void swap (int p1, int p2)
        {
            if (!withinBounds(p1) || !withinBounds(p2)) { return; }
            int temp = grid[p1];
            grid[p1] = grid[p2];
            grid[p2] = temp;
        }
        public bool moveUP()
        {
            if (pos / w == 0) { return false; } //cannot move UP if in the top row
            swap(pos, pos - w); //swap the empty tile with the tile above it
            pos -= w; //update the variable tracking the position of the empty tile
            moves++;
            return true;
        }
        public bool moveLEFT()
        {
            if (pos % w == 0) { return false; } //cannot move LEFT if on edge
            swap(pos, pos - 1); //swap the empty tile with the tile to it's left
            pos--; //update the variable tracking the empty tile's position
            moves++;
            return true;
        }
        public bool moveDOWN()
        {
            if (pos / w == w - 1) { return false; } //cannot move DOWN if in the bottom row
            swap(pos, pos + w); //swap the empty tile with the tile below it
            pos += w; //update the variable tracking the position of the empty tile
            moves++;
            return true;
        }
        public bool moveRIGHT()
        {
            if (pos % w == w - 1) { return false; } //cannot move RIGHT if on edge
            swap(pos, pos + 1); //swap the empty tile with the tile to it's right
            pos++; //update the variable tracking the empty tile's position
            moves++;
            return true;
        }
        public void draw()
        {
            Console.Clear();
            resetColors();
            Console.WriteLine("╔"+bar+ "╗");
            for (int n = 0; n < sq; n++)
            {
                int num = grid[n];
                bool color = ( ((num / w) + (num % w)) % 2 == 0) ;
                resetColors();
                if (n % w == 0) { Console.Write("║"); }

                if (num == sq - 1) { Console.BackgroundColor = ConsoleColor.Black; }
                else if (color) { Console.BackgroundColor = ConsoleColor.Red; }
                else { Console.BackgroundColor = ConsoleColor.Cyan; }

                Console.Write(" {0} ", (num == sq - 1)? ' ': symbols[num] );

                resetColors();
                if (n % w == w-1) { Console.Write("║\n"); }
            }
            Console.WriteLine("╚" + bar + "╝");
            Console.WriteLine("Moves: {0}", moves);
            if (win) { Console.WriteLine("YOU DID IT!"); }
        }
        public void control()
        {
            var c = Console.ReadKey();
            switch (c.Key)
            {
                default:
                    break;
                case ConsoleKey.NumPad8:
                case ConsoleKey.UpArrow:
                    moveUP();
                    break;
                case ConsoleKey.NumPad4:
                case ConsoleKey.LeftArrow:
                    moveLEFT();
                    break;
                case ConsoleKey.NumPad2:
                case ConsoleKey.DownArrow:
                    moveDOWN();
                    break;
                case ConsoleKey.NumPad6:
                case ConsoleKey.RightArrow:
                    moveRIGHT();
                    break;
                case ConsoleKey.Escape:
                    runGame = false;
                    break;
            }

            if (solved()) { win = true; }
        }

        private void resetColors()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

    }
}
