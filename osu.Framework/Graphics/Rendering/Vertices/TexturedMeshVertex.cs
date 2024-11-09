using System;
using System.Runtime.InteropServices;
using Assimp;
using osuTK.Graphics.ES30;

namespace osu.Framework.Graphics.Rendering.Vertices
{
    [StructLayout(LayoutKind.Sequential)]

    public struct TexturedMeshVertex : IEquatable<TexturedMeshVertex>, IMeshVertex<TexturedMeshVertex>
    {
        [VertexMember(3, VertexAttribPointerType.Float)]
        public Vector3D Position;

        [VertexMember(3, VertexAttribPointerType.Float)]
        public Vector3D TexturePosition;

        public bool Equals(TexturedMeshVertex other)
        {
            return Position == other.Position && TexturePosition == other.TexturePosition;
        }

        public static TexturedMeshVertex[] FromMesh(Assimp.Mesh mesh)
        {
            TexturedMeshVertex[] vertices = new TexturedMeshVertex[mesh.Vertices.Count];

            if (mesh.HasTextureCoords(0))
            {
                for (int i = 0; i < mesh.Vertices.Count; i++)
                {
                    vertices[i] = new TexturedMeshVertex()
                    {
                        Position = mesh.Vertices[i],

                        TexturePosition = mesh.TextureCoordinateChannels[0][i]
                    };
                }
            } else
            {
                for (int i = 0; i < mesh.Vertices.Count; i++)
                {
                    vertices[i] = new TexturedMeshVertex()
                    {
                        Position = mesh.Vertices[i],

                        TexturePosition = new Vector3D()
                    };
                }
            }


            return (vertices);
        }
    }
}
