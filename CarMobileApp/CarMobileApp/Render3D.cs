﻿using System;
using Urho;

namespace CarMobileApp
{
    public class Render3D : Application
    {
        private Node _node;

        private float X = 0;
        private float Y = 0;
        private float Z = 0;

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
            //scene
            var scene = new Scene();
            scene.CreateComponent<Octree>();

            //model
            _node = scene.CreateChild();
            _node.Position = new Vector3(0, 0, 5);
            _node.Rotation = new Quaternion(0, -90, 0);  //up/down, left/right, angel
            _node.Scale = new Vector3(1.5f, 2.5f, 4f);

            //model
            StaticModel modelObject = _node.CreateComponent<StaticModel>();
            modelObject.Model = ResourceCache.GetModel("Models/Car.mdl");

            //light
            Node light = scene.CreateChild(name: "light");
            light.SetScale(30);
            light.SetDirection(new Vector3(0, 0, 1));
            light.Position = new Vector3(0, 4, -12);
            light.CreateComponent<Light>().LightType = LightType.Directional;

            //camera
            Node cameraNode = scene.CreateChild(name: "camera");
            cameraNode.Position = new Vector3(0, 0, -16);
            Camera camera = cameraNode.CreateComponent<Camera>();

            //viewport
            Renderer.SetViewport(0, new Viewport(scene, camera, null));

            //background color
            Renderer.GetViewport(0).SetClearColor(new Color() { R = 0.247f, G = 0.247f, B = 0.247f });
        }

        public void SetRotation(float X, float Y, float Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;

            try
            {
                _node.Rotation = new Quaternion(this.Y * -2.2f, -90 + (this.Y * -5), this.Z * 4); //up/down, left/right, angel
            }
            catch (InvalidOperationException)
            {
                return;
            }
        }
    }
}
