using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace GDS.SharpDxResearch.GraphicObjects.Controls
{
    public class DxUserControl : ContentControl
    {
        public Visual3D GetVisual3D()
        {
            var child = Content;
            return child as Visual3D;
        }
    }
}
