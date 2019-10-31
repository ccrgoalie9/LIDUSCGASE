using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LID_Framework {
    public partial class LID_Form : Form {
        public LID_Form() {
            InitializeComponent();
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
                i++;
            }
        }

        private void filesButton_Click(object sender, EventArgs e) {
            string filePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase) + @"\Files";
            System.Diagnostics.Process.Start("explorer.exe", filePath);
        }
    }
}
