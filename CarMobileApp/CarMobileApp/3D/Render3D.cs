using Urho;

namespace CarMobileApp._3D
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

            _node = scene.CreateChild();
            _node.Position = new Vector3(0, 0, 5);
            _node.Rotation = new Quaternion(0, 0, 0); //up/down, left/right, angel
            _node.SetScale(1f);

            //model
            StaticModel modelObject = _node.CreateComponent<StaticModel>();
            modelObject.Model = ResourceCache.GetModel("Models/Cylinder.mdl");

            //light
            Node light = scene.CreateChild(name: "light");
            light.SetDirection(new Vector3(0.4f, -0.5f, 0.3f));
            light.CreateComponent<Light>();

            //camera
            Node cameraNode = scene.CreateChild(name: "camera");
            Camera camera = cameraNode.CreateComponent<Camera>();

            Renderer.SetViewport(0, new Viewport(scene, camera, null));
        }

        public void SetRotation(int X, int Y, int Z)
        {
            this.X = X;
            this.Y = Y * -4;
            this.Z = Z * 4;

            _node.Rotation = new Quaternion(this.Z, 0, this.Y); //up/down, left/right, angel
        }
    }
}
