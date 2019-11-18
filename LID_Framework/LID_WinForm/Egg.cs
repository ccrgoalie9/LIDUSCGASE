using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LID_WinForm {
    public partial class Egg : Form {

        char[,] gameSpace = new char[10, 10];
        int locX, locY, score, count, tresX, tresY;
        Random ran;

        public Egg() {
            InitializeComponent();
            for(int y = 0; y < 10; y++) {
                for(int x = 0; x < 10; x++) {
                    gameSpace[y, x] = '~';
                }
            }
            ran = new Random();
            locX = 4;
            locY = 4;
            score = 0;
            setTreasure();
            drawMap();
            count = 0;
        }

        private void Egg_KeyDown(object sender, KeyEventArgs e) {
            Console.WriteLine(e.KeyCode);
            if(e.KeyCode == Keys.W) {
                if(locY > 0) {
                    locY--;
                }
                drawMap();
            }
            if(e.KeyCode == Keys.S) {
                if(locY < 9) {
                    locY++;
                }
                drawMap();
            }
            if(e.KeyCode == Keys.A) {
                if(locX > 0) {
                    locX--;
                }
                drawMap();
            }
            if(e.KeyCode == Keys.D) {
                if(locX < 9) {
                    locX++;
                }
                drawMap();
            }
        }

        private void UpButton_Click(object sender, EventArgs e) {
            if (locY > 0) {
                locY--;
            }
            drawMap();
        }

        private void DownButton_Click(object sender, EventArgs e) {
            if(locY < 9) {
                locY++;
            }
            drawMap();
        }

        private void LeftButton_Click(object sender, EventArgs e) {
            if(locX > 0) {
                locX--;
            }
            drawMap();
        }

        private void RightButton_Click(object sender, EventArgs e) {
            if(locX < 9) {
                locX++;
            }
            drawMap();
        }

        private void setTreasure() {
            tresX = ran.Next(0,9);
            tresY = ran.Next(0,9);
        }

        private void drawMap() {
            string temp = "";

            //Reset TempMap
            char[,] tempMap = new char[10, 10];
            for (int y = 0; y < 10; y++) {
                for(int x = 0; x < 10; x++) {
                    tempMap[y, x] = gameSpace[y, x];
                }
            }

            //Award score if you land on the treasure
            if(tresX == locX && tresY == locY) {
                score++;
                setTreasure();
            }

            //Draw Treasure location
            tempMap[tresY, tresX] = 'T';

            //Set Character Location
            tempMap[locY, locX] = 'C';

            //Create string for display
            for(int y = 0; y < 10; y++) {
                for(int x = 0; x < 10; x++) {
                    if(x != 9) temp += tempMap[y, x] + " ";
                    else temp += tempMap[y, x];
                }
                temp += "\n";
            }
            GameSpace.Text = temp;
            ScoreLabel.Text = "Score: " + score;
            MovesLabel.Text = "Moves: " + count;

            //Create a new treasure
            if(count % 5 == 0) {
                setTreasure();
            }

            Refresh();
            count++;
        }
    }
}
