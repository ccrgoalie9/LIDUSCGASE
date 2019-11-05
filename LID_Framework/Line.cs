using SharpKml.Base;
using SharpKml.Dom;
using SharpKml.Engine;
using System;
using System.IO;

namespace LID_Framework {
    class Line {
        string filepath;

        public Line(double[,] input, string filename) {
            double[] lat = new double[(input.Length / 2)];
            double[] lon = new double[(input.Length / 2)];

            var document = new Document();
            document.Id = "null";
            LineString linestring = new LineString();
            CoordinateCollection coordinates = new CoordinateCollection();


            document.Open = true;
            document.Name = filename.Replace(".kml", "");
            linestring.AltitudeMode = AltitudeMode.ClampToGround;
            linestring.Extrude = true;
            linestring.Tessellate = true;

            //THIS PART NEED TO FIGURE OUT
            for (int i = 0; i < (input.Length / 2); i++) {
                lon[i] = input[i, 1];
            }
            for (int i = 0; i < (input.Length / 2); i++) {
                lat[i] = input[i, 0];
            }

            for (int i = 0; i < lon.Length; i++) {
                coordinates.Add(new Vector(lat[i], lon[i]));
            }

            linestring.Coordinates = coordinates;
            //HERE END


            Placemark placemark = new Placemark();
            placemark.Name = "hayden";
            placemark.Visibility = false;
            placemark.Geometry = linestring;

            LineStyle lineStyle = new LineStyle();
            lineStyle.Color = Color32.Parse("501400FA");
            lineStyle.Width = 10;

            PolygonStyle PolyStyle = new PolygonStyle();
            PolyStyle.Color = Color32.Parse("501400FA");


            Style SimpleStyle = new Style();
            SimpleStyle.Id = "thisusedtobelongname";
            SimpleStyle.Line = lineStyle;
            SimpleStyle.Polygon = PolyStyle;
            document.AddStyle(SimpleStyle);
            placemark.StyleUrl = new Uri("#thisusedtobelongname", UriKind.Relative);


            document.AddFeature(placemark);
            var kml = new Kml();
            kml.Feature = document;
            KmlFile kmlFile = KmlFile.Create(kml, true);
            {

                using (FileStream stream = File.OpenWrite(filename)) {
                    kmlFile.Save(stream);

                }
            }
        }

        public Line(Ingestor[] input) {
            filepath = (@"Files\KML\" + DateTime.UtcNow.ToString().Replace(" ", "  ").Substring(0, 10).Replace("/", "-").Replace(" ", "") + "_ICEBERGS.kml").Replace(" ", "");
            string filename = (DateTime.UtcNow.ToString().Replace(" ", "  ").Substring(0, 10).Replace("/", "-").Replace(" ", "") + "_ICEBERGS").Replace(" ", "");

            //Check if file already exists
            if (File.Exists(filepath)) {
                File.Delete(filepath);
            }

            //Document Creation
            var document = new Document();
            document.Id = "KML";
            document.Open = true;
            document.Name = filename;

            //Styling
            LineStyle lineStyle = new LineStyle();
            string colorCode = "ffffe481";
            lineStyle.Color = Color32.Parse(colorCode);
            lineStyle.Width = 5;
            PolygonStyle PolyStyle = new PolygonStyle();
            PolyStyle.Color = Color32.Parse(colorCode);

            //Actual reads style and adds poly and line together
            Style SimpleStyle = new Style();
            string styleID = "lineStyle";
            SimpleStyle.Id = styleID;
            SimpleStyle.Line = lineStyle;
            SimpleStyle.Polygon = PolyStyle;
            document.AddStyle(SimpleStyle);

            //LINE STRING & PLACEMARK CONSTRUCTION ZONE
            foreach (Ingestor ingest in input) {
                //One per segment
                LineString linestring = new LineString();
                CoordinateCollection coordinates = new CoordinateCollection();
                linestring.AltitudeMode = AltitudeMode.ClampToGround;
                linestring.Extrude = true;
                linestring.Tessellate = true;

                double[,] coordArray = ingest.GetCoordinates();

                double[] lat = new double[(coordArray.Length / 2)];
                double[] lon = new double[(coordArray.Length / 2)];

                for (int i = 0; i < (coordArray.Length / 2); i++) {
                    lon[i] = coordArray[i, 1];
                }
                for (int i = 0; i < (coordArray.Length / 2); i++) {
                    lat[i] = coordArray[i, 0];
                }

                for (int i = 0; i < lon.Length; i++) {
                    coordinates.Add(new Vector(lat[i], lon[i]));
                }

                linestring.Coordinates = coordinates;
                Placemark placemark = new Placemark();
                placemark.Name = ingest.GetLineType();
                placemark.Visibility = true;
                placemark.Geometry = linestring;
                placemark.StyleUrl = new Uri(("#" + styleID), UriKind.Relative); //Uri makes url refrence to indocument style rather than cloud sourced

                document.AddFeature(placemark);

            }
            //END LINE STRING CONSTRUCTION ZONE


            //Creates KML assignes it from document
            var kml = new Kml();
            kml.Feature = document;

            //Outputs KML File
            KmlFile kmlFile = KmlFile.Create(kml, true);
            using (FileStream stream = File.OpenWrite(filepath)) {
                kmlFile.Save(stream);
            }
        }

        //Accessor Methods
        public string GetOutFile() {
            return filepath;
        }


    }
}


