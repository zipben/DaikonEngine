using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Daikon.WindowManager
{
    public static class WindowManager
    {
        static RectangleShape dude;
        static RectangleShape floor;
        static Shape[] rads;
        static bool IsJumping;


        public static void Run()
        {
            VideoMode mode = new VideoMode(500, 500);
            RenderWindow window = new RenderWindow(mode, "Box Boi");

            window.Closed += (obj, e) => { window.Close(); };
            window.KeyPressed += KeyPressHandler;

            dude = new RectangleShape();
            dude.FillColor = Color.Magenta;
            dude.Size = new Vector2f(50, 50);

            
            float textWidth = dude.GetLocalBounds().Width;
            float textHeight = dude.GetLocalBounds().Height;
            float xOffset = dude.GetLocalBounds().Left;
            float yOffset = dude.GetLocalBounds().Top;
            dude.Origin = new Vector2f(textWidth / 2f + xOffset, textHeight / 2f + yOffset);
            dude.Position = new Vector2f(window.Size.X / 2f, window.Size.Y / 2f);

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
                delta = clock.Restart().AsSeconds();


                //if (!dude.GetGlobalBounds().Intersects(floor.GetGlobalBounds()))
                //{
                //    dude.Position += new Vector2f(0, 1f);
                //}


                //Game Loop
                window.DispatchEvents();
                window.Clear();
                window.Draw(dude);
                window.Draw(floor);
                window.Display();
            }
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
                case Keyboard.Key.Space: IsJumping = true; break;
            }
        }
    }
}
