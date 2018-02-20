using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using JeremyAnsel.Media.WavefrontObj;

namespace GDS.SharpDxResearch.ResourceLoad.GeometyLoad
{
    public class LoadedObjFile
    {
        private readonly ObjFile objFile;

        public static LoadedObjFile Load(string filePath)
        {
            ObjFile objFile = ObjFile.FromFile(filePath);

            return new LoadedObjFile(objFile);

        }

        public LoadedObjFile(ObjFile objFile)
        {
            this.objFile = objFile;
        }

        public IEnumerable<Point3D> GetPoints()
        {
            return objFile.Vertices.Select(vertice => new Point3D(vertice.X, vertice.Y, vertice.Z));
        }

        public IEnumerable<ObjFace> GetObjectFaces(string objectName)
        {
            return objFile.Faces
                          .Where(face => face.ObjectName == objectName);
        }

        public IEnumerable<string> GetObjectNames()
        {
            return objFile.Faces
                          .Select(face => face.ObjectName)
                          .Distinct();
        }

        public IEnumerable<int> GetObjectTriangleIndices(string objectName)
        {
            List<int> triangleIndices = new List<int>();

            IEnumerable<ObjFace> objectFaces = GetObjectFaces(objectName);

            foreach (ObjFace objectFace in objectFaces)
            {
                triangleIndices.AddRange(new[] { objectFace.Vertices[0].Vertex, objectFace.Vertices[1].Vertex, objectFace.Vertices[2].Vertex });

                if(objectFace.Vertices.Count > 3)
                    triangleIndices.AddRange(new[] { objectFace.Vertices[0].Vertex, objectFace.Vertices[2].Vertex, objectFace.Vertices[3].Vertex });
            }

            return triangleIndices;
        }

        public IEnumerable<Vector3D> GetNormals()
        {
            return objFile.VertexNormals
                          .Select(normal => new Vector3D(normal.X, normal.Y, normal.Z));
        }
    }
}
