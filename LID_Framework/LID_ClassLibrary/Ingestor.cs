﻿using System;
using System.IO;

namespace LID_ClassLibrary {
    public class Ingestor {
        // Summary:
        //     Takes the input of a string and converts the coordinates to decimal. 
        //     It stores this information in an array for ease of access and use
        //     for other parts of the program.
        private string ingested;
        private string output;
        private string lineType;
        private double[,] coords;

        private int ID;

        public Ingestor(string ingested, int i) {
            ID = i;
            this.ingested = ingested;
            ConvertInput();
            CreateOutput();
        }

        public Ingestor(string ingested, int i, int j) {
            ID = i;
            this.ingested = ingested;
            ConvertInput(j);
            CreateOutput();
        }
        //Accessors
        //Accessor to return the generated array
        public double[,] GetCoordinates() {
            return coords;
        }

        public string GetIngested() {
            return ingested;
        }

        public string GetOutput() {
            return output;
        }

        public string GetLineType() {
            return lineType;
        }

        public int GetID() {
            return ID;
        }

        //Modifier
        public void SetLineType(string into) {
            lineType = into;
        }

        //Constructor's Methods
        //Convert ingest to array
        private void ConvertInput() {
            try {
                ingested = ingested.Replace(", ", ","); //Get rid of pesky spaces
                string[] tempByComma = ingested.Split(',');
                int length = tempByComma.Length;
                coords = new double[length, 2];
                //Convert from lat/long to degrees
                //North is Positive, East is Positive
                int left = 0;
                foreach (string line in tempByComma) {
                    string[] tempBySpace = line.Split(' ');
                    string temp1;
                    string temp2;
                    string direction;
                    int sign;

                    //Convert Lat
                    temp1 = tempBySpace[0].Substring(0, 2);
                    temp2 = tempBySpace[0].Substring(3, 2);
                    direction = tempBySpace[0].Substring(5, 1);
                    //Determine sign based on direction
                    if (direction == "S") {
                        sign = -1;
                    }
                    else {
                        sign = 1;
                    }
                    coords[left, 0] = (Math.Truncate((Convert.ToDouble(temp1) + Convert.ToDouble(temp2) / 60) * sign * 1000000)) / 1000000;

                    //Convert Long
                    temp1 = tempBySpace[1].Substring(0, 3);
                    temp2 = tempBySpace[1].Substring(4, 2);
                    direction = tempBySpace[1].Substring(6, 1);
                    //Determine the sign based on direction
                    if (direction == "W") {
                        sign = -1;
                    }
                    else {
                        sign = 1;
                    }
                    coords[left, 1] = (Math.Truncate((Convert.ToDouble(temp1) + Convert.ToDouble(temp2) / 60) * sign * 1000000)) / 1000000;

                    //Increment the array
                    left++;
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                //To easily determine if something went wrong
                coords = new double[,] { { -1, -1 }, { -1, -1 } };
            }
        }

        //If reading from a decimal file the coordinates are already converted
        private void ConvertInput(int i) {
            ID = i;
            if(ingested.EndsWith(" ")) { ingested = ingested.Remove(ingested.LastIndexOf(" "), 1); }
            string[] tempByComma = ingested.Split(' ');
            coords = new double[tempByComma.Length, 2];
            int left = 0;
            foreach(string line in tempByComma) {
                string[] tempBySpace = line.Split(',');
                coords[left, 0] = Convert.ToDouble(tempBySpace[0]);
                coords[left, 1] = Convert.ToDouble(tempBySpace[1]);
                left++;
            }
        }

        //Create output string from coordinate array
        private void CreateOutput() {
            try {
                output = "";
                for (int i = 0; i < (coords.Length / 2); i++) {
                    for (int j = 0; j <= 1; j++) {
                        output += coords[i, j].ToString();
                        if (j == 0) output += ",";
                    }
                    output += " ";
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                output = e.Message;
            }
        }
    }
}
