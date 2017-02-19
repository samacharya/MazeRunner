using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MazeRunner.Source;

namespace MazeRunner
{
    /// <summary>
    /// Launches the maze solving algorithm.
    /// </summary>
    public class BluebeamMazeSolver
    {
        public void SolveMaze(string imageIn, string imageOut)
        {           
            try
            {
                var timer = System.Diagnostics.Stopwatch.StartNew(); // Start timing 
                string format = GetImageFormat(imageIn); // Get input image file format
                List<string> formatList = new List<string> { "jpg", "bmp", "png" }; // Only 3 types of image files supported
                if (!formatList.Contains(format))                                   // Throws exception if any other format 
                    throw new FileNotFoundException("Invalid image file. Please use bmp, jpg or png files only.");

                IMaze maze = new Maze(imageIn); // Initialize the maze with given input image file.
                MazeRunner mazeRunner = new MazeRunner(); // Initialize the maze solver object. This one uses a BFS (Breadth First Search algorithm).
                IEnumerable<MazeNode> wayOut = mazeRunner.Search(maze, imageOut); // Retrieve the solution path points if exist.
                var mazeNodes = wayOut as MazeNode[] ?? wayOut.ToArray();
                if (mazeNodes.Any())
                {
                    mazeRunner.PrintSolutionPath(mazeNodes, imageIn, imageOut, GetImageFormat(imageOut)); // Print the solution path
                    timer.Stop();
                    var secs = timer.ElapsedMilliseconds;
                    Console.WriteLine("Time taken: {0} ms", secs.ToString()); // print time taken                                              
                }
                else
                {
                    timer.Stop();
                    Console.WriteLine("Maze cannot be solved!"); // No way out!                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
        }

        /// <summary>
        /// Retrieves specified output image format. Only takes 3 formats (i.e. BMP, JPEG and PNG)
        /// </summary>
        /// <param name="imageOutPath"></param>
        /// <returns></returns>
        public string GetImageFormat(string imageOutPath)
        {
            string[] substrings = imageOutPath.Split('.'); // Split string to filename and extension
            return (substrings[1]); // return extension type as string
        }
    }
}
