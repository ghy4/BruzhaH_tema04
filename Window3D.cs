using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Drawing;

namespace ConsoleApp3
{
    class Window3D : GameWindow
    {

        private KeyboardState previousKeyboard;
        private Randomizer rando;
        private Line3D firstLine;

        // DEFAULTS
        Color DEFAULT_BKG_COLOR = Color.CornflowerBlue;

        public Window3D() : base(1280, 768, new GraphicsMode(32, 24, 0, 8))
        {
            VSync = VSyncMode.On;

            rando = new Randomizer();
            firstLine = new Line3D(rando);

            DisplayHelp();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);

            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            // set background
            GL.ClearColor(DEFAULT_BKG_COLOR);

            // set viewport
            GL.Viewport(0, 0, this.Width, this.Height);

            // set perspective
            Matrix4 perspectiva = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)this.Width / (float)this.Height, 1, 250);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspectiva);

            // set the eye
            Matrix4 eye = Matrix4.LookAt(30, 30, 30, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref eye);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            // LOGIC CODE
            KeyboardState currentKeyboard = Keyboard.GetState();
            MouseState currentMouse = Mouse.GetState();

            if (currentKeyboard[Key.Escape])
            {
                Exit();
            }

            if (currentKeyboard[Key.H] && !previousKeyboard[Key.H])
            {
                DisplayHelp();
            }

            if (currentKeyboard[Key.R] && !previousKeyboard[Key.R])
            {
                GL.ClearColor(DEFAULT_BKG_COLOR);
            }

            if (currentKeyboard[Key.B] && !previousKeyboard[Key.B])
            {
                GL.ClearColor(rando.RandomColor());
            }

            if (currentKeyboard[Key.X] && !previousKeyboard[Key.X])
            {
                firstLine.DiscoMode(rando);
            }

            if (currentKeyboard[Key.V] && !previousKeyboard[Key.V])
            {
                firstLine.ToggleVisibility();
            }


            if (currentKeyboard[Key.G] && !previousKeyboard[Key.G])
            {
                firstLine.ToggleWidth();
            }


            previousKeyboard = currentKeyboard;
            // END logic code
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            // RENDER CODE
            firstLine.Draw();

            // END render code

            SwapBuffers();
        }

        private void DisplayHelp()
        {
            Console.WriteLine("\n      MENIU");
            Console.WriteLine(" H - meniul");
            Console.WriteLine(" ESC - parasire aplicatie");
            Console.WriteLine(" R - resteaza scena la valori implicite");
            Console.WriteLine(" B - schimbare culoare de fundal");
            Console.WriteLine(" V - schimbare vizibilitate linii");
            Console.WriteLine(" G - schimbare grosime linii");
            Console.WriteLine(" X - DISCO MODE (LOL)");
        }

    }
}
