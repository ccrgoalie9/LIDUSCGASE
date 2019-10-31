using SharpKml.Base;
using SharpKml.Dom;
using SharpKml.Engine;
using System;
using System.Data;
using System.IO;

namespace LID_Framework {
    class Program {

        static void Main(string[] args) {
            Console.Title = "LID";
            
            //Get the current Bulletin
            Download test1 = new Download();

            //Get the necessary bits from the bulletin
            Scraper test2 = new Scraper(test1.GetOutFile());

            //Set the length for the KML creator array
            Line[] testKML = new Line[test2.GetCoordinatesIngestors().Length];

            //String of names for the kml files to open automatically later maybe
            string[] kmls = new string[test2.GetCoordinatesIngestors().Length];
            
            //Create the KML files in format: 'date'_ICEBERGS_'ID'.kml
            int i = 0;
            foreach(Ingestor x in test2.GetCoordinatesIngestors()) {
                kmls[i] = "Files/KML/" + System.DateTime.UtcNow.ToString().Substring(0, 10).Replace("/", "-") + "_ICEBERGS_" + x.GetID().ToString() + ".kml";
                testKML[i] = new Line(x.GetCoordinates(), (kmls[i]));

                //Testing
                Console.WriteLine("Line Type for ID_" + x.GetID() + ": " + x.GetLineType());
                //Testing

                i++;
            }
            //Testing
            Console.ReadKey();
            //Testing
        }
    }
}
