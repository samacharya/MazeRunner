using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace MazeRunner.Source
{
    /// <summary>
    /// Represents a closed-wall maze.
    /// </summary>
    public class Maze : IMaze
    {
        private MazeNode _begin = null;
        private MazeNode _end = null;
        private int _width = 0;
        private int _height = 0;
        MazeNode[,] _mazeNodes = null;

        public Bitmap MazeImage { get; set; }

        public MazeNode Start
        {
            get { return _begin; }

            set { throw new NotImplementedException(); }
        }

        public MazeNode Finish
        {
            get { return _end; }

            set { throw new NotImplementedException(); }
        }

        public int Width
        {
            get { return _width; }

            set { throw new NotImplementedException(); }
        }

        public int Height
        {
            get { return _height; }

            set { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Empty constructor (useful for testing purposes).
        /// </summary>
        public Maze()
        {
        }

        /// <summary>
        /// Constructor. Calls initMaze() function to configure the maze.
        /// </summary>
        /// <param name="imagePath"></param>
        public Maze(string imagePath)
        {
            if (!File.Exists(imagePath))
                throw new FileNotFoundException("Invalid image file");
            
            try
            {
                MazeImage = new Bitmap(imagePath);
                InitMaze(MazeImage);
            }
            catch (Exception)
            {
                Console.WriteLine("Error! Failed to initialize maze.");
            }
            
        }

        /// <summary>
        /// Get node at provided coordinates.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public MazeNode GetNode(int x, int y)
        {
            return _mazeNodes[x, y];
        }

        /// <summary>
        /// Returns all maze nodes.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<MazeNode> GetNodes()
        {
            for (int i = 0; i < _height; i++)
                for (int j = 0; j < _width; j++)
                    yield return _mazeNodes[i, j];
        }

        /// <summary>
        /// Checks if node is target node.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool IsTarget(MazeNode node)
        {
            return node.Equals(_end);
        }

        /// <summary>
        /// Gets 4 adjacent nodes (left, right, top, bottom). Does not consider diagonal nodes
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public IEnumerable<MazeNode> GetAdjacentNodes(MazeNode node)
        {
            int xPos = node.X;
            int yPos = node.Y;

            if (!IsWithinBounds(xPos, yPos))
                return new List<MazeNode>(0);

            List<MazeNode> adjNodes = new List<MazeNode>(4);
            if (IsWithinBounds(xPos + 1, yPos))
                adjNodes.Add(_mazeNodes[xPos + 1, yPos]); 
            if (IsWithinBounds(xPos, yPos - 1))
                adjNodes.Add(_mazeNodes[xPos, yPos - 1]);
            if (IsWithinBounds(xPos - 1, yPos))
                adjNodes.Add(_mazeNodes[xPos - 1, yPos]);
            if (IsWithinBounds(xPos, yPos + 1))
                adjNodes.Add(_mazeNodes[xPos, yPos + 1]);
            return adjNodes;
        }

        /// <summary>
        /// Checks if given coordinates are within maze boundaries.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public bool IsWithinBounds(int row, int col)
        {
            return ((row >= 0 && row < _width && col >= 0 && col <= _height));    
        }

        /// <summary>
        /// Configure maze properties (width, height, start node, end node, and states). 
        /// </summary>
        /// <param name="maze"></param>
        public void InitMaze(Bitmap maze)
        {
            if (maze != null)
            {
                _height = maze.Height;
                _width = maze.Width;
                _mazeNodes = new MazeNode[_width, _height];

                for (int i = 0; i < _width; i++)
                for (int j = 0; j < _height; j++)
                {
                    if (maze.GetPixel(i, j) == Color.FromArgb(255, 255, 255, 255))
                        _mazeNodes[i, j] = new MazeNode(i, j) { State = NodeState.NotVisited, Parent = null };

                    if (maze.GetPixel(i, j) == Color.FromArgb(255, 0, 0, 0))
                        _mazeNodes[i, j] = new MazeNode(i, j) { State = NodeState.Blocked, Parent = null };

                    if (maze.GetPixel(i, j) == Color.FromArgb(255, 255, 0, 0))
                    {
                        _mazeNodes[i, j] = new MazeNode(i, j) { State = NodeState.NotVisited, Parent = null };
                        if (_begin == null)
                        {
                            _begin = _mazeNodes[i, j];
                        }
                    }

                    if (maze.GetPixel(i, j) == Color.FromArgb(255, 0, 0, 255))
                    {
                        _mazeNodes[i, j] = new MazeNode(i, j) { State = NodeState.NotVisited, Parent = null };
                        if (_end == null)
                        {
                            _end = _mazeNodes[i, j];
                        }
                    }
                }
            }
            if (_begin == null || _end == null)
            {
                Console.WriteLine("Error: Start and/or end point not found!");
            }
        }
    }
}
