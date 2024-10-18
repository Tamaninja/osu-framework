using System;
using System.Runtime.InteropServices;
using Assimp;
using osuTK.Graphics.ES30;

namespace osu.Framework.Graphics.Rendering.Vertices
{
    [StructLayout(LayoutKind.Sequential)]

    public struct TexturedMeshVertex : IEquatable<TexturedMeshVertex>, IVertex
    {
        [VertexMember(3, VertexAttribPointerType.Float)]
        public Vector3D Position;

        [VertexMember(3, VertexAttribPointerType.Float)]
        public Vector3D TexturePosition;

        public TexturedMeshVertex(Assimp.Mesh mesh, int index)
        {
            Position = mesh.Vertices[index];
            TexturePosition = mesh.TextureCoordinateChannels[0][index];
        }


        public bool Equals(TexturedMeshVertex other)
        {
            return Position == other.Position && TexturePosition == other.TexturePosition;
        }


    }
}
