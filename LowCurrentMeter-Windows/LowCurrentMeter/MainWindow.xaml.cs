using System;
using System.IO.Ports;
using System.Text;
using System.Windows;
using System.Collections.Generic;

namespace LowCurrentMeter
{
    public partial class MainWindow : Window
    {
        // PUBLIC

        public MainWindow()
        {
            InitializeComponent();

            mGraphCurrent.SetTitle("Current");
            mGraphCurrent.SetUnit("mA");
            mGraphCurrent.SetMaxPoints(MAX_POINTS + 1);
            mGraphCurrent.SetAxisXRange(0, MAX_POINTS);
            mGraphCurrent.SetAxisYRange(0, 1100);

            InitSerialPort();
        }

        // PRIVATE

        private const int MAX_POINTS = 100;
        private const double MAX_OUTPUT_VOLTAGE = 15;

        private SerialPort mSerialPort = null;

        private void InitSerialPort()
        {
            string[] ports = SerialPort.GetPortNames();


            if (ports.Length > 0)
            {
                Console.WriteLine(ports[0]);

                mSerialPort = new SerialPort();
                mSerialPort.PortName = ports[0];
                mSerialPort.BaudRate = 115200;
                mSerialPort.Handshake = System.IO.Ports.Handshake.None;
                mSerialPort.Parity = Parity.None;
                mSerialPort.DataBits = 8;
                mSerialPort.StopBits = StopBits.Two;
                mSerialPort.ReadTimeout = 200;
                mSerialPort.WriteTimeout = 50;
                mSerialPort.DtrEnable = true;
                mSerialPort.DataReceived += SerialPort_DataReceived;
                mSerialPort.Open();
            }

            //serial.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(Recieve);
        }

        private void SerialPort_DataReceived(object s, SerialDataReceivedEventArgs e)
        {
            SerialPort port = s as SerialPort;
            byte[] data = new byte[port.BytesToRead];
            port.Read(data, 0, data.Length);

            //string[] asciiData = Encoding.Default.GetString(data).Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            IList<double> points = new List<double>(data.Length);
            for(int i = 0; i < data.Length - 1; i++)
            {
                ArraySegment<byte> arraySegment = new ArraySegment<byte>(data, i,2);
                points.Add(BitConverter.ToUInt16(arraySegment.Array, 0));
            }

            mGraphCurrent.AddPoints(points);
        }

    }
}
