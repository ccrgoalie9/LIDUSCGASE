using SharpKml.Base;
using SharpKml.Dom;
using SharpKml.Engine;
using System;
using System.IO;

namespace LID_ClassLibrary {
    public class Line {
        readonly string filepath;

        //Test
        public Line(double[,] input, string filename) {
            double[] lat = new double[(input.Length / 2)];
            double[] lon = new double[(input.Length / 2)];

            var document = new Document() {
                Id = "null"
            };
            LineString linestring = new LineString();
            CoordinateCollection coordinates = new CoordinateCollection();


            document.Open = true;
            document.Name = filename.Replace(".kml", "");
            linestring.AltitudeMode = AltitudeMode.ClampToGround;
            linestring.Extrude = true;
            linestring.Tessellate = true;

            //THIS PART NEED TO FIGURE OUT
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
            //HERE END


            Placemark placemark = new Placemark {
                Name = "hayden",
                Visibility = false,
                Geometry = linestring
            };

            LineStyle lineStyle = new LineStyle {
                Color = Color32.Parse("501400FA"),
                Width = 10
            };

            PolygonStyle PolyStyle = new PolygonStyle {
                Color = Color32.Parse("501400FA")
            };


            Style SimpleStyle = new Style {
                Id = "thisusedtobelongname",
                Line = lineStyle,
                Polygon = PolyStyle
            };
            document.AddStyle(SimpleStyle);
            placemark.StyleUrl = new Uri("#thisusedtobelongname", UriKind.Relative);


            document.AddFeature(placemark);
            var kml = new Kml {
                Feature = document
            };
            KmlFile kmlFile = KmlFile.Create(kml, true);
            {

                using(FileStream stream = File.OpenWrite(filename)) {
                    kmlFile.Save(stream);

                }
            }
        }

        //Normal Implementation
        public Line(Ingestor[] input, Config config) {
            filepath = (config.DirPath + @"\KML\" + DateTime.UtcNow.ToString("yyyy-MM-dd") + "_ICEBERGS.kml");
            string filename = (DateTime.UtcNow.ToString("yyyy-MM-dd") + "_ICEBERGS");

            //Check if file already exists
            if(File.Exists(filepath)) {
                File.Delete(filepath);
            }

            //Document Creation
            var document = new Document {
                Id = "KML",
                Open = true,
                Name = filename
            };

            //Styling
            string colorCode1 = config.KmlColor1;
            string colorCode2 = config.KmlColor2;
            string colorCode3 = config.KmlColor3;
            /*
            LineStyle lineStyle = new LineStyle {
                Color = Color32.Parse(colorCode),
                Width = config.KmlWidth
            };
            */

            /*
            PolygonStyle PolyStyle = new PolygonStyle {
                Color = Color32.Parse(colorCode)
            };
            */

            //Timespan
            SharpKml.Dom.TimeSpan lineTimespan = new SharpKml.Dom.TimeSpan {
                Begin = Convert.ToDateTime(DateTime.UtcNow.ToString("yyyy-MM-dd") + " 00:00:01"),
                End = Convert.ToDateTime(DateTime.UtcNow.ToString("yyyy-MM-dd") + " 23:59:59")
            };


            //Actual reads style and adds poly and line together
            string styleIL = "lineIcebergLimit";
            string styleEIL = "lineEstimatedIcebergLimit";
            string styleSIL = "lineSeaIceLimit";
            {
                //Style for Iceberg Limit
                Style SimpleStyle = new Style();
                SimpleStyle.Id = styleIL;
                SimpleStyle.Line = new LineStyle {
                    Color = Color32.Parse(colorCode1),
                    Width = config.KmlWidth
                };
                SimpleStyle.Polygon = new PolygonStyle {
                    Color = Color32.Parse(colorCode1)
                };
                document.AddStyle(SimpleStyle);
                //Style for Estimated Iceberg Limit
                SimpleStyle = new Style();
                SimpleStyle.Id = styleEIL;
                SimpleStyle.Line = new LineStyle {
                    Color = Color32.Parse(colorCode2),
                    Width = config.KmlWidth
                };
                SimpleStyle.Polygon = new PolygonStyle {
                    Color = Color32.Parse(colorCode2)
                };
                document.AddStyle(SimpleStyle);
                //Style for Sea Ice Limit
                SimpleStyle = new Style();
                SimpleStyle.Id = styleSIL;
                SimpleStyle.Line = new LineStyle {
                    Color = Color32.Parse(colorCode3),
                    Width = config.KmlWidth
                };
                SimpleStyle.Polygon = new PolygonStyle {
                    Color = Color32.Parse(colorCode3)
                };
                document.AddStyle(SimpleStyle);
            }

            //LINE STRING & PLACEMARK CONSTRUCTION ZONE
            foreach(Ingestor ingest in input) {
                //One per segment
                LineString linestring = new LineString();
                CoordinateCollection coordinates = new CoordinateCollection();
                linestring.AltitudeMode = AltitudeMode.ClampToGround;
                linestring.Extrude = true;
                linestring.Tessellate = true;

                double[,] coordArray = ingest.GetCoordinates();

                double[] lat = new double[(coordArray.Length / 2)];
                double[] lon = new double[(coordArray.Length / 2)];

                for(int i = 0; i < (coordArray.Length / 2); i++) {
                    lon[i] = coordArray[i, 1];
                }
                for(int i = 0; i < (coordArray.Length / 2); i++) {
                    lat[i] = coordArray[i, 0];
                }

                for(int i = 0; i < lon.Length; i++) {
                    coordinates.Add(new Vector(lat[i], lon[i]));
                }

                linestring.Coordinates = coordinates;
                Placemark placemark = new Placemark {
                    Name = ingest.GetLineType(),
                    Visibility = true,
                    Geometry = linestring,
                    //Moved StyleUrl out in order to be able to set it conditionally
                    //Moved Description out as well
                    Time = lineTimespan                                    //Timespan
                };
                if(ingest.GetLineType().Contains("ESTIMATED ICEBERG LIMIT")) {
                    placemark.StyleUrl = new Uri(("#" + styleEIL), UriKind.Relative); //Uri makes url refrence to indocument style rather than cloud sourced
                    placemark.Description = new Description() { Text = "Based off of data from Greenland" };
                } else if(ingest.GetLineType().Contains("ICEBERG LIMIT")) {
                    placemark.StyleUrl = new Uri(("#" + styleIL), UriKind.Relative);
                    placemark.Description = new Description() { Text = "Based off of data from the IIP and Canadian Ice Patrol" };
                } else if(ingest.GetLineType().Contains("SEA ICE LIMIT")) {
                    placemark.StyleUrl = new Uri(("#" + styleSIL), UriKind.Relative);
                    placemark.Description = new Description() { Text = "Sea ice within limit displayed" };
                } else {
                    placemark.StyleUrl = new Uri(("#" + styleIL), UriKind.Relative);
                    placemark.Description = new Description() { Text = "Based off of data from the IIP and Canadian Ice Patrol" };
                }

                document.AddFeature(placemark);

            }
            //END LINE STRING CONSTRUCTION ZONE


            //Creates KML assignes it from document
            var kml = new Kml {
                Feature = document
            };

            //Outputs KML File
            KmlFile kmlFile = KmlFile.Create(kml, true);
            using(FileStream stream = File.OpenWrite(filepath)) {
                kmlFile.Save(stream);
            }
        }

        //For Historic KML Creation
        public Line(Ingestor[] input, DateTime historic, Config config) {
            filepath = (config.DirPath + @"\KML\" + historic.ToString("yyyy-MM-dd") + "_ICEBERGS.kml");
            string filename = (historic.ToString("yyyy-MM-dd") + "_ICEBERGS");

            //Check if file already exists
            if(File.Exists(filepath)) {
                File.Delete(filepath);
            }

            //Document Creation
            var document = new Document {
                Id = "KML",
                Open = true,
                Name = filename
            };

            //Styling
            string colorCode1 = config.KmlColor1;
            string colorCode2 = config.KmlColor2;
            string colorCode3 = config.KmlColor3;

            //Timespan
            SharpKml.Dom.TimeSpan lineTimespan = new SharpKml.Dom.TimeSpan {
                Begin = Convert.ToDateTime(historic.ToString("yyyy-MM-dd") + " 00:00:01"),
                End = Convert.ToDateTime(historic.ToString("yyyy-MM-dd") + " 23:59:59")
            };

            //Actual reads style and adds poly and line together
            string styleIL = "lineIcebergLimit";
            string styleEIL = "lineEstimatedIcebergLimit";
            string styleSIL = "lineSeaIceLimit";
            {
                //Style for Iceberg Limit
                Style SimpleStyle = new Style();
                SimpleStyle.Id = styleIL;
                SimpleStyle.Line = new LineStyle {
                    Color = Color32.Parse(colorCode1),
                    Width = config.KmlWidth
                };
                SimpleStyle.Polygon = new PolygonStyle {
                    Color = Color32.Parse(colorCode1)
                };
                document.AddStyle(SimpleStyle);
                //Style for Estimated Iceberg Limit
                SimpleStyle = new Style();
                SimpleStyle.Id = styleEIL;
                SimpleStyle.Line = new LineStyle {
                    Color = Color32.Parse(colorCode2),
                    Width = config.KmlWidth
                };
                SimpleStyle.Polygon = new PolygonStyle {
                    Color = Color32.Parse(colorCode2)
                };
                document.AddStyle(SimpleStyle);
                //Style for Sea Ice Limit
                SimpleStyle = new Style();
                SimpleStyle.Id = styleSIL;
                SimpleStyle.Line = new LineStyle {
                    Color = Color32.Parse(colorCode3),
                    Width = config.KmlWidth
                };
                SimpleStyle.Polygon = new PolygonStyle {
                    Color = Color32.Parse(colorCode3)
                };
                document.AddStyle(SimpleStyle);
            }

            //LINE STRING & PLACEMARK CONSTRUCTION ZONE
            foreach(Ingestor ingest in input) {
                //One per segment
                LineString linestring = new LineString();
                CoordinateCollection coordinates = new CoordinateCollection();
                linestring.AltitudeMode = AltitudeMode.ClampToGround;
                linestring.Extrude = true;
                linestring.Tessellate = true;

                double[,] coordArray = ingest.GetCoordinates();

                double[] lat = new double[(coordArray.Length / 2)];
                double[] lon = new double[(coordArray.Length / 2)];

                for(int i = 0; i < (coordArray.Length / 2); i++) {
                    lon[i] = coordArray[i, 1];
                }
                for(int i = 0; i < (coordArray.Length / 2); i++) {
                    lat[i] = coordArray[i, 0];
                }

                for(int i = 0; i < lon.Length; i++) {
                    coordinates.Add(new Vector(lat[i], lon[i]));
                }

                linestring.Coordinates = coordinates;
                Placemark placemark = new Placemark {
                    Name = ingest.GetLineType(),
                    Visibility = true,
                    Geometry = linestring,
                    Time = lineTimespan
                };
                //Variably set Style and Description
                if(ingest.GetLineType().Contains("ESTIMATED ICEBERG LIMIT")) {
                    placemark.StyleUrl = new Uri(("#" + styleEIL), UriKind.Relative); //Uri makes url refrence to indocument style rather than cloud sourced
                    placemark.Description = new Description() { Text = "Based off of data from Greenland" };
                } else if(ingest.GetLineType().Contains("ICEBERG LIMIT")) {
                    placemark.StyleUrl = new Uri(("#" + styleIL), UriKind.Relative);
                    placemark.Description = new Description() { Text = "Based off of data from the IIP and Canadian Ice Patrol" };
                } else if(ingest.GetLineType().Contains("SEA ICE LIMIT")) {
                    placemark.StyleUrl = new Uri(("#" + styleSIL), UriKind.Relative);
                    placemark.Description = new Description() { Text = "Sea ice within limit displayed" };
                } else {
                    placemark.StyleUrl = new Uri(("#" + styleIL), UriKind.Relative);
                    placemark.Description = new Description() { Text = "Based off of data from the IIP and Canadian Ice Patrol" };
                }

                document.AddFeature(placemark);

            }
            //END LINE STRING CONSTRUCTION ZONE


            //Creates KML assignes it from document
            var kml = new Kml {
                Feature = document
            };

            //Outputs KML File
            KmlFile kmlFile = KmlFile.Create(kml, true);
            using(FileStream stream = File.OpenWrite(filepath)) {
                kmlFile.Save(stream);
            }
        }

        //Accessor Methods
        public string GetOutFile() {
            return filepath;
        }
    }
}


