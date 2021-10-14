using HelixToolkit.Wpf;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using Microsoft.Win32;

namespace Display3DModel
{

    public partial class MainWindow : Window
    {

        private string MODEL_PATH;
        ModelVisual3D device3D = new ModelVisual3D();

        public string GetPath()
        {
            var dialog = new OpenFileDialog();
            dialog.ShowDialog();
            if (dialog.ShowDialog() == true)
                return dialog.FileName;
            return null;
        }

        private void acceptButton_Click(object sender, RoutedEventArgs e)
        {
            MODEL_PATH = GetPath();
            if (viewPort3d.Children.Contains(device3D))
                viewPort3d.Children.Remove(device3D);
            device3D.Content = Display3d(MODEL_PATH);
            // Add to view port
            viewPort3d.Children.Add(device3D);
        }

        private void buttonAlgorithm_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void escButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private Model3D Display3d(string model)
        {
            Model3D device = null;
            try
            {
                //Adding a gesture here
                viewPort3d.RotateGesture = new MouseGesture(MouseAction.LeftClick);

                //Import 3D model file
                ModelImporter import = new ModelImporter();

                //Load the 3D model file
                device = import.Load(model);
            }
            catch (Exception e)
            {
                // Handle exception in case can not file 3D model
                MessageBox.Show("Exception Error : " + e.StackTrace);
            }
            return device;
        }
    }
}
