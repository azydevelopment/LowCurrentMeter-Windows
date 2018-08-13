using System;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.Generic;
using LiveCharts.Geared;
using System.Windows.Media;

namespace LowCurrentMeter
{
    public partial class TimeGraph : UserControl
    {
        /* PUBLIC */

        public GearedValues<double> mData { get; set; }

        public TimeGraph()
        {
            InitializeComponent();

            mData = new GearedValues<double>().WithQuality(Quality.Highest);

            DataContext = this;
        }

        public void SetTitle(String title)
        {
            mTitle.Text = title;
        }

        public void SetUnit(String unit)
        {
            mUnit.Text = unit;
        }

        public void SetMaxPoints(long numPoints)
        {
            mMaxPoints = numPoints;
            TrimData();
        }

        public void SetAxisXRange(double min, double max)
        {
            Axis axis = mGraph.AxisX[0];
            axis.MinValue = min;
            axis.MaxValue = max;
        }

        public void SetAxisYRange(double min, double max)
        {
            Axis axis = mGraph.AxisY[0];
            axis.MinValue = min;
            axis.MaxValue = max;
        }

        public void AddPoints(IList<double> values)
        {
            foreach (double value in values)
            {
                mData.Add(value);
            }
            TrimData();

            //Dispatcher.Invoke(() =>
            //{
            //    mCurrentValue.Text = mData[mData.Count - 1].ToString();
            //}); 
        }

        /* PRIVATE */

        private long mMaxPoints = 0;

        private void TrimData()
        {
            while (mData.Count > mMaxPoints)
            {
                mData.RemoveAt(0);
            }
        }

        // callbacks

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // TODO IMPLEMENT
        }
    }
}
