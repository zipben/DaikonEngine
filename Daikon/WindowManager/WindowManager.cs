using Daikon.Components;
using Daikon.Factories;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;

namespace Daikon.WindowManager
{
    public static class WindowManager
    {
        static RectangleShape dude;
        static RectangleShape floor;
        static StupidShapeFactory shapeFactory;
        static IStupidShape fallingShape;
        static Vector2f FallingShapeLocation;
        static List<Shape> FallingSegments;
        static List<Shape> LandedSegments;


        const float SEGMENT_SIZE = 50;


        public static void Run()
        {
            shapeFactory = new StupidShapeFactory();

            VideoMode mode = new VideoMode(500, 1000);
            RenderWindow window = new RenderWindow(mode, "Shitty Tetris");

            window.Closed += (obj, e) => { window.Close(); };
            window.KeyPressed += KeyPressHandler;
            window.Clear(Color.White);

            ResetFallingShapeLocation(window);

            FallingSegments = new List<Shape>();
            LandedSegments = new List<Shape>();

            fallingShape = shapeFactory.GetNextStupidShape();

            floor = new RectangleShape();
            floor.FillColor = Color.Green;
            floor.Size = new Vector2f(500, 50);
            floor.Position = new Vector2f(0, window.Size.Y - 50);


            Clock clock = new Clock();
            float delta = 0f;
            float angle = 0f;
            float angleSpeed = 90f;

            while (window.IsOpen)
            {
                //Game Loop
                window.DispatchEvents();
                window.Clear(Color.White);
                //window.Draw(dude);

                FallingSegments.Clear();

                if (clock.ElapsedTime.AsMilliseconds () > 500)
                {
                    FallingShapeLocation = new Vector2f(FallingShapeLocation.X, FallingShapeLocation.Y + SEGMENT_SIZE);
                    clock.Restart();
                }

                RectangleShape[] fallingSegments = fallingShape.GetShape(FallingShapeLocation);

                if (SegmentsHaveLanded(fallingSegments))
                {
                    LandedSegments.AddRange(fallingSegments);
                    fallingShape = shapeFactory.GetNextStupidShape();
                    ResetFallingShapeLocation(window);
                }
                else
                {
                    FallingSegments.AddRange(fallingSegments);
                }

                DrawSegments(window);
            }
        }

        private static void ResetFallingShapeLocation(RenderWindow window)
        {
            FallingShapeLocation = new Vector2f(window.Size.X / 2, 0);
        }

        private static void DrawSegments(RenderWindow window)
        {
            FallingSegments.ForEach(s => window.Draw(s));
            LandedSegments.ForEach(s => window.Draw(s));

            window.Draw(floor);

            window.Display();
        }

        private static bool SegmentsHaveLanded(Shape[] fallingSegments)
        {
            bool isIntersected = false;

            isIntersected = CheckVerticalIntersetion(fallingSegments, new List<RectangleShape>() { floor });

            isIntersected |= CheckVerticalIntersetion(fallingSegments, LandedSegments);

            return isIntersected;
        }

        private static bool CheckVerticalIntersetion(IEnumerable<Shape> groupA, IEnumerable<Shape> groupB)
        {
            foreach (var segmentA in groupA)
            {
                foreach(var segmentB in groupB)
                {
                    if (segmentA.Position.Y + SEGMENT_SIZE >= segmentB.Position.Y)
                        return true;
                }
            }

            return false;
        }

        private static void ApplyGravity(List<Shape> segments)
        {
            throw new NotImplementedException();
        }

        static void KeyPressHandler(object sender, KeyEventArgs e)
        {
            Window window = (Window)sender;
            if (e.Code == Keyboard.Key.Escape)
            {
                window.Close();
            }

            switch (e.Code)
            {
                case Keyboard.Key.Escape: window.Close(); break;
                case Keyboard.Key.Up: dude.Position += new Vector2f(0, -dude.Size.Y); break;
                case Keyboard.Key.Down: dude.Position += new Vector2f(0, dude.Size.Y); break;
                case Keyboard.Key.Left: dude.Position += new Vector2f(-dude.Size.X, 0); break;
                case Keyboard.Key.Right: dude.Position += new Vector2f(dude.Size.X, 0); break;
                case Keyboard.Key.Space: fallingShape.Rotate(); break;
            }
        }
    }
}
