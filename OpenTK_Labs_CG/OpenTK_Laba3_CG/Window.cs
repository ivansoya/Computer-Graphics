using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using Addition.Shader;
using Addition.Camera;
using Addition.Texture;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTK_Laba3_CG
{
    public class Window : GameWindow
    {
        private readonly float[] _verticesCube =
        {           
             // Координаты        Нормаль             
            -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f, // Передняя грань
             0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
            -0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,

            -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f, // Задняя грань
             0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
            -0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
            -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,

            -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f, // Левая грань
            -0.5f,  0.5f, -0.5f, -1.0f,  0.0f,  0.0f,
            -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,
            -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,
            -0.5f, -0.5f,  0.5f, -1.0f,  0.0f,  0.0f,
            -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,

             0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f, // Правая грань
             0.5f,  0.5f, -0.5f,  1.0f,  0.0f,  0.0f,
             0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,
             0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,
             0.5f, -0.5f,  0.5f,  1.0f,  0.0f,  0.0f, 
             0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,

            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f, // Нижняя грань
             0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f, 
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,

            -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f, // Верхняя грань
             0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,
            -0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,
            -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f
        };

        private readonly float[] _verticesPyramide =
            {            
             // Координаты        Нормаль          
            -0.5f, -0.5f, -0.5f,  0.0f,  0.24f, -0.97f, // Передняя грань
             0.5f, -0.5f, -0.5f,  0.0f,  0.24f, -0.97f,
             0.0f,  1.5f,  0.0f,  0.0f,  0.24f, -0.97f,

             0.5f, -0.5f, -0.5f,  0.97f,  0.24f,  0.0f, // Правая грань
             0.5f, -0.5f,  0.5f,  0.97f,  0.24f,  0.0f,
             0.0f,  1.5f,  0.0f,  0.97f,  0.24f,  0.0f,

             0.5f, -0.5f,  0.5f,  0.0f,  0.24f,  0.97f, // Задняя грань
            -0.5f, -0.5f,  0.5f,  0.0f,  0.24f,  0.97f,
             0.0f,  1.5f,  0.0f,  0.0f,  0.24f,  0.97f,

            -0.5f, -0.5f, -0.5f, -0.97f,  0.24f,  0.0f, // Левая грань
            -0.5f, -0.5f,  0.5f, -0.97f,  0.24f,  0.0f,
             0.0f,  1.5f,  0.0f, -0.97f,  0.24f,  0.0f,

            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f, // Нижняя грань
             0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,
        };

        private readonly Vector3[] _cubePosition =
        {
            new Vector3(0.0f, 0.0f, 0.0f),
            new Vector3(0.5f, 2.0f, -1.0f),
            new Vector3(2.5f, 1.5f, 0.5f),
            new Vector3(2.0f, -2.0f, 1.0f),
            new Vector3(4.0f, 0.0f, 0.5f),
            new Vector3(-2.0f, 1.7f, 2.0f), 
            new Vector3(0.15f, 5.7f, -1.1f)
        };

        private readonly Vector3[] _pyramidePosition =
        {
            new Vector3(0.5f, -3.0f, 1.0f),
            new Vector3(4.0f, 2.0f, 1.5f),
            new Vector3(-2.0f, -2.0f, -1.0f),
            new Vector3(-5.0f, 0.0f, 2.0f),
            new Vector3(0.7f, 3.5f, -1.5f)
        };

        private readonly Vector3[] _spherePosition =
        {
            new Vector3(-1.0f, 2.0f, -1.0f),
            new Vector3(-3.0f, 0.0f, -2.5f),
            new Vector3(-1.0f, 2.0f, 1.5f),
            new Vector3(3.0f, 0.0f, -3.0f),
            new Vector3(-2.0f, 3.0f, -4.0f)
        };

        private readonly Vector3 _skyBoxPosition = new Vector3(0.0f);

        private float[] _verticesSphere;

        private Vector3 _lightPos = new Vector3(0.5f, 2.5f, 1.0f);

        private Shader _lightingShader;
        private Shader _sourceOfLightShader;
        private Shader _depthShader;

        private Camera _camera;

        private float _time = 0.0f;
        bool _reverse = false;

        private Texture _depthMap;

        private int _vertexCubeBufferObject;
        private int _vertexPyramideBufferObject;
        private int _vertexSphereBufferObject;

        private int _fboDepthMap;

        private int _vaoLight;
        private int _vaoCube;
        private int _vaoPyramide;
        private int _vaoSphere;

        private int _shadowWidth = 1024;
        private int _shadowHeight = 1024;

        Vector2 _firstMousePosition;

        public Window(int width, int height, string title)
            : base(width, height, GraphicsMode.Default, title)
        {
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(Color4.Black);
            GL.Enable(EnableCap.DepthTest);

            _lightingShader = new Shader("../../Shaders/shader.vert", "../../Shaders/pointLight.frag");
            _sourceOfLightShader = new Shader("../../Shaders/shader.vert", "../../Shaders/shader.frag");
            _depthShader = new Shader("../../Shaders/shaderShadow.vert", "../../Shaders/shaderShadow.frag", "../../Shaders/shaderShadow.geom");

            GL.GenFramebuffers(1, out _fboDepthMap);

            _depthMap = new Texture(_shadowWidth, _shadowHeight, "CUBE_MAP_DEPTH");

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, _fboDepthMap);
            GL.FramebufferTexture(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, _depthMap.Handle, 0);

            GL.DrawBuffer(DrawBufferMode.None);
            GL.ReadBuffer(ReadBufferMode.None);

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

            _lightingShader.Use();
            _lightingShader.SetInt("depthMap", 0);

            createSphereVertices(0.5f, 250);

            // Инициализация VBO
            // Необходимо для занесения данных о вершинах в GPU
            _vertexCubeBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexCubeBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _verticesCube.Length * sizeof(float), _verticesCube, BufferUsageHint.StaticDraw);

            _vertexPyramideBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexPyramideBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _verticesPyramide.Length * sizeof(float), _verticesPyramide, BufferUsageHint.StaticDraw);

            _vertexSphereBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexSphereBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _verticesSphere.Length * sizeof(float), _verticesSphere, BufferUsageHint.StaticDraw);      

            _vaoCube = GL.GenVertexArray();
            GL.BindVertexArray(_vaoCube);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexCubeBufferObject);

            var cubeLocation = _lightingShader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(cubeLocation);
            GL.VertexAttribPointer(cubeLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);

            var normalLocation = _lightingShader.GetAttribLocation("aNormal");
            GL.EnableVertexAttribArray(normalLocation);
            GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));

            _vaoPyramide = GL.GenVertexArray();
            GL.BindVertexArray(_vaoPyramide);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexPyramideBufferObject);

            var pyramideLocation = _lightingShader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(pyramideLocation);
            GL.VertexAttribPointer(pyramideLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);

            var pyramideNormalLocation = _lightingShader.GetAttribLocation("aNormal");
            GL.EnableVertexAttribArray(pyramideNormalLocation);
            GL.VertexAttribPointer(pyramideNormalLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));

            _vaoSphere = GL.GenVertexArray();
            GL.BindVertexArray(_vaoSphere);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexSphereBufferObject);

            var sphereLocation = _lightingShader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(sphereLocation);
            GL.VertexAttribPointer(sphereLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);

            var normalSphereLocation = _lightingShader.GetAttribLocation("aNormal");
            GL.EnableVertexAttribArray(normalSphereLocation);
            GL.VertexAttribPointer(normalSphereLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));

            _vaoLight = GL.GenVertexArray();
            GL.BindVertexArray(_vaoLight);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexCubeBufferObject);

            var lightLocation = _sourceOfLightShader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(lightLocation);
            GL.VertexAttribPointer(lightLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);

            _camera = new Camera(Vector3.UnitZ * 3, Width / (float)Height);

            base.OnLoad(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            if (_time >= 3)
                _reverse = true;
            else if (_time <= -3)
                _reverse = false;

            if (_reverse == false)
                _time += 0.02f;
            else
                _time -= 0.02f;
            _lightPos.Z = _time;

            //Рендер Кубической текустуры глубины
            float nearPlane = 0.1f;
            float farPlane = 25.0f;
            Matrix4 shadowProj = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90.0f), (float)_shadowWidth / _shadowHeight, nearPlane, farPlane);
            List<Matrix4> shadowMatrix = new List<Matrix4>();
            shadowMatrix.Add(Matrix4.LookAt(_lightPos, _lightPos + new Vector3( 1.0f, 0.0f,  0.0f), new Vector3(0.0f, -1.0f,  0.0f)) * shadowProj);
            shadowMatrix.Add(Matrix4.LookAt(_lightPos, _lightPos + new Vector3(-1.0f, 0.0f,  0.0f), new Vector3(0.0f, -1.0f,  0.0f)) * shadowProj);
            shadowMatrix.Add(Matrix4.LookAt(_lightPos, _lightPos + new Vector3( 0.0f, 1.0f,  0.0f), new Vector3(0.0f,  0.0f,  1.0f)) * shadowProj);
            shadowMatrix.Add(Matrix4.LookAt(_lightPos, _lightPos + new Vector3( 0.0f,-1.0f,  0.0f), new Vector3(0.0f,  0.0f, -1.0f)) * shadowProj);
            shadowMatrix.Add(Matrix4.LookAt(_lightPos, _lightPos + new Vector3( 0.0f, 0.0f,  1.0f), new Vector3(0.0f, -1.0f,  0.0f)) * shadowProj);
            shadowMatrix.Add(Matrix4.LookAt(_lightPos, _lightPos + new Vector3( 0.0f, 0.0f, -1.0f), new Vector3(0.0f, -1.0f , 0.0f)) * shadowProj);

            GL.Viewport(0, 0, _shadowWidth, _shadowHeight);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, _fboDepthMap);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            _depthShader.Use();

            for (int i = 0; i < 6; i++)
               _depthShader.SetMatrix4($"shadowMatrices[{i}]", shadowMatrix[i]);
            _depthShader.SetVector3("lightPosition", _lightPos);
            _depthShader.SetFloat("farPlane", farPlane);

            renderScene(_depthShader, false);

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

            GL.Viewport(0, 0, Width, Height);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            _lightingShader.Use();

            _lightingShader.SetMatrix4("view", _camera.GetViewMatrix());
            _lightingShader.SetMatrix4("projection", _camera.GetProjectionMatrix());

            _lightingShader.SetVector3("viewPos", _camera.Position);

            _lightingShader.SetFloat("farPlane", farPlane);

            //Настройка света

            _lightingShader.SetFloat("light.constant", 1.0f);
            _lightingShader.SetFloat("light.linear", 0.09f);
            _lightingShader.SetFloat("light.quadratic", 0.032f);

            _lightingShader.SetVector3("light.position", _lightPos);
            _lightingShader.SetVector3("light.ambient", new Vector3(0.2f, 0.2f, 0.2f));
            _lightingShader.SetVector3("light.diffuse", new Vector3(0.5f, 0.5f, 0.5f));
            _lightingShader.SetVector3("light.specular", new Vector3(1.0f, 1.0f, 1.0f));
            _lightingShader.SetVector3("light.color", new Vector3(1.0f, 1.0f, 1.0f));

            _depthMap.UseCube();

            renderScene(_lightingShader, true);

            GL.BindVertexArray(_vaoLight);

            _sourceOfLightShader.Use();

            Matrix4 lightMatrix = Matrix4.Identity;
            lightMatrix *= Matrix4.CreateScale(0.2f);
            lightMatrix *= Matrix4.CreateTranslation(_lightPos);

            _sourceOfLightShader.SetMatrix4("model", lightMatrix);
            _sourceOfLightShader.SetMatrix4("view", _camera.GetViewMatrix());
            _sourceOfLightShader.SetMatrix4("projection", _camera.GetProjectionMatrix());

            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);

            SwapBuffers();

            base.OnRenderFrame(e);
        }

        private void renderScene(Shader shader, bool isUsualRender)
        {

            GL.BindVertexArray(_vaoCube);

            if (isUsualRender == true)
            {
                shader.SetVector3("objectColor", new Vector3(1.0f, 1.0f, 1.0f));
                shader.SetInt("isReversed", 1);

                shader.SetVector3("material.ambient", new Vector3(0.2f, 0.2f, 0.2f));
                shader.SetVector3("material.diffuse", new Vector3(1.0f, 1.0f, 1.0f));
                shader.SetVector3("material.specular", new Vector3(1.0f, 1.0f, 1.0f));
                shader.SetFloat("material.shininess", 16f);
            }

            Matrix4 boxMatrix = Matrix4.Identity;
            boxMatrix *= Matrix4.CreateTranslation(_skyBoxPosition);
            boxMatrix *= Matrix4.CreateScale(12.0f);
            shader.SetMatrix4("model", boxMatrix);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);

            if (isUsualRender == true)
            {
                shader.SetVector3("objectColor", new Vector3(0.26f, 1.0f, 0.0f));
                shader.SetInt("isReversed", 0);

                shader.SetVector3("material.ambient", new Vector3(0.0f, 0.5f, 0.0f));
                shader.SetVector3("material.diffuse", new Vector3(0.0f, 1.0f, 0.0f));
                shader.SetVector3("material.specular", new Vector3(0.5f, 0.5f, 0.5f));
                shader.SetFloat("material.shininess", 8f);
            }

            for (int i = 0; i < _cubePosition.Length; i++)
            {
                Matrix4 cubeMatrix = Matrix4.Identity;
                cubeMatrix *= Matrix4.CreateTranslation(_cubePosition[i]);
                float angle = 15.0f * i;
                cubeMatrix *= Matrix4.CreateFromAxisAngle(new Vector3(0.8f, 0.4f, 0.2f), angle);
                shader.SetMatrix4("model", cubeMatrix);

                GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
            }

            GL.BindVertexArray(_vaoSphere);

            if (isUsualRender == true)
            {
                shader.SetVector3("objectColor", new Vector3(0.0f, 1.0f, 1.0f));

                shader.SetVector3("material.ambient", new Vector3(0.0f, 0.4f, 0.4f));
                shader.SetVector3("material.diffuse", new Vector3(0.0f, 0.4f, 0.4f));
                shader.SetVector3("material.specular", new Vector3(1.0f, 1.0f, 1.0f));
                shader.SetFloat("material.shininess", 2f);
            }

            for (int i = 0; i < _spherePosition.Length; i++)
            {
                Matrix4 sphereMatrix = Matrix4.Identity;
                sphereMatrix *= Matrix4.CreateTranslation(_spherePosition[i]);
                float angle = 25.0f * i;
                sphereMatrix *= Matrix4.CreateFromAxisAngle(new Vector3(0.8f, 0.4f, 0.2f), angle);
                shader.SetMatrix4("model", sphereMatrix);
                GL.DrawArrays(PrimitiveType.Triangles, 0, _verticesSphere.Length / 6);
            }

            GL.BindVertexArray(_vaoPyramide);

            if (isUsualRender == true)
            {
                shader.SetVector3("objectColor", new Vector3(1.0f, 0.5f, 0.31f));

                shader.SetVector3("material.ambient", new Vector3(1.0f, 0.0f, 0.0f));
                shader.SetVector3("material.diffuse", new Vector3(1.0f, 0.0f, 0.0f));
                shader.SetVector3("material.specular", new Vector3(0.0f, 0.0f, 0.0f));
                shader.SetFloat("material.shininess", 8f);
            }

            for (int i = 0; i < _pyramidePosition.Length; i++)
            {
                Matrix4 pyramideMatrix = Matrix4.Identity;
                pyramideMatrix *= Matrix4.CreateTranslation(_pyramidePosition[i]);
                float angle = 15.0f * i;
                pyramideMatrix *= Matrix4.CreateFromAxisAngle(new Vector3(0.1f, 0.3f, 0.2f), angle);
                shader.SetMatrix4("model", pyramideMatrix);

                GL.DrawArrays(PrimitiveType.Triangles, 0, 18);
            }
        }

        private void createSphereVertices(float r, int total)
        {
            int size = (total + 1) * (total + 1) * 2 * 3 * 3 * 3; // число треугольников, количество точек, координаты точек, нормали
            _verticesSphere = new float[size];
            Vector3[,] globe = new Vector3[total + 1, total + 1];
            int vert = 0;
            for (int j = 0; j < total + 1; j++)
            {
                float lat = 0 + j * (float)Math.PI / total;
                for (int i = 0; i < total + 1; i++)
                {
                    float lon = 0 + i * 2 * (float)Math.PI / total;
                    float x = r * (float)Math.Sin(lat) * (float)Math.Cos(lon);
                    float y = r * (float)Math.Cos(lat);
                    float z = r * (float)Math.Sin(lat) * (float)Math.Sin(lon);
                    globe[i, j] = new Vector3(x, y, z);
                }
            }

            for (int i = 0; i < total; i++)
            {
                for (int j = 0; j < total; j++)
                {

                    Vector3[] g = {
                        globe[i, j],
                        globe[i + 1, j],
                        globe[i + 1, j + 1],
                    };

                    for (int n = 0; n < g.Length; n++) 
                    {
                        //Координаты точек
                        _verticesSphere[vert++] = g[n].X;
                        _verticesSphere[vert++] = g[n].Y;
                        _verticesSphere[vert++] = g[n].Z;

                        Vector3 normal = Vector3.Normalize(new Vector3(g[n].X, g[n].Y, g[n].Z));

                        // Нормали
                        _verticesSphere[vert++] = normal.X;
                        _verticesSphere[vert++] = normal.Y;
                        _verticesSphere[vert++] = normal.Z;
                    }

                    Vector3[] g2 = {
                        globe[i, j],
                        globe[i, j + 1],
                        globe[i + 1, j + 1],
                    };

                    for (int n = 0; n < g2.Length; n++)
                    {
                        //Координаты точек
                        _verticesSphere[vert++] = g2[n].X;
                        _verticesSphere[vert++] = g2[n].Y;
                        _verticesSphere[vert++] = g2[n].Z;

                        Vector3 normal = Vector3.Normalize(new Vector3(g2[n].X, g2[n].Y, g2[n].Z));

                        // Нормали
                        _verticesSphere[vert++] = normal.X;
                        _verticesSphere[vert++] = normal.Y;
                        _verticesSphere[vert++] = normal.Z;
                    }

                }
            }
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (!Focused)
            {
                return;
            }

            var input = Keyboard.GetState();

            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
            }

            const float cameraSpeed = 1.5f;
            const float sensitivity = 0.2f;

            if (input.IsKeyDown(Key.W))
            {
                _camera.Position += _camera.Front * cameraSpeed * (float)e.Time; // Forward
            }
            if (input.IsKeyDown(Key.S))
            {
                _camera.Position -= _camera.Front * cameraSpeed * (float)e.Time; // Backwards
            }
            if (input.IsKeyDown(Key.A))
            {
                _camera.Position -= _camera.Right * cameraSpeed * (float)e.Time; // Left
            }
            if (input.IsKeyDown(Key.D))
            {
                _camera.Position += _camera.Right * cameraSpeed * (float)e.Time; // Right
            }
            if (input.IsKeyDown(Key.Space))
            {
                _camera.Position += _camera.Up * cameraSpeed * (float)e.Time; // Up
            }
            if (input.IsKeyDown(Key.LShift))
            {
                _camera.Position -= _camera.Up * cameraSpeed * (float)e.Time; // Down
            }
            if (input.IsKeyDown(Key.Y))
            {
                Console.WriteLine(_camera.Position); // Получение координат
            }

            var mouse = Mouse.GetState();

            if (mouse.IsButtonDown(MouseButton.Left))
            {
                var deltaX = mouse.X - _firstMousePosition.X;
                var deltaY = mouse.Y - _firstMousePosition.Y;
                _firstMousePosition = new Vector2(mouse.X, mouse.Y);

                _camera.Yaw += deltaX * sensitivity;
                _camera.Pitch -= deltaY * sensitivity;
            }

            if (mouse.IsButtonDown(MouseButton.Right))
            {
                var deltaX = mouse.X - _firstMousePosition.X;
                var deltaY = mouse.Y - _firstMousePosition.Y;
                _firstMousePosition = new Vector2(mouse.X, mouse.Y);

                if (deltaX != 0)
                    _camera.Position += _camera.Right * deltaX * (float)e.Time / 3;
                if (deltaY != 0)
                    _camera.Position -= _camera.Up * deltaY * (float)e.Time / 3;
            }

            _firstMousePosition = new Vector2(mouse.X, mouse.Y);

            base.OnUpdateFrame(e);
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            base.OnResize(e);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            _camera.Fov -= e.DeltaPrecise;
            _camera.AspectRatio = Width / (float)Height;
            base.OnMouseWheel(e);
        }

        protected override void OnUnload(EventArgs e)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            GL.DeleteBuffer(_vertexCubeBufferObject);
            GL.DeleteBuffer(_vertexPyramideBufferObject);
            GL.DeleteBuffer(_vertexSphereBufferObject);

            GL.DeleteFramebuffer(_fboDepthMap);
            GL.DeleteVertexArray(_vaoCube);
            GL.DeleteVertexArray(_vaoLight);
            GL.DeleteVertexArray(_vaoSphere);
            GL.DeleteVertexArray(_vaoPyramide);

            GL.DeleteProgram(_lightingShader.Handle);
            GL.DeleteProgram(_sourceOfLightShader.Handle);
            GL.DeleteProgram(_depthShader.Handle);

            base.OnUnload(e);
        }

    }
}