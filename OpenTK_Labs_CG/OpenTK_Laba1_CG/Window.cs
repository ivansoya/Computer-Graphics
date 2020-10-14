using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;

namespace OpenTK_Laba1_CG
{
    public class Window : GameWindow
    {

        public Window(int width, int height, string title)
            : base(width, height, GraphicsMode.Default, title)
        {

        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            var input = Keyboard.GetState();

            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
            }

            base.OnUpdateFrame(e);
        }
    }
}
