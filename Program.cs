using System;
using System.Security;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Robot
{
    class Program
    {
        const Char CHAR_NODE = ' ';
        const Char CHAR_ROCK = '-';
        const String CHAR_HOLES = "<{["; // for each 2 open-close combo in grid, 1 hole is dug
        //                                           (< goes to >, [ goes to ], { goes to } and vise versa)
        static Stack<Obstacle> obstacles; // spaces which effect movement
        static Stack<Node> nodes; // freely movable spaces
        static private string gridStr;
        static int[] digHole(char holeChar) // returns the position of the other end of a hole <{[
        {
            int rowLength = gridStr.Split(Environment.NewLine.ToCharArray())[0].Length;
            char otherSide = (char)((int)holeChar + 2);
            return (gridStr.Contains(otherSide)?new int[]{gridStr.Replace(Environment.NewLine, "").IndexOf(otherSide)%rowLength,(int)Math.Floor(gridStr.IndexOf(otherSide) / (double)rowLength)}:null);
        } // only works because <{[ all have closing chars at +2 their ascii values
        //   ending position has x value given by indexof(opposite char value in gridStr) % (row length + 1)
        //                  and y value at floor(indexof(opposite char value) / row length)
        //                  where row length is given by length of first row.

        static void parseGrid(string fileName)
        {
            if (!File.Exists(fileName)) { Console.WriteLine("File does not exist!"); return; }
            
            try
            {
                gridStr = File.ReadAllText(fileName);
            }
            catch (ArgumentException ae) { Console.WriteLine("Invalid file name!"); return; }
            catch (PathTooLongException ptle) { Console.WriteLine("Path is too long!"); return; }
            catch (DirectoryNotFoundException dnfe) { Console.WriteLine("Directory not found!"); return; }
            catch (IOException ioe) { Console.WriteLine("I/O exception!"); return; }
            catch (UnauthorizedAccessException uae) { Console.WriteLine("Cannot access file!"); return; }
            catch (SecurityException se) { Console.WriteLine("Security exception!"); return; }
            // file was read successfully
            obstacles = new Stack<Obstacle>();
            nodes = new Stack<Node>();
            int x, y; x = 0; y = 0;
            foreach (string line in gridStr.Trim().Split(Environment.NewLine.ToCharArray()))
            {
                foreach (char chr in line)
                {
                    if (chr==CHAR_ROCK) { // is a rock
                        Obstacle r = new Rock(new int[] {x, y});
                        obstacles.Push(r);
                    }
                    if(Char.IsNumber(chr)){ // is a spinner
                        Obstacle s = new Spinner(new int[] {x, y}, chr-'0');
                        
                        obstacles.Push(s);
                    }
                    if(CHAR_HOLES.Contains(chr)){ // is a hole
                        Obstacle h = new Hole(new int[] { x, y }, digHole(chr));
                        Obstacle h2 = new Hole(digHole(chr), new int[] { x, y });
                        //Console.WriteLine(h.position[0] + ", " + h.position[1] + " -> " + h2.position[0] + ", " + h2.position[1]);
                        obstacles.Push(h); obstacles.Push(h2);
                    }
                    if (chr.Equals(CHAR_NODE))
                    {
                        Node n = new Node(new int[]{ x, y });
                        nodes.Push(n);
                    }
                    x+=(x>=line.Length?-line.Length+1:1);
                }
                if (!String.IsNullOrEmpty(line) && !String.IsNullOrWhiteSpace(line)) { y++; }
                Console.WriteLine(line);
            }
        }

        static void Main(string[] args)
        {
            if (args.Length < 4) { Console.WriteLine("Incorrect syntax\nRobot.exe gridFile.txt startingX startingY FFRFFLFRLF"); return; }
            foreach (string arg in args) { if (arg.All(Char.IsWhiteSpace)) { Console.WriteLine("Missing argument?"); return; } }
            parseGrid(args[0]);
            Console.WriteLine("Grid read successfully.\nFound " + obstacles.Count + " obstacles and " + nodes.Count + " nodes.");
            GenericRobot gr = new GenericRobot(new int[] { Int16.Parse(args[1].Trim()), Int16.Parse(args[2].Trim()) }, GenericRobot.Facing.NORTH, args[3], obstacles, nodes);
            Console.WriteLine("Grid traversed successfully.\nEnding position: " + gr.curPos[0] + ", " + gr.curPos[1]);
            
        }
    }
}
