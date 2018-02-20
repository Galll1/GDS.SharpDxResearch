using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using GDS.SharpDxResearch.GraphicObjects.ViewModels;
using GDS.SharpDxResearch.ResourceLoad.GeometyLoad;

namespace GDS.SharpDxResearch.SharpDx.MainModule.ViewModels
{
    public class RenderViewModel
    {
        public ObservableCollection<DxObjectViewModel> RenderContents { get; private set; }

        public RenderViewModel()
        {
            RenderContents = new ObservableCollection<DxObjectViewModel>();

            LoadObjects();
        }

        private void LoadObjects()
        {
            LoadedObjFile objFile = LoadedObjFile.Load(@"F:\Projects\Assets\bugatti\bugatti.obj");
            IEnumerable<string> objectNames = objFile.GetObjectNames();

            foreach (string objectName in objectNames)
            {
                if (objectName.ToLower().Contains("cube") == false)
                    continue;

                RenderContents.Add(GetObjectViewModel(objFile, objectName));
            }
        }

        private DxObjectViewModel GetObjectViewModel(LoadedObjFile objFile, string objectName)
        {
            return new DxObjectViewModel()
            {
                Positions = new Point3DCollection(objFile.GetPoints()),
                TriangleIndices = new Int32Collection(objFile.GetObjectTriangleIndices(objectName)),
                Normals = new Vector3DCollection(objFile.GetNormals())
            };

        }
    }
}
