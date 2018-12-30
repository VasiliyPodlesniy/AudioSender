using System;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO.Ports;

namespace AudioSenser
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AudioSend audioSender;

        public MainWindow()
        {
            InitializeComponent();
            audioSender = new AudioSend();
            FillPortsComboBox();
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((String)ConnectButton.Content == "Connect")
                {
                    if (PortComboBox.SelectedItem == null) return;
                    audioSender.OpenPort(PortComboBox.SelectedItem.ToString(), 9600);
                    ConnectButton.Content = "Disconnect";
                    RefreshPortsButton.IsEnabled = false;
                    PortComboBox.IsEnabled = false;
                }
                else
                {
                    audioSender.Dispose();
                    PortComboBox.IsEnabled = true;
                    RefreshPortsButton.IsEnabled = true;
                    ConnectButton.Content = "Connect";
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Problem with access to COM \n" + "Please, change COM and try again!");
            }
        }

        private void FillPortsComboBox()
        {
            PortComboBox.Items.Clear();
            var names = audioSender.GetAvailablePorts();
            foreach (var name in names)
            {
                var port = new SerialPort(name);
                if (!port.IsOpen) PortComboBox.Items.Add(name);
                port.Dispose();
            }
            PortComboBox.SelectedIndex = 0;
        }

        private void RefreshPortsButton_Click(object sender, RoutedEventArgs e)
        {
            FillPortsComboBox();
        }

        private void setupAudio_Click(object sender, RoutedEventArgs e)
        {
            var openDialog = new OpenFileDialog()
            {
                Filter = "Audio File | *.mp3;.wav",
                FilterIndex = 1
            };
            var showDialog = openDialog.ShowDialog();
        }

        private void setupText_Click(object sender, RoutedEventArgs e)
        {
            var openDialog = new OpenFileDialog()
            {
                Filter = "Text File | *.txt",
                FilterIndex = 1
            };
            var showDialog = openDialog.ShowDialog();
        }
    }
}
