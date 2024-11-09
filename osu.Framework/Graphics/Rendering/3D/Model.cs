using System.Collections.Generic;
using Assimp;
using osu.Framework.Graphics.OpenGL;

namespace osu.Framework.Graphics.Rendering
{

    public class Model : ThreeDimensionalDrawNode
    {
        public List<Assimp.Material> Materials;
        public Mesh[] Meshes;
        public readonly string Filepath;
        public Model(string filepath, SceneNode scene) : base(scene)
        {

            Filepath = filepath;
            AssimpContext importer = new AssimpContext();
            Assimp.Scene sceneInfo = importer.ImportFile(filepath, PostProcessSteps.Triangulate | PostProcessSteps.FlipUVs);
            Materials = sceneInfo.Materials;

            loadMeshes(sceneInfo.Meshes);
        }

        private void loadMeshes(List<Assimp.Mesh> assimpMeshes)
        {
            Meshes = new Mesh[assimpMeshes.Count];

            for (int i = 0; i < assimpMeshes.Count; i++)
            {
                Meshes[i] = Scene.Renderer.ImportMesh(assimpMeshes[i], this);
            }
        }

        public static string BOX_3D => @"E:\Rocksmeth\supreme-broccoli\TestTest123.Resources\Models\Trashcan_Small1.fbx";
        public static string NOTE => @"E:\Rocksmeth\supreme-broccoli\TestTest123.Resources\Models\Stone.fbx";
        public static string MCQUEEN => @"E:\Rocksmeth\supreme-broccoli\TestTest123.Resources\Models\Lighting Mcqueen\LightingMcqueen.obj";
    }
}
