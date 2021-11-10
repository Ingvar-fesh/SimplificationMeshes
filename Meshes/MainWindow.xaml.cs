using HelixToolkit.Wpf;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using Meshes;
using Microsoft.Win32;

namespace Display3DModel
{
    public partial class MainWindow : Window 
    {

        string MODEL_PATH;
        ModelVisual3D device3D = new ModelVisual3D();

        
        /*
         * Данный метод получает путь к объекту
         */
        public String GetPath()
        {
            var dialog = new OpenFileDialog();
            dialog.ShowDialog();
            if (dialog.ShowDialog() == true)
                return dialog.FileName;
            return null;
        }

        /*
         * Данный метод загружает 3D модель по пути MODEL_PATH
         */
        private void acceptButton_Click(object sender, RoutedEventArgs e)
        {
            MODEL_PATH = GetPath();
            if (viewPort3d.Children.Contains(device3D))
                viewPort3d.Children.Remove(device3D);
            device3D.Content = Display3d(MODEL_PATH);
            // Add to view port
            viewPort3d.Children.Add(device3D);
        }


        /*
         * Данный метод открывает новое окно и запускает алгоритм упрощения до параллелипипеда
         */
        private void buttonAlgorithm_Click(object sender, RoutedEventArgs e)
        {
            SimpleAlg alg = new SimpleAlg();
            var newWin = new Window1(alg.cubeAlg(MODEL_PATH));
            newWin.Show();
        }
            
        /*
         * Данный метод закрывает весь проект
         */
        private void escButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        /*
         * Данный метод загружает модель по передаваемому пути
         */
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
