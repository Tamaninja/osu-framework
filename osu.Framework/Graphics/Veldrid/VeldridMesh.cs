// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.OpenGL.Buffers;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Rendering.Vertices;
using osu.Framework.Graphics.Veldrid.Buffers;
using osu.Framework.Graphics.Veldrid.Buffers.Staging;
using osu.Framework.Graphics.Veldrid.Pipelines;
using osu.Framework.Graphics.Veldrid.Vertices;
using osu.Framework.Logging;
using osu.Framework.Platform;
using osu.Framework.Statistics;
using osuTK;
using Veldrid;
using Vulkan;

namespace osu.Framework.Graphics.Veldrid
{
    internal class VeldridMesh : Mesh
    {

        public VeldridIndexBuffer IndexBuffer;
        public VeldridVertexBuffer<TexturedMeshVertex> VertexBuffer;
        private VeldridRenderer renderer;
        public VeldridMesh(VeldridRenderer renderer, Assimp.Mesh mesh) : base(mesh)
        {
            this.renderer = renderer;

            TexturedMeshVertex[] vertices = new TexturedMeshVertex[mesh.Vertices.Count];
            for (int i = 0; i < mesh.Vertices.Count; i++)
            {
                vertices[i] = new TexturedMeshVertex(mesh, i);
            }

            VertexBuffer = new VeldridVertexBuffer<TexturedMeshVertex>(renderer, vertices);

            uint[] indices = mesh.GetUnsignedIndices();

            DeviceBuffer indexBuffer = renderer.Factory.CreateBuffer(new BufferDescription((uint)indices.Length * sizeof(uint), BufferUsage.IndexBuffer));
            renderer.Device.UpdateBuffer(indexBuffer, 0, indices);

            IndexBuffer = new VeldridIndexBuffer(indexBuffer, indices.Length);

        }


        public override void Draw()
        {
            renderer.BindIndexBuffer(IndexBuffer);
            renderer.BindVertexBuffer(VertexBuffer);
            renderer.DrawVertices(Rendering.PrimitiveTopology.Triangles, 0, IndexBuffer.Size);

/*            renderer.DrawMesh(this);
*/
        }
    }
}
