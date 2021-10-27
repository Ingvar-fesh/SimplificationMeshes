using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf.SharpDX;
using Meshes;
using Microsoft.Win32;

namespace Display3DModel
{

    public partial class MainWindow : Window 
    {

    String MODEL_PATH;
    ModelVisual3D device3D = new ModelVisual3D();


    public String GetPath()
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
        var newWin = new Window1(MODEL_PATH);
        newWin.Show();
    }

    private void escButton_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    public MainWindow()
    {
        InitializeComponent();
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
