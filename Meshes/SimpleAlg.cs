using System;
using System.Collections.Generic;
using PLY.Types;
using PLY;
using Model = PLY.Types.Model;

namespace Meshes
{
    /*
     * Упрощает модель до параллелипипеда, используя ply парсер
     */
    class SimpleAlg
    {
        public string cubeAlg(string MODEL_PATH)
        {
            PLYFormat pf3 = new PLYFormat();
            Model figure = pf3.Reader(MODEL_PATH);
            string newpath;
            Model fig = new Model();

            double x, y, z;
            double min = float.MaxValue;
            double max = float.MinValue;
            double minX = 0, minY = 0, minZ = 0;
            double maxX = 0, maxY = 0, maxZ = 0;

            for (int i = 0; i < figure.Vertices.Count; ++i)
            {
                x = figure.Vertices[i].X;
                y = figure.Vertices[i].Y;
                z = figure.Vertices[i].Z;
                if (Math.Sqrt(x * x + y * y + z * z) < min)
                {
                    minX = x;
                    minY = y;
                    minZ = z;
                    min = (double) Math.Sqrt(x * x + y * y + z * z);
                }

                if (Math.Sqrt(x * x + y * y + z * z) > max)
                {
                    maxX = x;
                    maxY = y;
                    maxZ = z;
                    max = (double) Math.Sqrt(x * x + y * y + z * z);
                }
            }

            List<Face> fac = new List<Face>();
            List<Edge> edg = new List<Edge>();
            List<Vertex<double>> ver = new List<Vertex<double>>();

            fac.Clear();
            edg.Clear();
            ver.Clear();

            Vertex<double> ver1 = new Vertex<double>(minX, minY, minZ);
            Vertex<double> ver2 = new Vertex<double>(maxX, maxY, maxZ);
            Vertex<double> ver3 = new Vertex<double>(minX, minY, maxZ);
            Vertex<double> ver4 = new Vertex<double>(maxX, minY, maxZ);
            Vertex<double> ver5 = new Vertex<double>(maxX, minY, minZ);
            Vertex<double> ver6 = new Vertex<double>(maxX, maxY, minZ);
            Vertex<double> ver7 = new Vertex<double>(minX, maxY, minZ);
            Vertex<double> ver8 = new Vertex<double>(minX, maxY, maxZ);

            ver.Add(ver1);
            ver.Add(ver2);
            ver.Add(ver3);
            ver.Add(ver4);
            ver.Add(ver5);
            ver.Add(ver6);
            ver.Add(ver7);
            ver.Add(ver8);

            fac.Add(new Face(3, new List<int>() {0, 3, 4}));
            fac.Add(new Face(3, new List<int>() {0, 4, 5}));
            fac.Add(new Face(3, new List<int>() {3, 4, 5}));
            fac.Add(new Face(3, new List<int>() {0, 6, 5}));
            fac.Add(new Face(3, new List<int>() {0, 2, 3}));
            fac.Add(new Face(3, new List<int>() {0, 2, 6}));
            fac.Add(new Face(3, new List<int>() {2, 7, 6}));
            fac.Add(new Face(3, new List<int>() {1, 3, 5}));
            fac.Add(new Face(3, new List<int>() {1, 2, 3}));
            fac.Add(new Face(3, new List<int>() {1, 2, 7}));
            fac.Add(new Face(3, new List<int>() {1, 6, 5}));
            fac.Add(new Face(3, new List<int>() {1, 6, 7}));


            Model figure_new = new Model(fac, edg, ver);
            return pf3.Writer(MODEL_PATH, figure_new);
        }
    }
}
