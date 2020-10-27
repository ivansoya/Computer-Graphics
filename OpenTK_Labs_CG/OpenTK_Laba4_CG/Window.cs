using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using Addition.Shader;
using Addition.Camera;
using Addition.Texture;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using System.Linq;

namespace OpenTK_Laba4_CG
{
    public class Window : GameWindow
    {
        private readonly float[] _verticesCube =
        {           
            // Точки              Нормали              Текстура
            -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 0.0f, // Передняя грань
             0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 0.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 1.0f,
            -0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 0.0f,

            -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 0.0f, // Задняя грань
             0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 0.0f,
            -0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 1.0f,

            -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 0.0f, // Левая грань
            -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
            -0.5f,  0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 1.0f,
            -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
            -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 0.0f,
            -0.5f, -0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 0.0f,

             0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 0.0f, // Правая грань
             0.5f,  0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 0.0f,

            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f, // Нижняя грань
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 0.0f,
             0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 0.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 0.0f,

            -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 1.0f, // Верхняя
             0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 0.0f,
            -0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 0.0f,
            -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 1.0f
        };

        private readonly float[] _verticesPyramide =
            {            
             // Координаты        Нормаль              Текстура 
            -0.5f, -0.5f, -0.5f,  0.0f,  0.24f, -0.97f, 0.0f, 1.0f, // Передняя грань
             0.5f, -0.5f, -0.5f,  0.0f,  0.24f, -0.97f, 1.0f, 1.0f,
             0.0f,  1.5f,  0.0f,  0.0f,  0.24f, -0.97f, 0.5f, 2.0f,

             0.5f, -0.5f, -0.5f,  0.97f,  0.24f,  0.0f, 0.0f, 1.0f, // Правая грань
             0.5f, -0.5f,  0.5f,  0.97f,  0.24f,  0.0f, 1.0f, 1.0f,
             0.0f,  1.5f,  0.0f,  0.97f,  0.24f,  0.0f, 0.5f, 2.0f,

             0.5f, -0.5f,  0.5f,  0.0f,  0.24f,  0.97f, 0.0f, 1.0f, // Задняя грань
            -0.5f, -0.5f,  0.5f,  0.0f,  0.24f,  0.97f, 1.0f, 1.0f,
             0.0f,  1.5f,  0.0f,  0.0f,  0.24f,  0.97f, 0.5f, 2.0f,

            -0.5f, -0.5f, -0.5f, -0.97f,  0.24f,  0.0f, 0.0f, 1.0f, // Левая грань
             0.0f,  1.5f,  0.0f, -0.97f,  0.24f,  0.0f, 0.5f, 2.0f,
            -0.5f, -0.5f,  0.5f, -0.97f,  0.24f,  0.0f, 1.0f, 1.0f,

            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f, // Нижняя грань
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 0.0f,
             0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 0.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 0.0f
        };

        private readonly float[] _vericesQuad = {
            // positions        // texture Coords
            -1.0f,  1.0f, 0.0f, 0.0f, 1.0f,
            -1.0f, -1.0f, 0.0f, 0.0f, 0.0f,
             1.0f,  1.0f, 0.0f, 1.0f, 1.0f,
             1.0f, -1.0f, 0.0f, 1.0f, 0.0f,
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

        private Vector3[] _lightPos =
        {
            new Vector3(0.0f, 1.5f, 0.0f),
            new Vector3(-3.5f, -1.0f, 3.5f),
            new Vector3(4.0f, 4.5f, -4.8f)
        };

        private Vector3[] _lightColor =
        {
            new Vector3(1.0f, 1.0f, 1.0f),
            new Vector3(0.0f, 1.0f, 1.0f),
            new Vector3(1.76f, 0.0f, 0.0f)
        };

        private float[] _lightIntensive = { 1.0f, 0.7f, 0.7f };

        private float[] _lightFarPlane = { 25.0f, 25.0f, 25.0f };

        //private float _exposure = 2.0f;

        private Shader _lightingShader;
        private Shader _sourceOfLightShader;
        private Shader _depthShader;
        private Shader _blurShader;
        private Shader _hdrShader;

        private Camera _camera;

        private float _time = 0.0f;
        bool _reverse = false;

        private List<Texture> _depthMaps;
        private List<Texture> _colorMaps;
        private List<Texture> _pingPongMaps;

        public Dictionary<string, Texture> _texturesList = new Dictionary<string, Texture>
        {
            { "emerald_block",   null},
            { "emerald_block_s", null},
            { "iron_block",      null},
            { "iron_block_s",    null},
            { "crystal",         null},
            { "crystal_s",       null},
            { "bricks",          null},
            { "bricks_s",        null}
        };

        private int _vertexCubeBufferObject;
        private int _vertexPyramideBufferObject;
        private int _vertexSphereBufferObject;
        private int _vertexQuadBufferObject;

        private int _rboDepth;

        private List<int> _fboDepthMaps;
        private List<int> _fboPingPongs;
        private int _fboHDR;

        private int _vaoCube;
        private int _vaoPyramide;
        private int _vaoSphere;
        private int _vaoQuad;

        private int _shadowWidth = 1024;
        private int _shadowHeight = 1024;

        Vector2 _firstMousePosition;
        private float _rotationAngleY = 0.0f;
        private float _rotationAngleX = 0.0f;

        public Window(int width, int height, string title)
            : base(width, height, GraphicsMode.Default, title)
        {
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(Color4.Black);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);

            _lightingShader = new Shader("../../Shaders/shader.vert", "../../Shaders/pointLight.frag");
            _sourceOfLightShader = new Shader("../../Shaders/shader.vert", "../../Shaders/shader.frag");
            _depthShader = new Shader("../../Shaders/depthShader.vert", "../../Shaders/depthShader.frag", "../../Shaders/depthShader.geom");
            _blurShader = new Shader("../../Shaders/hdrShader.vert", "../../Shaders/blur.frag");
            _hdrShader = new Shader("../../Shaders/hdrShader.vert", "../../Shaders/hdrShader.frag");

            initializeTextures();

            _depthMaps = new List<Texture>();
            _fboDepthMaps = new List<int>();
            _fboPingPongs = new List<int>();

            // Инициализация кубических карт глубины для теневого покрытия
            for (int i = 0; i < _lightPos.Length; i++)
            {
                int fbo = GL.GenFramebuffer();
                _fboDepthMaps.Add(fbo);
                GL.BindFramebuffer(FramebufferTarget.Framebuffer, _fboDepthMaps[i]);

                _depthMaps.Add(new Texture(_shadowWidth, _shadowHeight, "CUBE_MAP_DEPTH"));
                GL.FramebufferTexture(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, _depthMaps[i].Handle, 0);

                // Checking status
                var status = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);

                if (status != FramebufferErrorCode.FramebufferComplete)
                {
                    Console.WriteLine("FB error, status: 0x%x", status);
                }

                GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            }

            _fboHDR = GL.GenFramebuffer();
            _colorMaps = new List<Texture>();
            _pingPongMaps = new List<Texture>();

            _rboDepth = GL.GenRenderbuffer();
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, _rboDepth);
            GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferStorage.DepthComponent, Width, Height);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, _fboHDR);
            for (int i = 0; i < 2; i++)
            {
                _colorMaps.Add(new Texture(Width, Height, "HDR_MAP"));
                GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0 + i, TextureTarget.Texture2D, _colorMaps[i].Handle, 0);
            }
            GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, RenderbufferTarget.Renderbuffer, _rboDepth);

            DrawBuffersEnum[] attachments = { DrawBuffersEnum.ColorAttachment0, DrawBuffersEnum.ColorAttachment1 };
            GL.DrawBuffers(attachments.Length, attachments);

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

            for (int i = 0; i < 2; i++)
            {
                _fboPingPongs.Add(GL.GenFramebuffer());
                GL.BindFramebuffer(FramebufferTarget.Framebuffer, _fboPingPongs[i]);
                _pingPongMaps.Add(new Texture(Width, Height, "HDR_MAP"));
                GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, _pingPongMaps[i].Handle, 0);
            }

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

            _lightingShader.Use();
            _lightingShader.SetInt("material.diffuseTexture", 0);
            _lightingShader.SetInt("material.specularTexture", 1);
            for (int i = 0; i < _lightPos.Length; i++) {
                _lightingShader.SetInt($"shadowMap[{i}]", 2 + i);
            }

            _blurShader.Use();
            _blurShader.SetInt("lightMap", 0);
            _hdrShader.Use();
            _hdrShader.SetInt("hdrBuffer", 0);
            _hdrShader.SetInt("bloom", 1);

            createSphereVertices(0.5f, 50);

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

            _vertexQuadBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexQuadBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vericesQuad.Length * sizeof(float), _vericesQuad, BufferUsageHint.StaticDraw);

            //Инициализация VAO
            _vaoCube = initializeVAO(_vaoCube, _vertexCubeBufferObject, 3, 8, 3);
            _vaoPyramide = initializeVAO(_vaoPyramide, _vertexPyramideBufferObject, 3, 8, 3);
            _vaoSphere = initializeVAO(_vaoSphere, _vertexSphereBufferObject, 3, 8, 3);
            _vaoQuad = initializeVAO(_vaoQuad, _vertexQuadBufferObject, 3, 5, 2);

            _camera = new Camera(Vector3.UnitZ * 3, Width / (float)Height);

            base.OnLoad(e);
        }

        private void initializeTextures()
        {
            foreach (string pair in _texturesList.Keys.ToArray())
            {
                string path = "../../Textures/" + pair + ".png";
                _texturesList[pair] = new Texture(path);
            }
        }

        private int initializeVAO(int vao, int vbo, int step, int size, int numberOfLocations)
        {
            vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);

            for (int i = 0; i < numberOfLocations; i++) {
                GL.EnableVertexAttribArray(i);
                int a = step;
                if (i == numberOfLocations - 1) {
                    a = step - 1 ;
                }
                GL.VertexAttribPointer(i, a, VertexAttribPointerType.Float, false, size * sizeof(float), i * step * sizeof(float));
            }
            return vao;
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
            _lightPos[0].Z = _time;

            //--------------------------------------
            //Рендер Кубической текустуры глубины
            //--------------------------------------

            GL.Viewport(0, 0, _shadowWidth, _shadowHeight);

            List<Matrix4> shadowMatrix = new List<Matrix4>();
            float nearPlane = _camera.NearPlane;

            GL.CullFace(CullFaceMode.Back);

            for (int i = 0; i < _lightPos.Length; i++)
            {
                GL.BindFramebuffer(FramebufferTarget.Framebuffer, _fboDepthMaps[i]);
                GL.Clear(ClearBufferMask.DepthBufferBit);
                Matrix4 shadowProj = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90.0f), (float)_shadowWidth / _shadowHeight, nearPlane, _lightFarPlane[i]);
                shadowMatrix.Add(Matrix4.LookAt(_lightPos[i], _lightPos[i] + new Vector3(1.0f, 0.0f, 0.0f), new Vector3(0.0f, -1.0f, 0.0f)) * shadowProj);
                shadowMatrix.Add(Matrix4.LookAt(_lightPos[i], _lightPos[i] + new Vector3(-1.0f, 0.0f, 0.0f), new Vector3(0.0f, -1.0f, 0.0f)) * shadowProj);
                shadowMatrix.Add(Matrix4.LookAt(_lightPos[i], _lightPos[i] + new Vector3(0.0f, 1.0f, 0.0f), new Vector3(0.0f, 0.0f, 1.0f)) * shadowProj);
                shadowMatrix.Add(Matrix4.LookAt(_lightPos[i], _lightPos[i] + new Vector3(0.0f, -1.0f, 0.0f), new Vector3(0.0f, 0.0f, -1.0f)) * shadowProj);
                shadowMatrix.Add(Matrix4.LookAt(_lightPos[i], _lightPos[i] + new Vector3(0.0f, 0.0f, 1.0f), new Vector3(0.0f, -1.0f, 0.0f)) * shadowProj);
                shadowMatrix.Add(Matrix4.LookAt(_lightPos[i], _lightPos[i] + new Vector3(0.0f, 0.0f, -1.0f), new Vector3(0.0f, -1.0f, 0.0f)) * shadowProj);

                _depthShader.Use();

                for (int face = 0; face < 6; face++)
                    _depthShader.SetMatrix4($"shadowMatrices[{face}]", shadowMatrix[face]);
                _depthShader.SetVector3("lightPosition", _lightPos[i]);
                _depthShader.SetFloat("farPlane", _lightFarPlane[i]);

                renderScene(_depthShader, false);

                shadowMatrix.Clear();
            }

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

            //--------------------------------------
            //Рендер основной сцены
            //--------------------------------------
            GL.Viewport(0, 0, Width, Height);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, _fboHDR);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            _lightingShader.Use();

            _lightingShader.SetMatrix4("view", _camera.GetViewMatrix());
            _lightingShader.SetMatrix4("projection", _camera.GetProjectionMatrix());

            _lightingShader.SetVector3("viewPos", _camera.Position);

            //Настройка света
            _lightingShader.SetInt("lightNumber", _lightPos.Length);

            for (int i = 0; i < _lightPos.Length; i++)
            {
                _lightingShader.SetFloat($"light[{i}].constant", 1.0f);
                _lightingShader.SetFloat($"light[{i}].linear", 0.09f);
                _lightingShader.SetFloat($"light[{i}].quadratic", 0.032f);

                _lightingShader.SetVector3($"light[{i}].position", _lightPos[i]);
                _lightingShader.SetVector3($"light[{i}].ambient", new Vector3(0.2f, 0.2f, 0.2f));
                _lightingShader.SetVector3($"light[{i}].diffuse", new Vector3(0.5f, 0.5f, 0.5f));
                _lightingShader.SetVector3($"light[{i}].specular", new Vector3(1.0f, 1.0f, 1.0f));
                _lightingShader.SetVector3($"light[{i}].color", _lightColor[i]);

                _lightingShader.SetFloat($"light[{i}].farPlane", _lightFarPlane[i]);
                _lightingShader.SetFloat($"light[{i}].intensive", _lightIntensive[i]);

                _depthMaps[i].UseCube(TextureUnit.Texture2 + i);
            }

            renderScene(_lightingShader, true);

            //--------------------------------------
            // Рендер источников света
            //--------------------------------------

            GL.BindVertexArray(_vaoSphere);
            _sourceOfLightShader.Use();

            _sourceOfLightShader.SetMatrix4("view", _camera.GetViewMatrix());
            _sourceOfLightShader.SetMatrix4("projection", _camera.GetProjectionMatrix());

            for (int i = 0; i < _lightPos.Length; i++)
            {
                _sourceOfLightShader.SetVector3("color", _lightColor[i]);

                Matrix4 lightMatrix = Matrix4.Identity;
                lightMatrix *= Matrix4.CreateScale(0.4f);
                lightMatrix *= Matrix4.CreateTranslation(_lightPos[i]);

                _sourceOfLightShader.SetMatrix4("model", lightMatrix);

                GL.DrawArrays(PrimitiveType.Triangles, 0, _verticesSphere.Length / 8);
            }

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

            //--------------------------------------
            // Блюринг света
            //--------------------------------------
            GL.CullFace(CullFaceMode.Back);
            bool horizontal = true;
            int amount = 6;
            _blurShader.Use();
            _colorMaps[1].Use();
            for (int i = 0; i < amount; i++)
            {
                GL.BindFramebuffer(FramebufferTarget.Framebuffer, _fboPingPongs[horizontal ? 1 : 0]);
                _blurShader.SetBool("horizontal", horizontal);
                GL.BindVertexArray(_vaoQuad);
                GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);
                _pingPongMaps[horizontal ? 1 : 0].Use();
                horizontal = !horizontal;
            }
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

            //--------------------------------------
            //  Отрисовка фреймбуфера
            //--------------------------------------
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            _hdrShader.Use();
            _colorMaps[0].Use();
            _pingPongMaps[horizontal ? 0 : 1].Use(TextureUnit.Texture1);
            //_hdrShader.SetFloat("exposure", _exposure);

            GL.BindVertexArray(_vaoQuad);
            GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);

            SwapBuffers();

            base.OnRenderFrame(e);
        }

        private void renderScene(Shader shader, bool isUsualRender)
        {

            //Рендер "скай бокса"
            GL.BindVertexArray(_vaoCube);

            if (isUsualRender == true)
            {
                GL.CullFace(CullFaceMode.Back);
                _texturesList["bricks"].Use();
                _texturesList["bricks_s"].Use(TextureUnit.Texture1);
                shader.SetInt("isReversed", 1);

                shader.SetFloat("material.shininess", 16f);
            }

            Matrix4 boxMatrix = Matrix4.Identity;
            boxMatrix *= Matrix4.CreateTranslation(_skyBoxPosition);
            boxMatrix *= Matrix4.CreateScale(13.0f);
            shader.SetMatrix4("model", boxMatrix);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);

            //Рендер кубов
            if (isUsualRender == true)
            {
                GL.CullFace(CullFaceMode.Front);
                _texturesList["emerald_block"].Use();
                _texturesList["emerald_block_s"].Use(TextureUnit.Texture1);
                shader.SetInt("isReversed", 0);

                shader.SetFloat("material.shininess", 8f);
            }

            for (int i = 0; i < _cubePosition.Length; i++)
            {

                Matrix4 cubeMatrix = Matrix4.Identity;
                cubeMatrix *= Matrix4.CreateTranslation(_cubePosition[i]);
                float angle = 15.0f * i;
                cubeMatrix *= Matrix4.CreateFromAxisAngle(new Vector3(0.8f, 0.4f, 0.2f), angle);
                cubeMatrix *= Matrix4.CreateFromAxisAngle(new Vector3(0.0f, 1.0f, 0.0f), _rotationAngleY);
                cubeMatrix *= Matrix4.CreateFromAxisAngle(new Vector3(1.0f, 0.0f, 0.0f), _rotationAngleX);
                shader.SetMatrix4("model", cubeMatrix);

                GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
            }

            // Рендер сфер
            GL.BindVertexArray(_vaoSphere);

            if (isUsualRender == true)
            {
                _texturesList["crystal"].Use();
                _texturesList["crystal_s"].Use(TextureUnit.Texture1);

                shader.SetFloat("material.shininess", 0.6f);
            }

            for (int i = 0; i < _spherePosition.Length; i++)
            {

                Matrix4 sphereMatrix = Matrix4.Identity;
                sphereMatrix *= Matrix4.CreateTranslation(_spherePosition[i]);
                float angle = 25.0f * i;
                sphereMatrix *= Matrix4.CreateFromAxisAngle(new Vector3(0.8f, 0.4f, 0.2f), angle);
                sphereMatrix *= Matrix4.CreateFromAxisAngle(new Vector3(0.0f, 1.0f, 0.0f), _rotationAngleY);
                sphereMatrix *= Matrix4.CreateFromAxisAngle(new Vector3(1.0f, 0.0f, 0.0f), _rotationAngleX);
                shader.SetMatrix4("model", sphereMatrix);
                GL.DrawArrays(PrimitiveType.Triangles, 0, _verticesSphere.Length / 8);
            }

            //Рендер пирамид 
            GL.BindVertexArray(_vaoPyramide);

            if (isUsualRender == true)
            {
                _texturesList["iron_block"].Use();
                _texturesList["iron_block_s"].Use(TextureUnit.Texture1);

                shader.SetFloat("material.shininess", 2.0f);
            }

            for (int i = 0; i < _pyramidePosition.Length; i++)
            {
                Matrix4 pyramideMatrix = Matrix4.Identity;
                pyramideMatrix *= Matrix4.CreateTranslation(_pyramidePosition[i]);
                float angle = 15.0f * i;
                pyramideMatrix *= Matrix4.CreateFromAxisAngle(new Vector3(0.1f, 0.3f, 0.2f), angle);
                pyramideMatrix *= Matrix4.CreateFromAxisAngle(new Vector3(0.0f, 1.0f, 0.0f), _rotationAngleY);
                pyramideMatrix *= Matrix4.CreateFromAxisAngle(new Vector3(1.0f, 0.0f, 0.0f), _rotationAngleX);
                shader.SetMatrix4("model", pyramideMatrix);

                GL.DrawArrays(PrimitiveType.Triangles, 0, 18);
            }
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (!Focused)
            {
                return;
            }

            var input = Keyboard.GetState();
            var mouse = Mouse.GetState();

            const float cameraSpeed = 1.5f;
            const float sensitivity = 0.2f;

            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
            }

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

            if (input.IsKeyDown(Key.Up))
            {
                _rotationAngleX -= 0.05f;
            }

            if (input.IsKeyDown(Key.Down))
            {
                _rotationAngleX += 0.05f;
            }

            if (input.IsKeyDown(Key.Left))
            {
                _rotationAngleY += 0.05f;
            }

            if (input.IsKeyDown(Key.Right))
            {
                _rotationAngleY -= 0.05f;
            }


            if (input.IsKeyDown(Key.Y))
            {
                Console.WriteLine(_camera.Position); // Получение координат
            }

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
            GL.Viewport(ClientRectangle);
            base.OnResize(e);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            _camera.Fov -= e.DeltaPrecise;
            _camera.AspectRatio = Width / (float)Height;
            base.OnMouseWheel(e);
        }

        private void createSphereVertices(float r, int total)
        {
            int size = (total + 1) * (total + 1) * 2 * 3 * 3 * 3 * 2; // число треугольников, количество точек, координаты точек, нормали, текстура
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
                        globe[i + 1, j + 1],
                        globe[i + 1, j],
                    };

                    for (int n = 0; n < g.Length; n++)
                    {
                        //Координаты точки
                        _verticesSphere[vert++] = g[n].X;
                        _verticesSphere[vert++] = g[n].Y;
                        _verticesSphere[vert++] = g[n].Z;

                        Vector3 normal = Vector3.Normalize(new Vector3(g[n].X, g[n].Y, g[n].Z));

                        // Нормаль
                        _verticesSphere[vert++] = normal.X;
                        _verticesSphere[vert++] = normal.Y;
                        _verticesSphere[vert++] = normal.Z;

                        // Текстура
                        float u = (float)(Math.Atan2(g[n].X, g[n].Y) / (Math.PI * 2) + 0.5);
                        float v = g[n].Z * 0.5f + 0.5f;

                        _verticesSphere[vert++] = u;
                        _verticesSphere[vert++] = v;
                    }

                    Vector3[] g2 = {
                        globe[i, j],
                        globe[i, j + 1],
                        globe[i + 1, j + 1],
                    };

                    for (int n = 0; n < g2.Length; n++)
                    {
                        //Координаты точки
                        _verticesSphere[vert++] = g2[n].X;
                        _verticesSphere[vert++] = g2[n].Y;
                        _verticesSphere[vert++] = g2[n].Z;

                        Vector3 normal = Vector3.Normalize(new Vector3(g2[n].X, g2[n].Y, g2[n].Z));

                        // Нормаль
                        _verticesSphere[vert++] = normal.X;
                        _verticesSphere[vert++] = normal.Y;
                        _verticesSphere[vert++] = normal.Z;

                        // Текстура
                        float u = (float)(Math.Atan2(g2[n].X, g2[n].Y) / (Math.PI * 2) + 0.5);
                        float v = g2[n].Z * 0.5f + 0.5f;

                        _verticesSphere[vert++] = u;
                        _verticesSphere[vert++] = v;
                    }

                }
            }
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

            for (int i = 0; i < _lightPos.Length; i++) {
                GL.DeleteFramebuffer(_fboDepthMaps[i]);
            }
            GL.DeleteVertexArray(_vaoCube);
            GL.DeleteVertexArray(_vaoSphere);
            GL.DeleteVertexArray(_vaoPyramide);

            GL.DeleteProgram(_lightingShader.Handle);
            GL.DeleteProgram(_sourceOfLightShader.Handle);
            GL.DeleteProgram(_depthShader.Handle);

            base.OnUnload(e);
        }

    }
}