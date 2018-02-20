using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using GDS.SharpDxResearch.GraphicObjects.Controls;
using GDS.SharpDxResearch.GraphicObjects.ViewModels;
using HelixToolkit.Wpf;
using Prism.Mvvm;

namespace GDS.SharpDxResearch.SharpDx.MainModule.Extensions
{
    public class HelixViewport3DWithApplicableItemTemplate : HelixViewport3D
    {
        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            ApplyVisual3DTemplate(e.NewItems);

            base.OnItemsChanged(e);
        }

        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            AddConstantValues();
            ApplyVisual3DTemplate(newValue);

            base.OnItemsSourceChanged(oldValue, newValue);
        }

        private void AddConstantValues()
        {
            SunLight sun = new SunLight() {ShowLights = false};
            ThreePointLights three = new ThreePointLights() { ShowLights = false };
            DirectionalHeadLight dir = new DirectionalHeadLight() { ShowLights = false, Color = Colors.Yellow};
            GridLinesVisual3D grid = new GridLinesVisual3D() {Width = 500, Length = 500, MinorDistance = 10, MajorDistance = 10, Thickness = 0.01};

            Children.Add(sun);
            Children.Add(three);
            Children.Add(dir);
            Children.Add(grid);
        }

        private void ApplyVisual3DTemplate(object newValue)
        {
            IEnumerable<BindableBase> viewModels = null;
            if (newValue is BindableBase viewModel)
            {
                viewModels = new List<BindableBase>() { viewModel };
            }

            if (newValue is IEnumerable<BindableBase> valueViewModels)
            {
                viewModels = valueViewModels;
            }

            if (viewModels == null)
            {
                return;
            }

            foreach (Visual3D visual3d in ResolveVisual3Ds(viewModels))
            {
                Children.Add(visual3d);
            }
        }

        private static IEnumerable<Visual3D> ResolveVisual3Ds(IEnumerable<BindableBase> viewModels)
        {
            List<Visual3D> visuals = new List<Visual3D>();
            foreach (BindableBase viewModel in viewModels)
            {
                object key = viewModel.GetType();
                if (Application.Current.Resources.Contains(key))
                {
                    CustomResourceDictionaryValue resource = Application.Current.Resources[key] as CustomResourceDictionaryValue;
                    Type viewType = resource.Value.GetType();

                    DxUserControl userControl = Activator.CreateInstance(viewType) as DxUserControl;
                    userControl.DataContext = viewModel;
                    Visual3D visual3D = userControl.GetVisual3D();
                    ModelVisual3D model = visual3D as ModelVisual3D;
                    GeometryModel3D geom = model.Content as GeometryModel3D;
                    DxObjectViewModel castedViewModel = viewModel as DxObjectViewModel;

                    geom?.Geometry
                        ?.SetValue(MeshGeometry3D.PositionsProperty, castedViewModel?.Positions);
                    geom?.Geometry
                        ?.SetValue(MeshGeometry3D.TriangleIndicesProperty, castedViewModel?.TriangleIndices);
                    geom?.Geometry
                        ?.SetValue(MeshGeometry3D.NormalsProperty, castedViewModel?.Normals);

                    visuals.Add(visual3D);
                }
            }

            return visuals;
        }
    }
}
