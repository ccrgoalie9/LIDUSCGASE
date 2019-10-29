using SharpKml.Base;
using SharpKml.Dom;
using SharpKml.Engine;
using System;
using System.Data;
using System.IO;

namespace KMLmaker
{
    class Program
    {


        static void Main(string[] args)
        {
            Injestor test = new Injestor("Coordinates2.txt");
            double[,] ah = test.GetCoordinates();
            Line oh = new Line(ah,"attempt4.kml");
        }

    }
}
