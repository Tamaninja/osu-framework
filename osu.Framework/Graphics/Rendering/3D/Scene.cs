using System.Collections.Generic;
using Assimp;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Logging;
using osuTK;


namespace osu.Framework.Graphics.Rendering
{

    // this acts as the root for the world matrix
    public partial class Scene : Container, ITexturedShaderDrawable
    {
        public IShader TextureShader { get; set; }
        public SceneNode Node { get; private set; }

        public Scene()
        {
            RelativeSizeAxes = Axes.Both;
        }

        [BackgroundDependencyLoader]
        private void load(ShaderManager shaders, IRenderer renderer, LargeTextureStore textureStore)
        {

            TextureShader = shaders.Load("nino", "nino");
            Node = new SceneNode(this, renderer, textureStore, shaders);

        }

        protected override DrawNode CreateDrawNode()
        {
            return new SceneDrawNode(this);
        }

        protected class SceneDrawNode : CompositeDrawableDrawNode
        {
            protected new Scene Source => (Scene)base.Source;


            public SceneDrawNode(Scene source) : base(source)
            {


            }

            protected override void Draw(IRenderer renderer)
            {
                foreach (var shader in Source.Node.Shaderers.Keys)
                {
                    Source.Node.CurrentShader = shader;
                    shader.Bind();

                        base.Draw(renderer);

                    shader.Unbind();
                }
            }
        }
    }




    public class SceneNode : Node
    {
        public IShader CurrentShader { get; set; }
        public Dictionary<IShader, List<ThreeDimensionalDrawNode>> Shaderers = [];
        public IRenderer Renderer { get; }
        public TextureStore TextureStore { get; }
        public ShaderManager ShaderManager { get; }

        public SceneNode(Scene scene, IRenderer renderer, LargeTextureStore textureStore, ShaderManager shaderManager) : base(scene)
        {
            scene.Add(Visualization);

            Name.Value = "Scene";
            Renderer = renderer;
            TextureStore = textureStore;
            ShaderManager = shaderManager;
            Scene = this;
        }


        public IShader GetShader(Mesh mesh)
        {
            var shader = ShaderManager.Load("nino", "nino");

            if (!Shaderers.TryGetValue(shader, out var children))
            {
                Shaderers[shader] = children = [];
            }
            children.Add(mesh);

            return shader;
        }


    }
}
