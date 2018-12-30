using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace AudioSenser
{
    class AudioSend
    {
        SerialPort port;

        public AudioSend()
        {
            port = new SerialPort();
        }

        public void OpenPort(string str, Int32 baudrate)
        {
            port.PortName = str;
            port.BaudRate = baudrate;
            port.Open();
        }

        public string[] GetAvailablePorts()
        {
            return SerialPort.GetPortNames();
        }

        public void Dispose()
        {
            port.Close();
            port.Dispose();
        }
    }
}
