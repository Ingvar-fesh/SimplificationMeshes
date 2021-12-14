using System;
using System.Collections.Generic;
using System.Numerics;
using MeshSimplification.Types;
using SharpDX;

namespace MeshSimplification.Algorithms
{
    public class CenterCycle
    {
        public Model Simplify(Model model)
        {
            Model newModel = new Model();
            foreach (Mesh mesh in model.Meshes)
            {
                Mesh simple = new Mesh(new List<Vertex>(mesh.Vertices), new List<Vertex>(mesh.Normals),
                    new List<Face>(mesh.Faces), new List<Edge>(mesh.Edges));
            }
            return newModel;
        }
    }
}
