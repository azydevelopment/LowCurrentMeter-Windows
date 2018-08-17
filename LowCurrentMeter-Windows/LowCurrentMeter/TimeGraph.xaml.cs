using System;
using System.Windows.Controls;
using LiveCharts.Wpf;
using System.Collections.Generic;
using LiveCharts.Geared;
using System.Threading.Tasks;

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

        public void SetDataMultiplier(double multiplier)
        {
            mDataMultiplier = multiplier;
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
            if (mDataMultiplier != 1)
            {
                for (int i = 0; i < values.Count; i++)
                {
                    values[i] *= mDataMultiplier;
                }
            }

            double currentValue = values[values.Count - 1];

            if (mUiUpdateCount > UI_UPDATE_PERIOD)
            {
                double dataSum = 0;
                int numPoints = Math.Min(DATA_HISTORY_WINDOW, mData.Count);
                for (int i = 0; i < numPoints; i++)
                {
                    dataSum += mData[mData.Count - 1 - i];
                }
                double average = dataSum / numPoints;

                App.Current.Dispatcher.BeginInvoke((Action)delegate
                {
                    mCurrentValue.Text = average.ToString("N2");
                }, null);

                mData.AddRange(mDataBuffer);
                TrimData();

                mDataBuffer.Clear();

                mUiUpdateCount = 0;
            }
            else
            {
                mDataBuffer.AddRange(values);

                mUiUpdateCount++;
            }
        }

        /* PRIVATE */

        private const int DATA_HISTORY_WINDOW = 100;
        private const int UI_UPDATE_PERIOD = 15;

        private long mMaxPoints = 0;
        private int mUiUpdateCount = 0;
        private double mDataMultiplier = 1;
        private List<double> mDataBuffer = new List<double>();

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
