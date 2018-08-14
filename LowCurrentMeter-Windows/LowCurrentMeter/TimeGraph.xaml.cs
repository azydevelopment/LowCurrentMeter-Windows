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

        public void AddPoints(List<double> values)
        {
            mData.AddRange(values);
            TrimData();

            double currentValue = values[values.Count - 1];


            if (mUiUpdateCount > UI_UPDATE_PERIOD)
            {
                App.Current.Dispatcher.BeginInvoke((Action)delegate
                {
                    mCurrentValue.Text = currentValue.ToString();
                }, null);
                mUiUpdateCount = 0;
            }
            else
            {
                mUiUpdateCount++;
            }
        }

        /* PRIVATE */

        private const int DATA_BUFFER_SIZE = 100;
        private const int UI_UPDATE_PERIOD = 50;

        private long mMaxPoints = 0;
        public int mUiUpdateCount = 0;

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
