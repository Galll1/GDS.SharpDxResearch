using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace GDS.SharpDxResearch.GraphicObjects.ViewModels
{
    public class DxObjectViewModel
    {
        public Point3DCollection Positions { get; set; }
        public Int32Collection TriangleIndices { get; set; }

        public DxObjectViewModel()
        {
            Positions = new Point3DCollection(GetObjectPositionPoints());
            TriangleIndices = new Int32Collection(GetObjectTriangleIndices());
        }

        private IEnumerable<int> GetObjectTriangleIndices()
        {
            yield return 2; yield return 3; yield return 1;
            yield return 2; yield return 1; yield return 0;
            yield return 7; yield return 1; yield return 3;
            yield return 7; yield return 5; yield return 1;
            yield return 6; yield return 5; yield return 7;
            yield return 6; yield return 4; yield return 5;
            yield return 6; yield return 2; yield return 0;
            yield return 2; yield return 0; yield return 4;
            yield return 2; yield return 7; yield return 3;
            yield return 2; yield return 6; yield return 7;
            yield return 0; yield return 1; yield return 5;
            yield return 0; yield return 5; yield return 4;
        }

        private IEnumerable<Point3D> GetObjectPositionPoints()
        {
            yield return new Point3D(0, 0, 0);
            yield return new Point3D(1, 0, 0);
            yield return new Point3D(0, 1, 0);
            yield return new Point3D(1, 1, 0);
            yield return new Point3D(0, 0, 1);
            yield return new Point3D(1, 0, 1);
            yield return new Point3D(0, 1, 1);
            yield return new Point3D(1, 1, 0);
        }
    }
}
