using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;

namespace Meshes
{
    public partial class Window1 : Window
    {
        ModelVisual3D device3D = new ModelVisual3D();
        public Window1(String path)
        {
            InitializeComponent();
            device3D.Content = Display3d(path);
            viewPort3d.Children.Add(device3D);
        }

        private Model3D Display3d(String model)
        {
            Model3D device = null;
            try
            {
                viewPort3d.RotateGesture = new MouseGesture(MouseAction.LeftClick);
                ModelImporter import = new ModelImporter();
                device = import.Load(model);
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception Error : " + e.StackTrace);
            }
            return device;
        }
    }
}
