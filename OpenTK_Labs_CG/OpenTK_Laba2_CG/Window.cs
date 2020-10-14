using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using Addition.Shader;
using System;

namespace OpenTK_Laba2_CG
{
    public class Window : GameWindow
    {
        private readonly float[] _vertices =
{
            // Координаты        Цвет
             0.5f,  0.5f, -0.5f, 1.0f, 1.0f, 1.0f, // Передняя справа внизу (0)
             0.5f, -0.5f, -0.5f, 1.0f, 0.0f, 1.0f, // Задняя справа внизу (1)
            -0.5f,  0.5f, -0.5f, 0.0f, 0.5f, 1.0f, // Передняя слева внизу (2)
            -0.5f, -0.5f, -0.5f, 0.0f, 1.0f, 1.0f, // Задняя слева внизу (3)
             0.5f,  0.5f,  0.5f, 1.0f, 0.5f, 1.0f, // Передняя справа вверху (4)
             0.5f, -0.5f,  0.5f, 1.0f, 0.0f, 0.5f, // Задняя справа вверху (5)
            -0.5f,  0.5f,  0.5f, 0.5f, 0.5f, 1.5f, // Передняя слева вверху (6)
            -0.5f, -0.5f,  0.5f, 0.0f, 1.0f, 0.0f  // Задняя слева вверху (7)
        };

        private readonly uint[] _indices =
        {
            0, 1, 3,
            1, 2, 3,
            0, 1, 4,
            1, 4, 5,
            1, 3, 5,
            3, 7, 5,
            0, 2, 4,
            2, 6, 4,
            3, 2, 6,
            3, 7, 6,
            6, 7, 5,
            6, 4, 5
        };

        private Shader _shader;

        private double _time;

        private int _vertexBufferObject;
        private int _elementBufferObject;
        private int _vertexArrayObject;

        private Matrix4 _view;
        private Matrix4 _projection;

        public Window(int width, int height, string title)
            : base(width, height, GraphicsMode.Default, title)
        {
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(Color4.Black);
            GL.Enable(EnableCap.DepthTest);

            // Инициализация VBO
            // Необходимо для занесения данных о вершинах в GPU
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            // Инициализация EBO
            // Необходимо для отрисовки по вершинам
            _elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

            _shader = new Shader("../../Shaders/shader.vert", "../../Shaders/shader.frag");
            _shader.Use();

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexArrayObject);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);

            var vertexLocation = _shader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(vertexLocation);
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);

            var color = _shader.GetAttribLocation("aColor");
            GL.EnableVertexAttribArray(color);
            GL.VertexAttribPointer(color, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));

            _view = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);

            _projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), Width / (float)Height, 0.1f, 100.0f);

            base.OnLoad(e);
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

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            _time += 5.0 * e.Time;

            GL.BindVertexArray(_vertexArrayObject);

            _shader.Use();

            var model = Matrix4.Identity * Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(_time))
                       * Matrix4.CreateRotationY((float)MathHelper.DegreesToRadians(_time - 6.0));

            _shader.SetMatrix4("model", model);
            _shader.SetMatrix4("view", _view);
            _shader.SetMatrix4("projection", _projection);

            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);

            SwapBuffers();

            base.OnRenderFrame(e);
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            base.OnResize(e);
        }

        protected override void OnUnload(EventArgs e)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            GL.DeleteBuffer(_vertexBufferObject);
            GL.DeleteVertexArray(_vertexArrayObject);

            GL.DeleteProgram(_shader.Handle);

            base.OnUnload(e);
        }

    }
}
