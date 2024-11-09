using Assimp;
using NuGet.Protocol.Plugins;
using NUnit.Framework;
using osu.Framework.Graphics.OpenGL.Buffers;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Textures;
using osu.Framework.Logging;
using TestTest123.Game;

namespace osu.Framework.Graphics.Rendering
{
    public abstract class Mesh : ThreeDimensionalDrawNode
    {

        public Texture Texture { get; private set; }

        public bool IsTextured { get; private set; } = false;

        public Colour4 Colour { get; set; } = new Colour4(255, 255, 255, 22);
        public IShader TextureShader { get; set; }

        private UniformMaterial uniformMaterial => new UniformMaterial(Colour);
        private IUniformBuffer<UniformMaterial> uniformBuffer;

        public Mesh(Assimp.Mesh mesh, Model parent) : base(parent)
        {
            Name.Value = mesh.Name;

            Material = parent.Materials[mesh.MaterialIndex];
/*            Colour = new Colour4(Material.ColorDiffuse.R, Material.ColorDiffuse.G, Material.ColorDiffuse.B, Material.ColorDiffuse.A);
*/            if (Material.HasTextureDiffuse)
            {
                Texture = Scene.TextureStore.Get(Material.TextureDiffuse.FilePath);
                IsTextured = true;
            }

            TextureShader = Scene.GetShader(this);
        }


        public override void Draw(IRenderer renderer)
        {
            Texture ??= renderer.WhitePixel;
            uniformBuffer ??= renderer.CreateUniformBuffer<UniformMaterial>();

            Scene.CurrentShader.BindUniformBlock("u_Colour", uniformBuffer);


            Texture.Bind();
            foreach (NodeInstance instance in Instances)
            {
                uniformBuffer.Data = new UniformMaterial(Colour * instance.Colour);

                renderer.PushProjectionMatrix(instance.LocalMatrix.Value * renderer.ProjectionMatrix);


                Draw();


                renderer.PopProjectionMatrix();
            }
        }

        protected abstract void Draw();
    }
}
