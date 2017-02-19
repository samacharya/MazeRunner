using System;

namespace MazeRunner.Source
{
    internal class Program
    {
        /// <summary>
        /// This is where it begins...
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Error: Invalid number of arguments."); // Enter input/output image files from the command line... 
                return;                                                   // ...(i.e. maze.exe "source.png" "destination.png")
            }
            string inputImage = args[0];
            string outputImage = args[1];

            BluebeamMazeSolver mazeSolver = new BluebeamMazeSolver(); // The maze solver 
            mazeSolver.SolveMaze(inputImage, outputImage); 
        }
    }
}
