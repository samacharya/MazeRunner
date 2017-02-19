using System.Collections.Generic;

namespace MazeRunner
{
    /// <summary>
    /// Interface for a maze.
    /// </summary>
    interface IMaze
    {
        int Height { get; set; }
        int Width { get; set; }
        MazeNode Start { get; set; }
        MazeNode Finish { get; set; }
        MazeNode GetNode(int x, int y);
        IEnumerator<MazeNode> GetNodes();
        IEnumerable<MazeNode> GetAdjacentNodes(MazeNode node);
        bool IsWithinBounds(int row, int column);
        bool IsTarget(MazeNode node);
    }
}
