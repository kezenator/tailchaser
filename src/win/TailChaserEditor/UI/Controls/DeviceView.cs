using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace Com.TailChaser.Editor.UI.Controls
{
    public partial class DeviceView : UserControl
    {
        public DeviceView()
        {
            InitializeComponent();

            m_FoundDevice = "";
            m_RecievedData = "";

            PerformDisconnect();
        }

        public void ToggleSignal(Model.SignalMask mask)
        {
            string tx_str = "";

            if (mask == Model.SignalMask.Tail)
            {
                tx_str = "TT";
            }
            else if (mask == Model.SignalMask.Brake)
            {
                tx_str = "TB";
            }
            else if (mask == Model.SignalMask.Reverse)
            {
                tx_str = "TR";
            }
            else if (mask == Model.SignalMask.IndicatorSolid)
            {
                tx_str = "TI";
            }

            if (m_Connected
                && !tx_str.Equals(""))
            {
                try
                {
                    m_SerialPort.Write(tx_str + "\r\n");
                }
                catch (Exception)
                {
                    PerformDisconnect();
                }
            }
        }

        private void m_DiscoveryTimer_Tick(object sender, EventArgs e)
        {
            string new_device = "";
            foreach (string str in SerialPort.GetPortNames())
            {
                new_device = str;
            }

            if (!new_device.Equals(m_FoundDevice))
            {
                PerformDisconnect();

                m_FoundDevice = new_device;

                if (m_FoundDevice.Length != 0)
                {
                    try
                    {
                        listBox1.Items.Add(m_FoundDevice);

                        m_StatusTextBox.Text = "Connecting to " + m_FoundDevice + "...";

                        m_RecievedData = "";

                        m_HeartbeatsSinceResponse = 0;
                        m_HeartbeatTimer.Interval = 2000;
                        m_HeartbeatTimer.Start();

                        m_SerialPort.Parity = Parity.None;
                        m_SerialPort.BaudRate = 9600;
                        m_SerialPort.DataBits = 8;
                        m_SerialPort.StopBits = StopBits.One;
                        m_SerialPort.Handshake = Handshake.None;
                        m_SerialPort.PortName = m_FoundDevice;

                        m_SerialPort.DataReceived += HandleSerialRecieve;

                        m_SerialPort.Open();
                    }
                    catch (Exception)
                    {
                        PerformDisconnect();
                    }
                }
            }
        }

        private void PerformDisconnect()
        {
            // Show disconnected

            m_TailButton.Enabled = false;
            m_TailLabel.Text = "";
            m_TailLabel.BackColor = SystemColors.Control;

            m_BrakeButton.Enabled = false;
            m_BrakeLabel.Text = "";
            m_BrakeLabel.BackColor = SystemColors.Control;

            m_ReverseButton.Enabled = false;
            m_ReverseLabel.Text = "";
            m_ReverseLabel.BackColor = SystemColors.Control;

            m_IndicatorButton.Enabled = false;
            m_IndicatorLabel.Text = "";
            m_IndicatorLabel.BackColor = SystemColors.Control;

            m_StatusTextBox.Text = "Discovering...";

            // Clear down internal state

            m_SerialPort.DataReceived -= HandleSerialRecieve;
            m_Connected = false;
            m_FoundDevice = "";
            m_RecievedData = "";
            listBox1.Items.Clear();
            m_HeartbeatTimer.Stop();

            // Ensure port is closed

            m_SerialPort.Close();
        }

        private void HandleSerialRecieve(object sender, SerialDataReceivedEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                try
                {
                    string recieved = m_SerialPort.ReadExisting();

                    listBox1.Items.Add("Data: " + recieved);

                    m_RecievedData += recieved;

                    ProcessRecievedData();
                }
                catch (Exception)
                {
                    PerformDisconnect();
                }
            }));
        }

        private void ProcessRecievedData()
        {
            string data = m_RecievedData;
            m_RecievedData = "";

            int line_start = 0;
            for (int i = 0; i < data.Length; ++i)
            {
                char ch = data[i];

                if (ch == '\n')
                {
                    ProcessRecievedLine(data.Substring(line_start, i - line_start));
                    line_start = i + 1;
                }
                else if (ch == '\r')
                {
                    if (i == (data.Length - 1))
                    {
                        // Wait until more data arrives to see what
                        // follows this CR
                    }
                    else if (data[i + 1] == '\n')
                    {
                        // Next character is a new line - accept
                        ProcessRecievedLine(data.Substring(line_start, i - line_start));
                        line_start = i + 2;
                        i += 1;
                    }
                    else
                    {
                        // Malformed - ignore
                        line_start = i + 1;
                    }

                }
            }

            // Take remaining data and place
            // it back into the receive buffer

            if (line_start < data.Length)
            {
                m_RecievedData = data.Substring(line_start);
            }
        }

        private void ProcessRecievedLine(string line)
        {
            listBox1.Items.Add("Line: " + line);

            if (line.Substring(0, 1).Equals("S"))
            {
                m_HeartbeatsSinceResponse = 0;
                if (m_Connected == false)
                {
                    m_Connected = true;
                    m_StatusTextBox.Text = "Connected via " + m_FoundDevice;
                    m_TailButton.Enabled = true;
                    m_BrakeButton.Enabled = true;
                    m_ReverseButton.Enabled = true;
                    m_IndicatorButton.Enabled = true;
                }

                if (line.Contains('T'))
                    m_TailLabel.BackColor = Color.DarkRed;
                else
                    m_TailLabel.BackColor = Color.Black;

                if (line.Contains('B'))
                    m_BrakeLabel.BackColor = Color.Red;
                else
                    m_BrakeLabel.BackColor = Color.Black;

                if (line.Contains('R'))
                    m_ReverseLabel.BackColor = Color.White;
                else
                    m_ReverseLabel.BackColor = Color.Black;

                if (line.Contains('I'))
                    m_IndicatorLabel.Text = "Enabled";
                else
                    m_IndicatorLabel.Text = "";

                if (line.Contains('F'))
                {
                    m_IndicatorLabel.ForeColor = Color.Black;
                    m_IndicatorLabel.BackColor = Color.Orange;
                }
                else
                {
                    m_IndicatorLabel.ForeColor = Color.White;
                    m_IndicatorLabel.BackColor = Color.Black;
                }
            }
        }

        private void m_HeartbeatTimer_Tick(object sender, EventArgs e)
        {
            m_HeartbeatTimer.Interval = 1000;

            try
            {
                m_SerialPort.Write("H\r\n");
            }
            catch (Exception)
            {
                PerformDisconnect();
                return;
            }

            m_HeartbeatsSinceResponse += 1;

            if (m_Connected)
            {
                // We're connected - see if there has been no response

                if (m_HeartbeatsSinceResponse > HEARTBEATS_UNTIL_DISCONNECT)
                {
                    PerformDisconnect();
                }
            }
            else
            {
                // We're not connected - if discovery doesn't get us anywhere
                // then close and try again

                if (m_HeartbeatsSinceResponse > HEARTBEATS_UNTIL_DISCOVERY_FAILED)
                {
                    PerformDisconnect();
                }
            }
        }

        private void m_TailButton_Click(object sender, EventArgs e)
        {
            ToggleSignal(Model.SignalMask.Tail);
        }

        private void m_BrakeButton_Click(object sender, EventArgs e)
        {
            ToggleSignal(Model.SignalMask.Brake);
        }

        private void m_ReverseButton_Click(object sender, EventArgs e)
        {
            ToggleSignal(Model.SignalMask.Reverse);
        }

        private void m_IndicatorButton_Click(object sender, EventArgs e)
        {
            ToggleSignal(Model.SignalMask.IndicatorSolid);
        }

        private static int HEARTBEATS_UNTIL_DISCOVERY_FAILED = 5;
        private static int HEARTBEATS_UNTIL_DISCONNECT = 4;

        private string m_FoundDevice;
        private bool m_Connected;
        private int m_HeartbeatsSinceResponse;
        private string m_RecievedData;
    }
}
