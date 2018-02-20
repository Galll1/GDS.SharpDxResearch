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
            SunLight sun = new SunLight() {ShowLights = true};
            ThreePointLights three = new ThreePointLights() { ShowLights = true };
            DirectionalHeadLight dir = new DirectionalHeadLight() { ShowLights = true, Color = Colors.Yellow};
            GridLinesVisual3D grid = new GridLinesVisual3D() {Width = 8, Length = 8, MinorDistance = 1, MajorDistance = 1, Thickness = 0.01};

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
                    visuals.Add(userControl.GetVisual3D());
                }
            }

            return visuals;
        }
    }
}
