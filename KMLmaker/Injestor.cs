﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMLmaker
{
    class Injestor
    {
        private string inFileName;
        private string outFileName;
        private string injested;
        private string output;
        private double[,] coords;

        private int ID;

        public Injestor()
        {

        }

        //For testing, to make sure that input of lat/longs can be processed (of format directly scraped from the bulletin
        public Injestor(string inFile)
        {
            inFileName = inFile;
            CheckInput();
            ReadFile();
            ConvertInput();
            CreateOutput();
            WriteFile();
        }
        //For testing

        public Injestor(string ingested, int i)
        {
            ID = i;
            injested = ingested;
            ConvertInput();
            CreateOutput();
        }

        //Accessors
        //Accessor to return the generated array
        public double[,] GetCoordinates()
        {
            return coords;
        }

        public string GetInjested()
        {
            return injested;
        }

        public string GetOutput()
        {
            return output;
        }

        public string GetInFileName()
        {
            return inFileName;
        }

        public string GetOutFileName()
        {
            return outFileName;
        }

        public int GetID()
        {
            return ID;
        }



        //Constructor's Methods
        //Make sure the input is machine readable
        private void CheckInput()
        {
            if (!(inFileName.EndsWith(".txt")))
            {
                inFileName = inFileName + ".txt";
            }
            outFileName = inFileName.Replace(".txt", ("_" + System.DateTime.Now.ToString().Substring(0, 8).Replace("/", "-") + ".txt"));
        }

        //Read from the file
        private void ReadFile()
        {
            try
            {
                injested = "";
                using (StreamReader coordFile = new StreamReader("files/" + inFileName))
                {
                    injested = coordFile.ReadToEnd();
                    //injested = injested.Replace(", ", ","); //Get rid of pesky spaces
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                injested = "Read Failed";
            }
        }

        //Convert injest to array
        private void ConvertInput()
        {
            try
            {
                injested = injested.Replace(", ", ","); //Get rid of pesky spaces
                string[] tempByComma = injested.Split(',');
                int length = tempByComma.Length;
                coords = new double[length, 2];
                //Convert from lat/long to degrees
                //North is Positive, East is Positive
                int left = 0;
                foreach (string line in tempByComma)
                {
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
                    if (direction == "S")
                    {
                        sign = -1;
                    }
                    else
                    {
                        sign = 1;
                    }
                    coords[left, 0] = (Math.Truncate((Convert.ToDouble(temp1) + Convert.ToDouble(temp2) / 60) * sign * 1000000)) / 1000000;

                    //Convert Long
                    temp1 = tempBySpace[1].Substring(0, 3);
                    temp2 = tempBySpace[1].Substring(4, 2);
                    direction = tempBySpace[1].Substring(6, 1);
                    //Determine the sign based on direction
                    if (direction == "W")
                    {
                        sign = -1;
                    }
                    else
                    {
                        sign = 1;
                    }
                    coords[left, 1] = (Math.Truncate((Convert.ToDouble(temp1) + Convert.ToDouble(temp2) / 60) * sign * 1000000)) / 1000000;

                    //Increment the array
                    left++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //To easily determine if something went wrong
                coords = new double[,] { { -1, -1 }, { -1, -1 } };
            }
        }

        //Create output string from coordinate array
        private void CreateOutput()
        {
            try
            {
                output = "";
                for (int i = 0; i < (coords.Length / 2); i++)
                {
                    for (int j = 0; j <= 1; j++)
                    {
                        output += coords[i, j].ToString();
                        if (j == 0) output += " ";
                    }
                    output += "\n";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                output = e.Message;
            }
        }

        //Write to new file
        private void WriteFile()
        {
            try
            {
                using (StreamWriter decimalFile = new StreamWriter("files/" + outFileName))
                {
                    decimalFile.Write(output);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    } //End Of Class
}
