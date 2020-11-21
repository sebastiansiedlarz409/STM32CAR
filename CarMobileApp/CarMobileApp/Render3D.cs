using System;
using Urho;

namespace CarMobileApp
{
    public class Render3D : Application
    {
        private Node _node;

        private readonly string _modelPath = "Models/Car.mdl";

        //previous rotation
        private float X = 0;
        private float Y = 0;
        private float Z = 0;

        //default rotation
        private readonly float defaultX = 0;         //up or down
        private readonly float defaultY = -90;       //left right
        private readonly float defaultZ = 0;         //angel

        public Render3D(ApplicationOptions opts) : base(opts)
        { }

        protected override void Start()
        {
            base.Start();

            Render();
        }

        private void Render()
        {
            Scene scene = new Scene();
            scene.CreateComponent<Octree>();
            Node lightNode = scene.CreateChild(name: "Light");
            Node cameraNode = scene.CreateChild(name: "Camera");

            //model
            _node = scene.CreateChild();
            _node.Position = new Vector3(0, 0, 5);
            _node.Rotation = new Quaternion(defaultX, defaultY, defaultZ);
            _node.Scale = new Vector3(1.5f, 2.5f, 4f);

            //model
            StaticModel modelObject = _node.CreateComponent<StaticModel>();
            modelObject.Model = ResourceCache.GetModel(_modelPath);

            //light
            lightNode.SetScale(30);
            lightNode.SetDirection(new Vector3(0, 0, 1));
            lightNode.Position = new Vector3(0, 4, -12);
            lightNode.CreateComponent<Light>().LightType = LightType.Directional;

            //camera
            cameraNode.Position = new Vector3(0, 0, -16);
            Camera camera = cameraNode.CreateComponent<Camera>();

            //viewport
            Renderer.SetViewport(0, new Viewport(scene, camera, null));

            //background color
            Renderer.GetViewport(0).SetClearColor(new Color() { R = 0.247f, G = 0.247f, B = 0.247f });
        }

        public void SetRotation(float X, float Y, float Z)
        {
            if (Math.Abs(this.X - X) > 0.3)
                this.X = X;

            if (Math.Abs(this.Y - Y) > 0.3)
                this.Y = Y;

            if (Math.Abs(this.Z - Z) > 0.3)
                this.Z = Z;

            try
            {
                _node.Rotation = new Quaternion(this.Y * -2.2f + defaultX,
                                                this.Y * 5 + defaultY,
                                                this.Z * 4 + defaultZ);
            }
            catch (InvalidOperationException)
            {
                return;
            }
        }
    }
}
