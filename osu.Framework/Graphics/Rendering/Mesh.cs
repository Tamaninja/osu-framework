using System;
using System.Collections.Generic;
using Assimp;
using osu.Framework.Logging;

namespace osu.Framework.Graphics.Rendering
{
    public class Mesh
    {

        public int MaterialIndex;
        public uint[] Indices;
        public string Name;
        public Vector3D[] Vertices;
        public Vector3D[] TextureCoords;

        public Mesh(Assimp.Mesh mesh)
        {

            Name = mesh.Name;
            MaterialIndex = mesh.MaterialIndex;
            TextureCoords = mesh.TextureCoordinateChannels[0].ToArray();
            Vertices = mesh.Vertices.ToArray();

            Indices = mesh.GetUnsignedIndices();
        }

        public virtual void Draw()
        {

        }
        public virtual void Dispose()
        {
        }
    }
}
