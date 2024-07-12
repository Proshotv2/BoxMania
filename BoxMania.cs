// Copyright QUANTOWER LLC. © 2017-2021. All rights reserved.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using TradingPlatform.BusinessLayer;
using TradingPlatform.BusinessLayer.Chart;
using TradingPlatform.BusinessLayer.Utils;

namespace BoxMania
{
    public class BoxMania : Indicator
    {

        //Globals

        [InputParameter("Box 1: Enabled", 10)] public bool Box1Enabled = true;
        [InputParameter("Box 1", 11, 1, 25000, 1, 0)] public string box1 = "0,0";
        [InputParameter("Zone Color", 12)] public Color zoneColor = Color.FromArgb(100, 0, 155, 0);

        [InputParameter("Box 2: Enabled", 20)] public bool Box2Enabled = true;
        [InputParameter("Box 2", 21, 1, 25000, 1, 0)] public string box2 = "0,0";
        [InputParameter("Zone Color 2", 22)] public Color zoneColor2 = Color.FromArgb(100, 0, 155, 0);

        [InputParameter("Box 3: Enabled", 30)] public bool Box3Enabled = true;
        [InputParameter("Box 3", 31, 1, 25000, 1, 0)] public string box3 = "0,0";
        [InputParameter("Zone Color 3", 32)] public Color zoneColor3 = Color.FromArgb(100, 0, 155, 0);

        [InputParameter("Box 4: Enabled", 40)] public bool Box4Enabled = true;
        [InputParameter("Box 4", 41, 1, 25000, 1, 0)] public string box4 = "0,0";
        [InputParameter("Zone Color 4", 42)] public Color zoneColor4 = Color.FromArgb(100, 0, 155, 0);

        [InputParameter("Box 5: Enabled", 50)] public bool Box5Enabled = true;
        [InputParameter("Box 5", 51, 1, 25000, 1, 0)] public string box5 = "0,0";
        [InputParameter("Zone Color 5", 52)] public Color zoneColor5 = Color.FromArgb(100, 0, 155, 0);

        [InputParameter("Box 6: Enabled", 60)] public bool Box6Enabled = true;
        [InputParameter("Box 6", 61, 1, 25000, 1, 0)] public string box6 = "0,0";
        [InputParameter("Zone Color 6", 62)] public Color zoneColor6 = Color.FromArgb(100, 0, 155, 0);

        [InputParameter("Box 7: Enabled", 70)] public bool Box7Enabled = true;
        [InputParameter("Box 7", 71, 1, 25000, 1, 0)] public string box7 = "0,0";
        [InputParameter("Zone Color 7", 72)] public Color zoneColor7 = Color.FromArgb(100, 0, 155, 0);

        [InputParameter("Box 8: Enabled", 80)] public bool Box8Enabled = true;
        [InputParameter("Box 8", 81, 1, 25000, 1, 0)] public string box8 = "0,0";
        [InputParameter("Zone Color 8", 82)] public Color zoneColor8 = Color.FromArgb(100, 0, 155, 0);

        [InputParameter("Box 9: Enabled", 90)] public bool Box9Enabled = true;
        [InputParameter("Box 9", 91, 1, 25000, 1, 0)] public string box9 = "0,0";
        [InputParameter("Zone Color 9", 92)] public Color zoneColor9 = Color.FromArgb(100, 0, 155, 0);

        [InputParameter("Box 10: Enabled", 100)] public bool Box10Enabled = true;
        [InputParameter("Box 10", 101, 1, 25000, 1, 0)] public string box10 = "0,0";
        [InputParameter("Zone Color 10", 102)] public Color zoneColor10 = Color.FromArgb(100, 0, 155, 0);

        [InputParameter("Box 11: Enabled", 110)] public bool Box11Enabled = true;
        [InputParameter("Box 11", 111, 1, 25000, 1, 0)] public string box11 = "0,0";
        [InputParameter("Zone Color 11", 112)] public Color zoneColor11 = Color.FromArgb(100, 0, 155, 0);

        [InputParameter("Box 12: Enabled", 120)] public bool Box12Enabled = true;
        [InputParameter("Box 12", 121, 1, 25000, 1, 0)] public string box12 = "0,0";
        [InputParameter("Zone Color 12", 122)] public Color zoneColor12 = Color.FromArgb(100, 0, 155, 0);

        [InputParameter("Box 13: Enabled", 130)] public bool Box13Enabled = true;
        [InputParameter("Box 13", 131, 1, 25000, 1, 0)] public string box13 = "0,0";
        [InputParameter("Zone Color 13", 132)] public Color zoneColor13 = Color.FromArgb(100, 0, 155, 0);

        [InputParameter("Box 14: Enabled", 140)] public bool Box14Enabled = true;
        [InputParameter("Box 14", 141, 1, 25000, 1, 0)] public string box14 = "0,0";
        [InputParameter("Zone Color 14", 142)] public Color zoneColor14 = Color.FromArgb(100, 0, 155, 0);

        [InputParameter("Box 15: Enabled", 150)] public bool Box15Enabled = true;
        [InputParameter("Box 15", 151, 1, 25000, 1, 0)] public string box15 = "0,0";
        [InputParameter("Zone Color 15", 152)] public Color zoneColor15 = Color.FromArgb(100, 0, 155, 0);

        public class boxMaster
        {
            public int HighNum;
            public int LowNum;
            public DateTime datetime;
            public DateTime datetimeEnd;
            public Color color;

            public boxMaster( int highNum, int lowNum, DateTime dt, DateTime end, Color c)
            {
                HighNum = highNum;
                LowNum = lowNum;
                datetime = dt;
                datetimeEnd = end;
                color = c;
            }

        }

        public BoxMania()
            : base()
        {
            // Defines indicator's name and description.
            Name = "BoxMania";
            Description = "Select two numbers and make a box!";

            // Defines line on demand with particular parameters.
            //AddLineSeries("line1", Color.CadetBlue, 1, LineStyle.Solid);

            // By default indicator will be applied on main window of the chart
            SeparateWindow = false;
            UpdateType = IndicatorUpdateType.OnTick;
        }

        private List<boxMaster> boxes = new List<boxMaster>();

        public override string ShortName => $"Box Mania";

        protected override void OnInit()
        {
            boxes = new List<boxMaster>();

        }

        static List<int> stringToIntConverter(string splitString)
        {
            string[] value = splitString.Split(',');
            int firstNum = Int32.Parse(value[0]);
            int secondNum = Int32.Parse(value[1]);

            List<int> numArray = new List<int>();
            numArray.Add(firstNum);
            numArray.Add(secondNum);

            return numArray;
        }

        private static DateTime ConvertUtcToEasternStandard(DateTime dateTime)
        {
            var easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, easternZone);
        }

        public void checkBoxes()
        {
            //get current timezone and set globex open for Eastern
            var easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime currentTime = DateTime.Now;
            DateTime startOfSession;
            DateTime endSession;

            //if current time is between 1500 and 0000 - 1 then DateTime startOfSession = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 18, 01, 0);
            //else DateTime startOfSession = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day - 1, 18, 01, 0);
            if (currentTime.TimeOfDay.Hours >= 18 && currentTime.TimeOfDay.Hours <= 23)
            {
                startOfSession = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 18, 01, 0);
                endSession = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day + 1, 16, 00, 0);
            }
            else
            {
                startOfSession = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day - 1, 18, 01, 0);
                endSession = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 16, 00, 0);
            }
            
            

            //Convert the Time to UTC and supply it the Eastern Timezone to get the time in Eastern
            DateTime today = TimeZoneInfo.ConvertTimeToUtc(startOfSession, easternZone);
            DateTime endOfSession = TimeZoneInfo.ConvertTimeToUtc(endSession, easternZone);
            //Core.Loggers.Log("Current Time = " + today);

            if (Box1Enabled)
            {
                List<int> newNum1 = stringToIntConverter(box1);
                boxMaster newbox = new boxMaster(newNum1[0], newNum1[1], today, endOfSession, zoneColor);
                boxes.Add(newbox);
            }

            if (Box2Enabled)
            {
                List<int> newNum2 = stringToIntConverter(box2);
                boxMaster newbox2 = new boxMaster(newNum2[0], newNum2[1], today, endOfSession, zoneColor2);
                boxes.Add(newbox2);
            }

            if (Box3Enabled)
            {
                List<int> newNum3 = stringToIntConverter(box3);
                boxMaster newbox3 = new boxMaster(newNum3[0], newNum3[1], today, endOfSession, zoneColor3);
                boxes.Add(newbox3);
            }

            if (Box4Enabled)
            {
                List<int> newNum4 = stringToIntConverter(box4);
                boxMaster newbox4 = new boxMaster(newNum4[0], newNum4[1], today, endOfSession, zoneColor4);
                boxes.Add(newbox4);
            }

            if (Box5Enabled)
            {
                List<int> newNum5 = stringToIntConverter(box5);
                boxMaster newbox5 = new boxMaster(newNum5[0], newNum5[1], today, endOfSession, zoneColor5);
                boxes.Add(newbox5);
            }

            if (Box6Enabled)
            {
                List<int> newNum6 = stringToIntConverter(box6);
                boxMaster newbox6 = new boxMaster(newNum6[0], newNum6[1], today, endOfSession, zoneColor6);
                boxes.Add(newbox6);
            }

            if (Box7Enabled)
            {
                List<int> newNum7 = stringToIntConverter(box7);
                boxMaster newbox7 = new boxMaster(newNum7[0], newNum7[1], today, endOfSession, zoneColor7);
                boxes.Add(newbox7);
            }

            if (Box8Enabled)
            {
                List<int> newNum8 = stringToIntConverter(box8);
                boxMaster newbox8 = new boxMaster(newNum8[0], newNum8[1], today, endOfSession, zoneColor8);
                boxes.Add(newbox8);
            }

            if (Box9Enabled)
            {
                List<int> newNum9 = stringToIntConverter(box9);
                boxMaster newbox9 = new boxMaster(newNum9[0], newNum9[1], today, endOfSession, zoneColor9);
                boxes.Add(newbox9);
            }

            if (Box10Enabled)
            {
                List<int> newNum10 = stringToIntConverter(box10);
                boxMaster newbox10 = new boxMaster(newNum10[0], newNum10[1], today, endOfSession, zoneColor10);
                boxes.Add(newbox10);
            }


        }

        protected override void OnUpdate(UpdateArgs args)
        {
            
            if ( boxes.Count == 0)
            {
                checkBoxes();
            }
            
        }

        public override void OnPaintChart(PaintChartEventArgs args)
        {
            base.OnPaintChart(args);

            if (CurrentChart == null)
                return;

            Graphics graphics = args.Graphics;
            IChartWindow mainWindow = CurrentChart.MainWindow;



            boxes.FindAll(item => true)
                .ForEach(item =>
                {
                    int leftIndex = (int)mainWindow.CoordinatesConverter.GetBarIndex(item.datetime);
                    int rightIndex = (int)mainWindow.CoordinatesConverter.GetBarIndex(item.datetimeEnd);
                    

                    double drawingPrice = Math.Max(item.HighNum, item.LowNum);

                    int startDrawingIndex = leftIndex;
                    int stopDrawingIndex = rightIndex;
                

                    int boxWidth = stopDrawingIndex - startDrawingIndex;
                    int boxXCoord = (int)Math.Round(mainWindow.CoordinatesConverter.GetChartX(item.datetime) - (CurrentChart.BarsWidth / 2));
                    int boxYCoord = (int)Math.Round(mainWindow.CoordinatesConverter.GetChartY(drawingPrice));
                    int boxHeight = (int)Math.Round(Math.Abs(item.HighNum - item.LowNum) * mainWindow.YScaleFactor);
                
                
                    //Core.Loggers.Log("dt = " + item.datetime.ToLocalTime() + ", color = " + item.color + ", highnum = " + item.HighNum + ", lownum = " + item.LowNum);

                    graphics.FillRectangle(new SolidBrush(item.color), boxXCoord, boxYCoord, boxWidth * this.CurrentChart.BarsWidth, boxHeight);
                });

        }


    }
}
