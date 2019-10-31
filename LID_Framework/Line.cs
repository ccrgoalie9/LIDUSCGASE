using SharpKml.Base;
using SharpKml.Dom;
using SharpKml.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LID_Framework {
    class Line {
        Line() {
        }

        public Line(double[,] input, string filename) {
            double[] lat = new double[(input.Length / 2)];
            double[] lon = new double[(input.Length / 2)];

            var document = new Document();
            document.Id = "null";
            LineString linestring = new LineString();
            CoordinateCollection coordinates = new CoordinateCollection();

            document.Open = true;
            document.Name = "MyDoc";
            linestring.AltitudeMode = AltitudeMode.ClampToGround;
            linestring.Extrude = true;
            linestring.Tessellate = true;

            for(int i = 0; i < (input.Length / 2); i++) {
                lon[i] = input[i, 1];
            }
            for(int i = 0; i < (input.Length / 2); i++) {
                lat[i] = input[i, 0];
            }

            for(int i = 0; i < lon.Length; i++) {
                coordinates.Add(new Vector(lat[i], lon[i]));
            }

            linestring.Coordinates = coordinates;
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

                using(FileStream stream = File.OpenWrite(filename)) {
                    kmlFile.Save(stream);

                }
            }
        }
    }
}


