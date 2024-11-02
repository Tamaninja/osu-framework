using System;
using System.Runtime.InteropServices;
using Assimp;
using osuTK.Graphics.ES30;

namespace osu.Framework.Graphics.Rendering.Vertices
{
    [StructLayout(LayoutKind.Sequential)]
    public struct TexturelessMeshVertex : IEquatable<TexturelessMeshVertex>, IMeshVertex<TexturelessMeshVertex>
    {
        [VertexMember(3, VertexAttribPointerType.Float)]
        public Vector3D Position;


        public bool Equals(TexturelessMeshVertex other)
        {
            return Position == other.Position;
        }

        public static TexturelessMeshVertex[] FromMesh(Assimp.Mesh mesh)
        {
            TexturelessMeshVertex[] vertices = new TexturelessMeshVertex[mesh.Vertices.Count];
            for (int i = 0; i < mesh.Vertices.Count; i++)
            {
                vertices[i] = new TexturelessMeshVertex()
                {
                    Position = mesh.Vertices[i],

                };
            }

            return vertices;
        }
    }
}
