﻿using HelixToolkit.Wpf;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using Microsoft.Win32;
using PLY;
using PLY.Types;
using System.Threading;

namespace Display3DModel
{
    public partial class MainWindow : Window
    {

        string MODEL_PATH;
        ModelVisual3D device3D = new ModelVisual3D();
        ModelVisual3D device3D2 = new ModelVisual3D();


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
            device3D.Content = Display3d(MODEL_PATH, viewPort3d);
            // Add to view port
            viewPort3d.Children.Add(device3D);
        }


        /*
         * Данный метод открывает новое окно и запускает алгоритм упрощения до параллелипипеда
         */
        private void buttonAlgorithm_Click(object sender, RoutedEventArgs e)
        {
            BoundBox_AABB algorithm = new BoundBox_AABB();
            PLYFormat pf3 = new PLYFormat();
            Model figure = pf3.Reader(MODEL_PATH);
            Model result = algorithm.Simplify(figure);
            if (viewPort.Children.Contains(device3D2))
                viewPort.Children.Remove(device3D2);
            device3D2.Content = Display3d(pf3.Writer(MODEL_PATH, result), viewPort);
            viewPort.Children.Add(device3D2);
        }

        private void buttonAlgorithm_Click2(object sender, RoutedEventArgs e)
        {
            EdgeContraction algorithm = new EdgeContraction();
            PLYFormat pf3 = new PLYFormat();
            Model figure = pf3.Reader(MODEL_PATH);
            Model result = algorithm.Simplify(figure, 0.1);
            if (viewPort.Children.Contains(device3D2))
                viewPort.Children.Remove(device3D2);
            device3D2.Content = Display3d(pf3.Writer(MODEL_PATH, result), viewPort);
            viewPort.Children.Add(device3D2);
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
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            InitializeComponent();
            
        }

        /*
         * Данный метод загружает модель по передаваемому пути
         */
        private Model3D Display3d(String model, HelixViewport3D viewport)
        {
            Model3D device = null;
            try
            {
                viewport.RotateGesture = new MouseGesture(MouseAction.LeftClick);
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