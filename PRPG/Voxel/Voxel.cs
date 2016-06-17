using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace PRPG.Voxel
{
    public class VoxelObject : DrawableGameComponent
    {
        GraphicsDeviceManager _graphics;
        VertexPositionColor[] _verts;
        BasicEffect _effect;

        private float rotation = 0f;
        public Vector3 Position { get; set; }
        public Vector3 Size { get; set; }

        public float CamSpeed { get; set; }

        Color pointColor = Color.Black;

        public VoxelObject(Game game, GraphicsDeviceManager graphics, Vector3 position = new Vector3()) : base(game)
        {
            this._graphics = graphics;
            Position = position;
        }

        public override void Initialize()
        {
            Vector3 topLeftFront = new Vector3(-1.0f, 1.0f, -1.0f) * Size;
            Vector3 topLeftBack = new Vector3(-1.0f, 1.0f, 1.0f) * Size;
            Vector3 topRightFront = new Vector3(1.0f, 1.0f, -1.0f) * Size;
            Vector3 topRightBack = new Vector3(1.0f, 1.0f, 1.0f) * Size;
            

            Vector3 btmLeftFront = new Vector3(-1.0f, -1.0f, -1.0f) * Size;
            Vector3 btmLeftBack = new Vector3(-1.0f, -1.0f, 1.0f) * Size;
            Vector3 btmRightFront = new Vector3(1.0f, -1.0f, -1.0f) * Size;
            Vector3 btmRightBack = new Vector3(1.0f, -1.0f, 1.0f) * Size;
            
            _verts = new VertexPositionColor[36];
            //앞
            _verts[0] = new VertexPositionColor(topLeftFront, pointColor);
            _verts[1] = new VertexPositionColor(btmLeftFront, pointColor);
            _verts[2] = new VertexPositionColor(topRightFront,pointColor);
            _verts[3] = new VertexPositionColor(btmLeftFront, pointColor);
            _verts[4] = new VertexPositionColor(btmRightFront, pointColor);
            _verts[5] = new VertexPositionColor(topRightFront, pointColor);
            //뒤
            _verts[6] = new VertexPositionColor(topLeftBack, pointColor);
            _verts[7] = new VertexPositionColor(topRightBack, pointColor);
            _verts[8] = new VertexPositionColor(btmLeftBack, pointColor);
            _verts[9] = new VertexPositionColor(btmLeftBack, pointColor);
            _verts[10] = new VertexPositionColor(topRightBack, pointColor);
            _verts[11] = new VertexPositionColor(btmRightBack, pointColor);
            //위
            _verts[12] = new VertexPositionColor(topLeftFront, pointColor);
            _verts[13] = new VertexPositionColor(topRightBack, pointColor);
            _verts[14] = new VertexPositionColor(topLeftBack, pointColor);
            _verts[15] = new VertexPositionColor(topLeftFront, pointColor);
            _verts[16] = new VertexPositionColor(topRightFront, pointColor);
            _verts[17] = new VertexPositionColor(topRightBack, pointColor);
            //아래
            _verts[18] = new VertexPositionColor(btmLeftFront, pointColor);
            _verts[19] = new VertexPositionColor(btmLeftBack, pointColor);
            _verts[20] = new VertexPositionColor(btmRightBack, pointColor);
            _verts[21] = new VertexPositionColor(btmLeftFront, pointColor);
            _verts[22] = new VertexPositionColor(btmRightBack, pointColor);
            _verts[23] = new VertexPositionColor(btmRightFront, pointColor);
            //왼쪽
            _verts[24] = new VertexPositionColor(topLeftFront, pointColor);
            _verts[25] = new VertexPositionColor(btmLeftBack, pointColor);
            _verts[26] = new VertexPositionColor(btmLeftFront, pointColor);
            _verts[27] = new VertexPositionColor(topLeftBack, pointColor);
            _verts[28] = new VertexPositionColor(btmLeftBack, pointColor);
            _verts[29] = new VertexPositionColor(topLeftFront, pointColor);
            //오른쪽
            _verts[30] = new VertexPositionColor(topRightFront, pointColor);
            _verts[31] = new VertexPositionColor(btmRightFront, pointColor);
            _verts[32] = new VertexPositionColor(btmRightBack, pointColor);
            _verts[33] = new VertexPositionColor(topRightBack, pointColor);
            _verts[34] = new VertexPositionColor(topRightFront, pointColor);
            _verts[35] = new VertexPositionColor(btmRightBack, pointColor);

            
            _effect = new BasicEffect(GraphicsDevice);
            base.Initialize();
        }

        VertexBuffer vertexBuff;
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // The assignment of effect.View and effect.Projection
            // are nearly identical to the code in the Model drawing code.
            var cameraPosition = Position;
            var cameraLookAtVector = Vector3.Zero;
            var cameraUpVector = Vector3.UnitZ;

            _effect.World = Matrix.Identity;
            _effect.View = Matrix.CreateLookAt(cameraPosition, cameraLookAtVector, cameraUpVector);
            _effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 0.5f, 500.0f);
            //_effect.View = Matrix.CreateTranslation(Position);

            float aspectRatio =
                _graphics.PreferredBackBufferWidth / (float)_graphics.PreferredBackBufferHeight;
            float fieldOfView = MathHelper.PiOver4;
            float nearClipPlane = 1;
            float farClipPlane = 200;

            _effect.Projection = Matrix.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, nearClipPlane, farClipPlane);
            _effect.LightingEnabled = true;

            foreach (var pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                vertexBuff = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), 36, BufferUsage.WriteOnly);

                vertexBuff.SetData<VertexPositionColor>(_verts);
                GraphicsDevice.SetVertexBuffer(vertexBuff);

                GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 12);
            }

            base.Draw(gameTime);
        }

        public void MoveForward()
        {
            Position += _effect.View.Forward * CamSpeed;
        }

        public void MoveBack()
        {
            Position += _effect.View.Backward * CamSpeed;
        }

        public void MoveLeft()
        {
            Position += _effect.View.Left * CamSpeed;
        }

        public void MoveRight()
        {
            Position += _effect.View.Right * CamSpeed;
        }

        public void MoveUp()
        {
            Position += _effect.View.Up * CamSpeed;
        }

        public void MoveDown()
        {
            Position += _effect.View.Down * CamSpeed;
        }
    }
}
