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
            Download test1 = new Download();
            Scraper test2 = new Scraper(test1.GetOutFile());
            Line[] testKML = new Line[test2.GetCoordinatesIngestors().Length];
            int i = 0;
            foreach(Ingestor x in test2.GetCoordinatesIngestors()) {
                testKML[i] = new Line(x.GetCoordinates(), ("Files/KML/ICEBERGS"+ x.GetID().ToString() + System.DateTime.Now.ToString().Substring(0, 10).Replace("/", "-") + ".kml"));
                i++;
            }
        }
    }
}
