using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using Prism.Mvvm;

namespace GDS.SharpDxResearch.SharpDx.MainModule.Extensions
{
    public static class RenderContentsExtension
    {
        public static readonly DependencyProperty RenderContentsProperty =
            DependencyProperty.RegisterAttached("RenderContents", typeof(object), typeof(RenderContentsExtension), new PropertyMetadata(null, RenderContents));
        private static void RenderContents(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            IEnumerable<BindableBase> viewModels = null;

            if (dependencyPropertyChangedEventArgs.NewValue is BindableBase viewModel)
            {
                viewModels = new List<BindableBase>() { viewModel };
            }

            if (dependencyPropertyChangedEventArgs.NewValue is IEnumerable<BindableBase> valueViewModels)
            {
                viewModels = valueViewModels;
            }

            if (viewModels == null)
            {
                return;
            }

            if (dependencyObject is HelixViewport3D viewport)
            {
                foreach (Visual3D visual3d in ResolveVisual3Ds(viewModels))
                {
                    viewport.Children.Add(visual3d);
                }
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
                    resource.Value.DataContext = viewModel;
                    visuals.Add(resource.Value.GetVisual3D());
                }
            }

            return visuals;
        }

        public static object GetRenderContents(DependencyObject obj)
        {
            return (IEnumerable)obj.GetValue(RenderContentsProperty);
        }
        public static void SetRenderContents(DependencyObject obj, object value)
        {
            obj.SetValue(RenderContentsProperty, value);
        }
    }
}
