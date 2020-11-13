using System;
using Urho;

namespace CarMobileApp
{
    public class Render3D : Application
    {
        private Node _node;

        private int X = 0;
        private int Y = 0;
        private int Z = 0;

        public Render3D(ApplicationOptions opts) : base(opts)
        {

        }

        protected override void Start()
        {
            base.Start();

            Render();
        }

        private void Render()
        {
            var scene = new Scene();
            scene.CreateComponent<Octree>();

            //scene
            _node = scene.CreateChild();
            _node.Position = new Vector3(0, 4, 5);
            _node.Rotation = new Quaternion(180, -90, 0);  //up/down, left/right, angel
            _node.SetScale(1f);

            //model
            StaticModel modelObject = _node.CreateComponent<StaticModel>();
            modelObject.Model = ResourceCache.GetModel("Models/CarModel.mdl");

            //light
            Node light = scene.CreateChild(name: "light");
            light.SetDirection(new Vector3(0.4f, -0.5f, 0.3f));
            light.CreateComponent<Light>();

            //camera
            Node cameraNode = scene.CreateChild(name: "camera");
            cameraNode.Position = new Vector3(0, 0, -16);
            Camera camera = cameraNode.CreateComponent<Camera>();

            //viewport
            Renderer.SetViewport(0, new Viewport(scene, camera, null));

            //background color
            Renderer.GetViewport(0).SetClearColor(new Color() { R = 0.247f, G = 0.247f, B = 0.247f });
        }

        public void SetRotation(int X, int Y, int Z)
        {
            this.X = X;
            this.Y = Y * -4;
            this.Z = Z * 4;

            try
            {
                _node.Rotation = new Quaternion(180 + this.Y, -90, this.Z); //up/down, left/right, angel
            }
            catch (InvalidOperationException)
            {
                return;
            }
        }
    }
}
