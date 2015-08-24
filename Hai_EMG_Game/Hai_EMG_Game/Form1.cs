﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


/*
Author: Hai Tang (haitang@jhu.edu)
*/
namespace Hai_EMG_Game
{
    public partial class MainForm : Form
    {
        //Data Acquisition and Bit Manipulation
        int counter = 0;
        int[] receivedBuffer = new int[500];
        int firstByte;
        int secondByte;
        int thirdByte;
        int combine;
        bool sign = false;
        int[] envelop = new int[1000000];//1000s
        Encoding enc = Encoding.GetEncoding(1252);

        //Version D and OB Digitization
        int[] DACenvelop = new int[1000000];//Only for D2, 0-255
        int[] digitizedEnvelop = new int[1000000];
        int signalPeakD2 = 800;
        double stepSizeD2 = 256.0 / 77.0; //0-255, digitizedLevel = 77; NOTE: Must add XX.0 to ensure double accuracy. Otherwise 256/77=3.
        int signalPeakOB = 1024;
        double stepSizeOB = 1024.0 / 100.0; //digitiedLevel = 100

        //Display
        int DisplayLength = 10000; //Sampling rate = 1000
        int disp;
        Boolean showDigitized = false;

        //Target Levels
        int elapsedTime = 0;
        int center = 0;
        int halfWidth = 5;
        int timeInterval = 5;
        int spaceInterval = 10;
        int hitCounts = 0;
        int peakLevel = 0;//77 for D2 and 90 for OB, i.e., 10-70 for D2 and 10-90 for OB
        Boolean hitCountsRefresh = false;
        double hitThreshold = 0.001; // hitThreshold of timeInterval in target area means really hit
        Boolean isGameStart = false;
        int totalHits = 0;
        Boolean totalHitsCounted = false;
        int totalTrials = 0;

        //Recording and Reading
        string savingPath = "C:\\Users\\Owner\\Desktop\\Game_Data\\";
        FileStream myFileStream;
        StreamWriter myStreamWriter;
        Boolean recording = false;
        string readingPath;
        int[] savedEnvelop = new int[1000000];//1000s
        int[] savedDigitizedEnvelop = new int[1000000];
        string electrode = "";
        string filePath;

        //Training
        int trainingTime = 5;
        int trainingElapsed = 0;


        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button_stop_recording.Enabled = false;
            electrode = "IBT";
            button_IBTVD.Enabled = false;
            button_pause.Enabled = false;
        }

        private void serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            
            for (int i = 0; i < receivedBuffer.Length; i++)
            {
                if (serialPort.IsOpen)
                {
                    receivedBuffer[i] = serialPort.ReadByte();//Read a byte from the stream and advances the position within the stream by one byte, or returns -1 if at the end of the stream.
                }
            }

            for (int i = 0; i <= receivedBuffer.Length - 5; i++)
            {
                //refresh the buffer and reset counter when it's full
                if (counter == 1000000) 
                {
                    Array.Clear(envelop, 0, envelop.Length);
                    Array.Clear(digitizedEnvelop, 0, digitizedEnvelop.Length);
                    counter = 0;
                }

                if (receivedBuffer[i] == 35 && receivedBuffer[i + 4] == 36)
                {
                    //NOTE: Least significant bit comes first!
                    firstByte = receivedBuffer[i + 1];
                    secondByte = receivedBuffer[i + 2];
                    thirdByte = receivedBuffer[i + 3];

                    combine = firstByte << 16 | secondByte << 8 | thirdByte; //Concat three bytes together bitwisely
                    sign = GetBit(firstByte, 7); //When the sign bit is 0 (false), positive; 1 (true), negative
                    if (!sign) //Positive number
                    {
                        envelop[counter] = combine;
                    }
                    else //Negative number
                    {
                        //combine = (~firstByte) << 16 | (~secondByte) << 8 | (~thirdByte); //Take the complement. ~ is complement!
                        //Pad the extra leading byte for int with 11111111. Which is the "sign byte"
                        for (int temp = 24; temp < 32; temp++)
                        {
                            combine = combine | (1 << temp);
                        }
                        envelop[counter] = combine; //Take the correct negative value. Not need to take complement and plus 1 any more.
                    }

                    if(electrode == "IBT")
                    {
                        DACenvelop[counter] = envelop[counter] * 255 / signalPeakD2;
                        if (DACenvelop[counter] > 255)
                        {
                            DACenvelop[counter] = 255;
                        }
                        digitizedEnvelop[counter] = (int)(DACenvelop[counter] / stepSizeD2); //No need to floor an int since it's auto truncked.
                    }
                    else if(electrode == "OttoBock")
                    {
                        digitizedEnvelop[counter] = (int)(envelop[counter] / stepSizeOB);
                    }


                    //Put the data into recording file
                    if (recording)
                    {
                        myStreamWriter.Write(counter.ToString() + '\t' + envelop[counter].ToString() + "\t" + digitizedEnvelop[counter].ToString() + "\t");
                        myStreamWriter.WriteLine();
                    }

                    counter++;
                }
            }

            System.Threading.Thread.Sleep(10);//Slightly delay for 100; 50 works fine; 30 works fine. 10 works fine. 
        }

        private static bool GetBit(int b, int bitNum) // a=11010010, GetBit(a,0) = 0; GetBit(a,7) = 1;
        {
            return (b & (1 << bitNum)) != 0;
        }

        private Byte ReverseBits(Byte originalByte)
        {
            int result = 0;
            for(int i=0; i<8; i++)
            {
                result = result << 1;
                result += originalByte & 1;
                originalByte = (Byte)(originalByte >> 1);
            }
            return (Byte)result;
        }


        private void button_start_Click(object sender, EventArgs e)
        {
            try
            {
                if (!serialPort.IsOpen)
                {
                    serialPort.Open(); 
                }
                button_startDisplay.Enabled = false;
                button_pause.Enabled = true;
            }
            catch
            {
                MessageBox.Show("Cannot Open Serial Port. Check COM Number or Availability.");
            }

            timer_display.Enabled = true;
        }

        private void timer_display_Tick(object sender, EventArgs e)
        {
            this.Invoke(new EventHandler(DisplayData));
        }

        private void DisplayData(object s, EventArgs e)
        {
            //Bar Graph
            if (counter > 1)
            {
                if (isGameStart && center != 0)
                {
                    if (hitCountsRefresh) //Every time the target area updated, refresh the hitCounts.
                    {
                        hitCounts = 0;
                        hitCountsRefresh = false;
                    }
                    if (digitizedEnvelop[counter - 1] > center - halfWidth && digitizedEnvelop[counter - 1] < center + halfWidth)
                    {
                        hitCounts++; //Count the accumulated time in the target area
                    }
                }

                this.chart_DigitBar.Series["BarEMGVal"].Points.Clear();
                this.chart_DigitBar.Series["targetLevel"].Points.Clear();

                if (electrode == "IBT")
                {
                    this.chart_DigitBar.ChartAreas[0].AxisY.Maximum = 80;
                    this.chart_DigitBar.ChartAreas[0].AxisY.Minimum = 0;
                    this.chart_DigitBar.ChartAreas[0].AxisY.Interval = 10;
                    this.chart_DigitBar.Titles["Real Time Bar"].Text = "D2 Real Time Bar (0-77)";
                }
                else if (electrode == "OttoBock")
                {
                    this.chart_DigitBar.ChartAreas[0].AxisY.Maximum = 100;
                    this.chart_DigitBar.ChartAreas[0].AxisY.Minimum = 0;
                    this.chart_DigitBar.ChartAreas[0].AxisY.Interval = 10;
                    this.chart_DigitBar.Titles["Real Time Bar"].Text = "OB Real Time Bar (0-100)";
                }

                this.chart_DigitBar.Series["BarEMGVal"].Points.AddXY("Strength", 0, digitizedEnvelop[counter - 1]); //Note that counter++ after putting in data. So we need counter - 1 here!!!
                this.chart_DigitBar.Series["BarEMGVal"]["DrawSideBySide"] = "false"; //Overlap two series
                this.chart_DigitBar.Series[0].Color = Color.FromArgb(200, 255, 0, 0); //Set color and transparency //Red
                this.chart_DigitBar.Series[0].BorderColor = Color.FromArgb(200, 0, 0, 128); //Navy

                this.chart_DigitBar.Series["targetLevel"].Points.AddXY("Strength", center - halfWidth, center + halfWidth);
                this.chart_DigitBar.Series["targetLevel"]["DrawSideBySide"] = "false";
                this.chart_DigitBar.Series[1].Color = Color.FromArgb(200, 255, 215, 0); //Gold
                this.chart_DigitBar.Series[1].BorderColor = Color.FromArgb(200, 184, 131, 11); //Dark Gold
                this.chart_DigitBar.Series[1].BorderWidth = 5;

                if (hitCounts > timeInterval * 1000 * hitThreshold)
                {
                    this.chart_DigitBar.Series[1].Color = Color.FromArgb(200, 0, 255, 0); //Green
                    this.chart_DigitBar.Series[1].BorderColor = Color.FromArgb(200, 0, 100, 0); //Dark Green
                    this.chart_DigitBar.Series[1].BorderWidth = 5;

                    if (!totalHitsCounted)
                    {
                        totalHits++;
                        totalHitsCounted = true;
                    }
                    
                }

                if(center == 0)
                {
                    this.chart_DigitBar.Series[1].Color = Color.FromArgb(0, 0, 0, 0); //Disappear
                    this.chart_DigitBar.Series[1].BorderWidth = 0;
                }
            }

            //Real Time EMG Graph
            this.chart_EMGrealtime.Series["EMGVal"].Points.Clear();
            if (counter >= DisplayLength)
            {
                for (disp = counter - DisplayLength; disp < counter; disp++)
                {
                    if (!showDigitized)
                    {
                        this.chart_EMGrealtime.ChartAreas[0].AxisY.Maximum = Double.NaN; //Default AutoScale
                        this.chart_EMGrealtime.ChartAreas[0].AxisY.Minimum = Double.NaN;
                        this.chart_EMGrealtime.ChartAreas[0].AxisY.Interval = Double.NaN;
                        this.chart_EMGrealtime.Titles["EMG_Envelop"].Text = "Filtered EMG Signal";
                        this.chart_EMGrealtime.Series["EMGVal"].Points.AddXY((disp / 1000).ToString(), envelop[disp]);
                    }
                    else //Show Digitized EMG
                    {
                        if(electrode == "IBT")
                        {
                            this.chart_EMGrealtime.ChartAreas[0].AxisY.Maximum = 80;
                            this.chart_EMGrealtime.ChartAreas[0].AxisY.Minimum = 0;
                            this.chart_EMGrealtime.ChartAreas[0].AxisY.Interval = 10;
                            this.chart_EMGrealtime.Titles["EMG_Envelop"].Text = "Digitized EMG Signal (0-77)";
                        }
                        else if(electrode == "OttoBock")
                        {
                            this.chart_EMGrealtime.ChartAreas[0].AxisY.Maximum = 100;
                            this.chart_EMGrealtime.ChartAreas[0].AxisY.Minimum = 0;
                            this.chart_EMGrealtime.ChartAreas[0].AxisY.Interval = 10;
                            this.chart_EMGrealtime.Titles["EMG_Envelop"].Text = "Digitized EMG Signal (0-100)";
                        }
                        this.chart_EMGrealtime.Series["EMGVal"].Points.AddXY((disp / 1000).ToString(), digitizedEnvelop[disp]);

                    }
                }
            }
            else //Time elapsed less than display length
            {
                for (disp = 0; disp < DisplayLength; disp++)
                {
                    if (!showDigitized)
                    {
                        this.chart_EMGrealtime.ChartAreas[0].AxisY.Maximum = Double.NaN;//Default AutoScale
                        this.chart_EMGrealtime.ChartAreas[0].AxisY.Minimum = Double.NaN;
                        this.chart_EMGrealtime.ChartAreas[0].AxisY.Interval = Double.NaN;
                        this.chart_EMGrealtime.Titles["EMG_Envelop"].Text = "Filtered EMG Signal";
                        this.chart_EMGrealtime.Series["EMGVal"].Points.AddXY((disp / 1000).ToString(), envelop[disp]);
                    }
                    else //Show Digitized EMG
                    {
                        if (electrode == "IBT")
                        {
                            this.chart_EMGrealtime.ChartAreas[0].AxisY.Maximum = 80;
                            this.chart_EMGrealtime.ChartAreas[0].AxisY.Minimum = 0;
                            this.chart_EMGrealtime.ChartAreas[0].AxisY.Interval = 10;
                            this.chart_EMGrealtime.Titles["EMG_Envelop"].Text = "Digitized EMG Signal (0-77)";
                        }
                        else if (electrode == "OttoBock")
                        {
                            this.chart_EMGrealtime.ChartAreas[0].AxisY.Maximum = 100;
                            this.chart_EMGrealtime.ChartAreas[0].AxisY.Minimum = 0;
                            this.chart_EMGrealtime.ChartAreas[0].AxisY.Interval = 10;
                            this.chart_EMGrealtime.Titles["EMG_Envelop"].Text = "Digitized EMG Signal (0-100)";
                        }
                        this.chart_EMGrealtime.Series["EMGVal"].Points.AddXY((disp / 1000).ToString(), digitizedEnvelop[disp]);
                    }
                }
            }
        }

        private void trackBar_displayLength_Scroll(object sender, EventArgs e)
        {
            DisplayLength = trackBar_displayLength.Value;
            label_trackBar.Text = "Display Length: " + (trackBar_displayLength.Value / 1000).ToString() + " s";
        }

        private void timer_targetLevel_Tick(object sender, EventArgs e)
        {
            elapsedTime++;
            if(elapsedTime % timeInterval == 0) // update every timeInterval second
            {
                center += spaceInterval;
                totalTrials++;
                hitCountsRefresh = true;
                totalHitsCounted = false;
            }

            if(electrode == "IBT")
            {
                peakLevel = 77;
            }
            else if(electrode == "OttoBock")
            {
                peakLevel = 90;
            }

            if(center > peakLevel)
            {
                timer_targetLevel.Enabled = false;
                center = 0;
                MessageBox.Show("Game Finished! You get " + totalHits + " Hits out of " + totalTrials + " Trials!" );
                isGameStart = false;
                button_StartGame.Enabled = true;
                myStreamWriter.Write("Game Finished! You get " + totalHits + " Hits out of " + totalTrials + " Trials!");
                button_stop_recording_Click(sender, e);
                button_StartGame.BackColor = Color.Gold;
            }

        }

        private void button_StartGame_Click(object sender, EventArgs e)
        {
            button_start_Click(sender, e);
            button_StartGame.BackColor = Color.Lime;

            if (textBox_subjectName.Text != "")
            {
                totalHits = 0;
                totalTrials = 0; //Already including the initial one, since it actually counts from 10, 20, ... 80 to get stopped, but the real number should be 7 (10-70).
                isGameStart = true;
                timer_targetLevel.Enabled = true;
                center = spaceInterval;
                button_StartGame.Enabled = false;

                button_start_recording_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Please Enter Your Name!");
            }
        }

        private void button_switchGraph_Click(object sender, EventArgs e)
        {
            showDigitized = !showDigitized;
            if (showDigitized)
            {
                button_switchGraph.Text = "Show Filtered EMG Signal";
            }
            else
            {
                button_switchGraph.Text = "Show Digitalized EMG Envelopl";
            }
        }

        private void button_start_recording_Click(object sender, EventArgs e)
        {
            if (textBox_subjectName.Text != "")
            {
                
                if (electrode == "IBT")
                {
                    if (!Directory.Exists(savingPath + textBox_subjectName.Text + electrode + textBox_envelopWinLen.Text))
                    {
                        Directory.CreateDirectory(savingPath + textBox_subjectName.Text + electrode + textBox_envelopWinLen.Text);
                    }
                    filePath = savingPath + textBox_subjectName.Text + electrode + textBox_envelopWinLen.Text + "\\" + DateTime.Now.ToString("dd-mm-yyyy_hh-mm-ss") + ".txt";
                }
                else if(electrode == "OttoBock")
                {
                    if (!Directory.Exists(savingPath + textBox_subjectName.Text + electrode ))
                    {
                        Directory.CreateDirectory(savingPath + textBox_subjectName.Text + electrode );
                    }
                    filePath = savingPath + textBox_subjectName.Text + electrode + "\\" + DateTime.Now.ToString("dd-mm-yyyy_hh-mm-ss") + ".txt";
                }
                
                myFileStream = new FileStream(filePath, System.IO.FileMode.Create);
                myStreamWriter = new StreamWriter(myFileStream);
                recording = true;

                button_start_recording.Enabled = false;
                button_stop_recording.Enabled = true;
            }
            else
            {
                MessageBox.Show("Please input the subject's name!");
            }
        }

        private void button_stop_recording_Click(object sender, EventArgs e)
        {
            recording = false;
            myStreamWriter.Close();
            myFileStream.Close();

            button_start_recording.Enabled = true;
            button_stop_recording.Enabled = false;
        }

        private void button_select_file_Click(object sender, EventArgs e)
        {
            OpenFileDialog myOpenFileDialog = new OpenFileDialog();
            myOpenFileDialog.InitialDirectory = savingPath;
            myOpenFileDialog.Filter = "txt Files(*.txt)|*.txt|All Files(*.*)|*.*";
            if (myOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                readingPath = myOpenFileDialog.FileName;
            }
            textBox_ReadDirectory.Text = readingPath;
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        }

        private void button_display_file_Click(object sender, EventArgs e)
        {
            if (textBox_ReadDirectory.Text != "")
            {
                timer_display.Enabled = false;
                readingPath = textBox_ReadDirectory.Text;
                Read_txt();
            }
            else
            {
                MessageBox.Show("Please Select or Input the Read File");
            }
        }

        private void Read_txt()
        {
            StreamReader myStreamReader = new StreamReader(readingPath, Encoding.Default);
            string wholeString;
            string[] wholeStringArray;
            int[] wholeIntArray;
            wholeString = myStreamReader.ReadToEnd();
            wholeStringArray = wholeString.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);
            Array.Resize(ref wholeStringArray, wholeStringArray.Length - 10);//Remove the last 10 elements in array by resizing, which are "Game Finished! You get " + totalHits + " Hits out of " + totalTrials + " Trials!" 
            wholeIntArray = Array.ConvertAll(wholeStringArray, int.Parse);
            myStreamReader.Close();

            int id = 0;
            for(int i=1; i< wholeIntArray.Length; i += 3)
            {
                savedEnvelop[id] = wholeIntArray[i];
                id++;
            }
            id = 0;
            for(int i=2; i<wholeIntArray.Length; i += 3)
            {
                savedDigitizedEnvelop[id] = wholeIntArray[i];
                id++;
            }

            DisplayFileData(id);
        }

        private void DisplayFileData(int dispLength)
        {
            this.chart_EMGrealtime.Series["EMGVal"].Points.Clear();
            for (disp = 0; disp < dispLength; disp++)
            {
                if (!showDigitized)
                {
                    this.chart_EMGrealtime.ChartAreas[0].AxisY.Maximum = Double.NaN; //Default AutoScale
                    this.chart_EMGrealtime.ChartAreas[0].AxisY.Minimum = Double.NaN;
                    this.chart_EMGrealtime.ChartAreas[0].AxisY.Interval = Double.NaN;
                    this.chart_EMGrealtime.Titles["EMG_Envelop"].Text = "Filtered EMG Signal From File";
                    this.chart_EMGrealtime.Series["EMGVal"].Points.AddXY((disp / 1000).ToString(), savedEnvelop[disp]);
                }
                else
                {
                    this.chart_EMGrealtime.ChartAreas[0].AxisY.Maximum = 80;
                    this.chart_EMGrealtime.ChartAreas[0].AxisY.Minimum = 0;
                    this.chart_EMGrealtime.ChartAreas[0].AxisY.Interval = 10;
                    this.chart_EMGrealtime.Titles["EMG_Envelop"].Text = "Digitized EMG Signal (0-77) From File";
                    this.chart_EMGrealtime.Series["EMGVal"].Points.AddXY((disp / 1000).ToString(), savedDigitizedEnvelop[disp]);
                }
            }
        }

        private void button_return_realtime_Click(object sender, EventArgs e)
        {
            timer_display.Enabled = true;
        }

        private void button_IBTVD_Click(object sender, EventArgs e)
        {
            electrode = "IBT";
            button_IBTVD.Enabled = false;
            button_OB.Enabled = true;
        }

        private void button_OB_Click(object sender, EventArgs e)
        {
            electrode = "OttoBock";
            button_OB.Enabled = false;
            button_IBTVD.Enabled = true;
        }

        private void button_pause_Click(object sender, EventArgs e)
        {
            timer_display.Enabled = false;
            button_pause.Enabled = false;
            button_startDisplay.Enabled = true;
        }

        private void timer_training_Tick(object sender, EventArgs e)
        {
            trainingElapsed++;
            if(trainingElapsed == trainingTime)
            {
                if(electrode == "IBT")
                {
                    signalPeakD2 = envelop.Max();
                }
                else if(electrode == "OttoBock")
                {
                    signalPeakOB = envelop.Max();
                    stepSizeOB = signalPeakOB / 100.0;
                }
                button_training.BackColor = Color.Lime;
                button_training.Text = "Your Trained Max Strength is " + envelop.Max().ToString();
            }
        }

        private void button_training_Click(object sender, EventArgs e)
        {
            button_start_Click(sender,e);
            timer_training.Enabled = true;
            button_training.BackColor = Color.Cyan;
        }
    }
}
