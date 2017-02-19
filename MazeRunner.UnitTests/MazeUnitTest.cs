using MazeRunner;
using System.Drawing;
using MazeRunner.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MazeRunner.UnitTests
{
    [TestClass]
    public class MazeUnitTest
    {
        // Helper method for generating test bitmaps at runtime
        public Bitmap GenerateBitmap(int width, int height, Point start, Point end)
        {
            Bitmap img = new Bitmap(10, 10);
            img.SetPixel(start.X, start.Y, Color.Red);
            img.SetPixel(end.X, end.Y, Color.Blue);
            return img;
        }

        [TestMethod]
        public void TestOutputFileExtension()
        {
            BluebeamMazeSolver bms = new BluebeamMazeSolver();
            string ext = bms.GetImageFormat("destination.png");
            Assert.AreEqual("png", ext);
        }

        [TestMethod]
        public void TestStartNodePosition()
        {
            Point start = new Point(2, 2);
            Point end = new Point(7,8);
            Bitmap testImg = GenerateBitmap(10, 10, start, end);
            Maze maze = new Maze();
            maze.InitMaze(testImg);
            Point mazeStart = new Point(maze.Start.X, maze.Start.Y);
            Assert.AreEqual(start, mazeStart);
        }

        [TestMethod]
        public void TestEndNodePosition()
        {
            Point start = new Point(5, 9);
            Point end = new Point(1, 1);
            Bitmap testImg = GenerateBitmap(10, 10, start, end);
            Maze maze = new Maze();
            maze.InitMaze(testImg);
            Point mazeEnd = new Point(maze.Finish.X, maze.Finish.Y);
            Assert.AreEqual(end, mazeEnd);
        }
    }
}
