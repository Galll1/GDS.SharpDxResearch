using System.Collections.ObjectModel;
using GDS.SharpDxResearch.GraphicObjects.ViewModels;

namespace GDS.SharpDxResearch.SharpDx.MainModule.ViewModels
{
    public class RenderViewModel
    {
        public ObservableCollection<DxObjectViewModel> RenderContents { get; private set; }
        //public Geometry3D Geometry => (new DxObject() { DataContext = new DxObjectViewModel() }).GetMesh();
        //public DxObjectViewModel RenderContents => new DxObjectViewModel();

        public RenderViewModel()
        {
            RenderContents = new ObservableCollection<DxObjectViewModel>() { new DxObjectViewModel() };
        }
    }
}
