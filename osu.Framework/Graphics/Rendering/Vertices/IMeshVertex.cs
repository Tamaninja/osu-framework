// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Framework.Graphics.Rendering.Vertices
{
    public interface IMeshVertex<T> : IVertex
    {
        public abstract static T[] FromMesh(Assimp.Mesh mesh);
    }
}
