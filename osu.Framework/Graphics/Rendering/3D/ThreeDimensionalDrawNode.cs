using Assimp;

namespace osu.Framework.Graphics.Rendering
{
    public class ThreeDimensionalDrawNode : Node
    {
        public Material Material { get; set; }

        public ThreeDimensionalDrawNode(Node parent) : base(parent)
        {

        }

        public virtual void Draw(IRenderer renderer)
        {
            foreach (ThreeDimensionalDrawNode node in Children)
            {

                node.Draw(renderer);
            }
        }


    }
}
