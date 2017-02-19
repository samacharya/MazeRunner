using System.Collections.Generic;

namespace MazeRunner
{
    /// <summary>
    /// Interface for a maze solver
    /// </summary>
    interface IMazeRunner
    {
        IEnumerable<MazeNode> Search(IMaze maze, string imagePath);
        IEnumerable<MazeNode> GetMazePath(IMaze maze, MazeNode finish);
        void PrintSolutionPath(IEnumerable<MazeNode> pathNodes, string imageIn, string imageOut, string imageOutFormat);
    }
}
