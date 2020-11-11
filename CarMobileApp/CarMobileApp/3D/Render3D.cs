using System.Threading.Tasks;
using Urho;
using Urho.Actions;

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

        protected override async void Start()
        {
            base.Start();

            await Render();
        }

        private async Task Render()
        {
            var scene = new Scene();
            scene.CreateComponent<Octree>();

            _node = scene.CreateChild();
            _node.Position = new Vector3(0, 0, 5);
            _node.Rotation = new Quaternion(90, 0, 0);
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

        public async Task SetRotation(int X, int Y, int Z)
        {
            this.X = X;
            this.Y = ((Y / 2) * -1);
            this.Z = Z/5;

            //up and down
            //useless
            //left right
            await _node.RunActionsAsync(
                new RepeatForever(new RotateBy(duration: 0.05f,
                    deltaAngleX: Z/5, deltaAngleY: 0, deltaAngleZ: ((Y/2)*-1))));
        }
    }
}
