using Urho;
using Urho.Gui;

namespace CarMobileApp._3D
{
    public class Render3D : Urho.Application
    {
        public Render3D(ApplicationOptions opts) : base(opts) { }

        protected override unsafe void Start()
        {
            base.Start();

            var text = new Text()
            {
                Value = "Hello World!",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            text.SetColor(Color.Cyan);
            text.SetFont(font: ResourceCache.GetFont("Fonts/Anonymous Pro.ttf"), size: 30);
            // Add to UI Root
            UI.Root.AddChild(text);
        }
    }
}
