using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MazeRunner
{
    /// <summary>
    /// Breadth First Search algorithm to find way out of maze.
    /// </summary>
    class MazeRunner : IMazeRunner
    {
        long counter = 0;
        /// <summary>
        /// Prints the path points as green overlay over the input image file and saves as a new image file.
        /// </summary>
        /// <param name="pathNodes"></param>
        /// <param name="imageInPath"></param>
        /// <param name="imageOutPath"></param>
        /// <param name="imageOutFormat"></param>
        public void PrintSolutionPath(IEnumerable<MazeNode> pathNodes, string imageInPath, string imageOutPath, string imageOutFormat)
        {
            Bitmap solutionImage = new Bitmap(imageInPath);
            foreach (var node in pathNodes)
            {
                solutionImage.SetPixel(node.X, node.Y, Color.Green);
            }
            switch (imageOutFormat)
            {
                case "bmp":
                    solutionImage.Save(imageOutPath, System.Drawing.Imaging.ImageFormat.Bmp);
                    break;
                case "jpg":
                    solutionImage.Save(imageOutPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    break;
                default:
                    solutionImage.Save(imageOutPath, System.Drawing.Imaging.ImageFormat.Png);
                    break;
            }
        }

        /// <summary>
        /// Retrieves solution path points out of a solvable maze. 
        /// </summary>
        /// <param name="maze"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public IEnumerable<MazeNode> GetMazePath(IMaze maze, MazeNode end)
        {
            MazeNode currNode = end;
            ICollection<MazeNode> path = new List<MazeNode>(); // List of solution path nodes. 
            while (currNode != null)
            {
                path.Add(currNode);
                currNode = currNode.Parent; // Backtracking to retrieve all path nodes.
            }
            return path;
        }

        /// <summary>
        /// The main BFS algorithm. Returns list of solution path nodes using backtracking method.
        /// </summary>
        /// <param name="maze"></param>
        /// <param name="imagePath"></param>
        public IEnumerable<MazeNode> Search(IMaze maze, string imagePath)
        {
            Queue<MazeNode> queue = new Queue<MazeNode>(); // Maintains unexplored nodes.
            MazeNode start = maze.Start; // The starting node (first red pixel)
            start.State = NodeState.Queued; // Start node goes into the queue first.
            queue.Enqueue(start);
            start.Parent = null;             

            while (queue.Count > 0)
            {
                MazeNode currNode = queue.Dequeue();
                if (maze.IsTarget(currNode))
                {
                    var outPath = GetMazePath(maze, currNode); // Get path points
                    Console.WriteLine("Solution found!");
                    Console.WriteLine("Nodes visited: {0}", counter);
                    return outPath;
                }
                foreach (MazeNode adjNode in maze.GetAdjacentNodes(currNode))
                {
                    if (adjNode.State != NodeState.NotVisited) continue;
                    adjNode.Parent = currNode; // Assign parent node for backtracking
                    adjNode.State = NodeState.Queued; // Add node to queue
                    queue.Enqueue(adjNode);
                }
                currNode.State = NodeState.Visited; // Mark visited nodes to avoid infinite loop
                counter++;
            }
            return Enumerable.Empty<MazeNode>();
        }
    }
}
