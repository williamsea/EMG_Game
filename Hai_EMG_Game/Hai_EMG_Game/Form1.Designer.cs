﻿namespace Hai_EMG_Game
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.button_startDisplay = new System.Windows.Forms.Button();
            this.timer_display = new System.Windows.Forms.Timer(this.components);
            this.chart_EMGrealtime = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart_DigitBar = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.trackBar_displayLength = new System.Windows.Forms.TrackBar();
            this.label_trackBar = new System.Windows.Forms.Label();
            this.timer_targetLevel = new System.Windows.Forms.Timer(this.components);
            this.button_StartGame = new System.Windows.Forms.Button();
            this.button_switchGraph = new System.Windows.Forms.Button();
            this.button_start_recording = new System.Windows.Forms.Button();
            this.button_stop_recording = new System.Windows.Forms.Button();
            this.textBox_subjectName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_envelopWinLen = new System.Windows.Forms.TextBox();
            this.button_return_realtime = new System.Windows.Forms.Button();
            this.button_display_file = new System.Windows.Forms.Button();
            this.button_select_file = new System.Windows.Forms.Button();
            this.label28 = new System.Windows.Forms.Label();
            this.textBox_ReadDirectory = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart_EMGrealtime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_DigitBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_displayLength)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialPort
            // 
            this.serialPort.BaudRate = 250000;
            this.serialPort.PortName = "COM2";
            this.serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort_DataReceived);
            // 
            // button_startDisplay
            // 
            this.button_startDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_startDisplay.Location = new System.Drawing.Point(11, 30);
            this.button_startDisplay.Name = "button_startDisplay";
            this.button_startDisplay.Size = new System.Drawing.Size(89, 50);
            this.button_startDisplay.TabIndex = 1;
            this.button_startDisplay.Text = "Start Display";
            this.button_startDisplay.UseVisualStyleBackColor = true;
            this.button_startDisplay.Click += new System.EventHandler(this.button_start_Click);
            // 
            // timer_display
            // 
            this.timer_display.Interval = 1;
            this.timer_display.Tick += new System.EventHandler(this.timer_display_Tick);
            // 
            // chart_EMGrealtime
            // 
            chartArea1.AxisX.Title = "Time (s)";
            chartArea1.AxisY.Title = "Envelop";
            chartArea1.BorderWidth = 2;
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            chartArea1.Name = "ChartArea1";
            this.chart_EMGrealtime.ChartAreas.Add(chartArea1);
            this.chart_EMGrealtime.Location = new System.Drawing.Point(252, 3);
            this.chart_EMGrealtime.Name = "chart_EMGrealtime";
            series1.BorderWidth = 3;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.Name = "EMGVal";
            this.chart_EMGrealtime.Series.Add(series1);
            this.chart_EMGrealtime.Size = new System.Drawing.Size(825, 702);
            this.chart_EMGrealtime.TabIndex = 3;
            this.chart_EMGrealtime.Text = "chart1";
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.Name = "EMG_Envelop";
            title1.Text = "EMG Envelop";
            this.chart_EMGrealtime.Titles.Add(title1);
            // 
            // chart_DigitBar
            // 
            chartArea2.AxisY.Interval = 10D;
            chartArea2.AxisY.Maximum = 80D;
            chartArea2.AxisY.Minimum = 0D;
            chartArea2.BorderWidth = 2;
            chartArea2.CursorX.IsUserSelectionEnabled = true;
            chartArea2.Name = "ChartArea1";
            this.chart_DigitBar.ChartAreas.Add(chartArea2);
            this.chart_DigitBar.Location = new System.Drawing.Point(1083, 3);
            this.chart_DigitBar.Name = "chart_DigitBar";
            series2.BorderWidth = 5;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.RangeColumn;
            series2.Name = "BarEMGVal";
            series2.YValuesPerPoint = 2;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.RangeColumn;
            series3.Name = "targetLevel";
            series3.YValuesPerPoint = 2;
            this.chart_DigitBar.Series.Add(series2);
            this.chart_DigitBar.Series.Add(series3);
            this.chart_DigitBar.Size = new System.Drawing.Size(328, 702);
            this.chart_DigitBar.TabIndex = 5;
            title2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title2.Name = "Real Time Bar";
            title2.Text = "Real Time Bar";
            this.chart_DigitBar.Titles.Add(title2);
            // 
            // trackBar_displayLength
            // 
            this.trackBar_displayLength.Location = new System.Drawing.Point(11, 93);
            this.trackBar_displayLength.Maximum = 10000;
            this.trackBar_displayLength.Minimum = 100;
            this.trackBar_displayLength.Name = "trackBar_displayLength";
            this.trackBar_displayLength.Size = new System.Drawing.Size(193, 45);
            this.trackBar_displayLength.TabIndex = 8;
            this.trackBar_displayLength.Value = 100;
            this.trackBar_displayLength.Scroll += new System.EventHandler(this.trackBar_displayLength_Scroll);
            // 
            // label_trackBar
            // 
            this.label_trackBar.AutoSize = true;
            this.label_trackBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_trackBar.Location = new System.Drawing.Point(8, 136);
            this.label_trackBar.Name = "label_trackBar";
            this.label_trackBar.Size = new System.Drawing.Size(209, 15);
            this.label_trackBar.TabIndex = 9;
            this.label_trackBar.Text = "Select Display Length From 1s to 10s";
            // 
            // timer_targetLevel
            // 
            this.timer_targetLevel.Interval = 1000;
            this.timer_targetLevel.Tick += new System.EventHandler(this.timer_targetLevel_Tick);
            // 
            // button_StartGame
            // 
            this.button_StartGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_StartGame.Location = new System.Drawing.Point(106, 30);
            this.button_StartGame.Name = "button_StartGame";
            this.button_StartGame.Size = new System.Drawing.Size(89, 50);
            this.button_StartGame.TabIndex = 10;
            this.button_StartGame.Text = "Start Game";
            this.button_StartGame.UseVisualStyleBackColor = true;
            this.button_StartGame.Click += new System.EventHandler(this.button_StartGame_Click);
            // 
            // button_switchGraph
            // 
            this.button_switchGraph.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_switchGraph.Location = new System.Drawing.Point(559, 703);
            this.button_switchGraph.Name = "button_switchGraph";
            this.button_switchGraph.Size = new System.Drawing.Size(263, 36);
            this.button_switchGraph.TabIndex = 11;
            this.button_switchGraph.Text = "Show Digitalized EMG Envelop";
            this.button_switchGraph.UseVisualStyleBackColor = true;
            this.button_switchGraph.Click += new System.EventHandler(this.button_switchGraph_Click);
            // 
            // button_start_recording
            // 
            this.button_start_recording.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_start_recording.Location = new System.Drawing.Point(9, 153);
            this.button_start_recording.Name = "button_start_recording";
            this.button_start_recording.Size = new System.Drawing.Size(99, 50);
            this.button_start_recording.TabIndex = 12;
            this.button_start_recording.Text = "Start Recording";
            this.button_start_recording.UseVisualStyleBackColor = true;
            this.button_start_recording.Click += new System.EventHandler(this.button_start_recording_Click);
            // 
            // button_stop_recording
            // 
            this.button_stop_recording.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_stop_recording.Location = new System.Drawing.Point(114, 153);
            this.button_stop_recording.Name = "button_stop_recording";
            this.button_stop_recording.Size = new System.Drawing.Size(99, 50);
            this.button_stop_recording.TabIndex = 13;
            this.button_stop_recording.Text = "Stop Recording";
            this.button_stop_recording.UseVisualStyleBackColor = true;
            this.button_stop_recording.Click += new System.EventHandler(this.button_stop_recording_Click);
            // 
            // textBox_subjectName
            // 
            this.textBox_subjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_subjectName.Location = new System.Drawing.Point(9, 64);
            this.textBox_subjectName.Name = "textBox_subjectName";
            this.textBox_subjectName.Size = new System.Drawing.Size(204, 26);
            this.textBox_subjectName.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 20);
            this.label1.TabIndex = 15;
            this.label1.Text = "Subject:";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Gainsboro;
            this.groupBox1.Controls.Add(this.button_StartGame);
            this.groupBox1.Controls.Add(this.button_startDisplay);
            this.groupBox1.Controls.Add(this.trackBar_displayLength);
            this.groupBox1.Controls.Add(this.label_trackBar);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(243, 167);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Display";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Gainsboro;
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.textBox_envelopWinLen);
            this.groupBox2.Controls.Add(this.button_stop_recording);
            this.groupBox2.Controls.Add(this.button_start_recording);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.textBox_subjectName);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(7, 206);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(239, 211);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Record";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(184, 20);
            this.label2.TabIndex = 17;
            this.label2.Text = "Envelop Window Length:";
            // 
            // textBox_envelopWinLen
            // 
            this.textBox_envelopWinLen.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_envelopWinLen.Location = new System.Drawing.Point(11, 121);
            this.textBox_envelopWinLen.Name = "textBox_envelopWinLen";
            this.textBox_envelopWinLen.Size = new System.Drawing.Size(50, 26);
            this.textBox_envelopWinLen.TabIndex = 16;
            this.textBox_envelopWinLen.Text = "300";
            // 
            // button_return_realtime
            // 
            this.button_return_realtime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_return_realtime.Location = new System.Drawing.Point(49, 135);
            this.button_return_realtime.Name = "button_return_realtime";
            this.button_return_realtime.Size = new System.Drawing.Size(168, 33);
            this.button_return_realtime.TabIndex = 52;
            this.button_return_realtime.Text = "Return Real Time";
            this.button_return_realtime.UseVisualStyleBackColor = true;
            this.button_return_realtime.Click += new System.EventHandler(this.button_return_realtime_Click);
            // 
            // button_display_file
            // 
            this.button_display_file.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_display_file.Location = new System.Drawing.Point(142, 96);
            this.button_display_file.Name = "button_display_file";
            this.button_display_file.Size = new System.Drawing.Size(75, 33);
            this.button_display_file.TabIndex = 51;
            this.button_display_file.Text = "Display";
            this.button_display_file.UseVisualStyleBackColor = true;
            this.button_display_file.Click += new System.EventHandler(this.button_display_file_Click);
            // 
            // button_select_file
            // 
            this.button_select_file.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_select_file.Location = new System.Drawing.Point(150, 36);
            this.button_select_file.Name = "button_select_file";
            this.button_select_file.Size = new System.Drawing.Size(67, 33);
            this.button_select_file.TabIndex = 50;
            this.button_select_file.Text = "Select File";
            this.button_select_file.UseVisualStyleBackColor = true;
            this.button_select_file.Click += new System.EventHandler(this.button_select_file_Click);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(15, 42);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(80, 20);
            this.label28.TabIndex = 48;
            this.label28.Text = "Directory: ";
            // 
            // textBox_ReadDirectory
            // 
            this.textBox_ReadDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_ReadDirectory.Location = new System.Drawing.Point(20, 70);
            this.textBox_ReadDirectory.Name = "textBox_ReadDirectory";
            this.textBox_ReadDirectory.Size = new System.Drawing.Size(197, 20);
            this.textBox_ReadDirectory.TabIndex = 49;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Gainsboro;
            this.groupBox3.Controls.Add(this.textBox_ReadDirectory);
            this.groupBox3.Controls.Add(this.button_return_realtime);
            this.groupBox3.Controls.Add(this.label28);
            this.groupBox3.Controls.Add(this.button_display_file);
            this.groupBox3.Controls.Add(this.button_select_file);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(12, 435);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(239, 182);
            this.groupBox3.TabIndex = 53;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Read Existing";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(63, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 20);
            this.label3.TabIndex = 18;
            this.label3.Text = "ms";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1474, 740);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button_switchGraph);
            this.Controls.Add(this.chart_DigitBar);
            this.Controls.Add(this.chart_EMGrealtime);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart_EMGrealtime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_DigitBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_displayLength)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.Button button_startDisplay;
        private System.Windows.Forms.Timer timer_display;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_EMGrealtime;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_DigitBar;
        private System.Windows.Forms.TrackBar trackBar_displayLength;
        private System.Windows.Forms.Label label_trackBar;
        private System.Windows.Forms.Timer timer_targetLevel;
        private System.Windows.Forms.Button button_StartGame;
        private System.Windows.Forms.Button button_switchGraph;
        private System.Windows.Forms.Button button_start_recording;
        private System.Windows.Forms.Button button_stop_recording;
        private System.Windows.Forms.TextBox textBox_subjectName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button_return_realtime;
        private System.Windows.Forms.Button button_display_file;
        private System.Windows.Forms.Button button_select_file;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox textBox_ReadDirectory;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_envelopWinLen;
        private System.Windows.Forms.Label label3;
    }
}

