namespace MazeRunner
{
    // A node can have 4 states. 
    public enum NodeState : int
    {
        NotVisited = 0,
        Visited = 1,
        Blocked = 2,
        Queued = 3
    }

    /// <summary>
    /// Maze node. Every pixel,block,character, etc. in a maze is represented by a MazeNode object.
    /// </summary>
    public class MazeNode
    {
        public int X { get; set; }
        public int Y { get; set; }
        public MazeNode Parent { get; set; }
        public NodeState State { get; set; } = NodeState.NotVisited;

        public MazeNode(int xPos, int yPos)
        {
            X = xPos;
            Y = yPos;
        }        
    }
}
