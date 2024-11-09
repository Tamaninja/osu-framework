// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using NuGet.Protocol.Plugins;
using osu.Framework.Graphics.OpenGL.Buffers;
using osu.Framework.Graphics.OpenGL;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Rendering.Vertices;
using osu.Framework.Graphics.Veldrid.Buffers;
using osu.Framework.Logging;
using Veldrid;

namespace osu.Framework.Graphics.Veldrid
{
    internal class VeldridMesh<T> : Mesh where T : unmanaged, IEquatable<T>, IMeshVertex<T>
    {

        public VeldridIndexBuffer IndexBuffer;
        public VeldridVertexBuffer<T> VertexBuffer;
        private VeldridRenderer renderer;
        public VeldridMesh(VeldridRenderer renderer, Assimp.Mesh mesh, Model parent) : base(mesh, parent)
        {
            this.renderer = renderer;

            T[] vertices = T.FromMesh(mesh);
            VertexBuffer = new VeldridVertexBuffer<T>(renderer, vertices);

            uint[] indices = mesh.GetUnsignedIndices();

            DeviceBuffer indexBuffer = renderer.Factory.CreateBuffer(new BufferDescription((uint)indices.Length * sizeof(uint), BufferUsage.IndexBuffer));
            renderer.Device.UpdateBuffer(indexBuffer, 0, indices);

            IndexBuffer = new VeldridIndexBuffer(indexBuffer, indices.Length);

        }
        protected override void Draw()
        {
            renderer.BindIndexBuffer(IndexBuffer);
            renderer.BindVertexBuffer(VertexBuffer);
            renderer.DrawVertices(Rendering.PrimitiveTopology.Triangles, 0, IndexBuffer.Size);
        }
    }
}
