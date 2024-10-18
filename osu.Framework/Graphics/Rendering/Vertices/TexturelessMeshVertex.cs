using System;
using System.Runtime.InteropServices;
using Assimp;
using osuTK.Graphics.ES30;

namespace osu.Framework.Graphics.Rendering.Vertices
{
    [StructLayout(LayoutKind.Sequential)]
    public struct TexturelessMeshVertex : IEquatable<TexturelessMeshVertex>, IVertex
    {
        [VertexMember(3, VertexAttribPointerType.Float)]
        public Vector3D Position;

        public TexturelessMeshVertex(Assimp.Mesh mesh, int index)
        {
            Position = mesh.Vertices[index];
        }

        public bool Equals(TexturelessMeshVertex other)
        {
            return Position == other.Position;
        }

    }
}
