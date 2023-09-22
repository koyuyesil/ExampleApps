using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
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

namespace DeviceListing
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            var usbDevices = GetUSBDevices();
            foreach (var usbDevice in usbDevices)
            {    //Console.WriteLine("Device ID: {0}, PNP Device ID: {1}, Description: {2}", usbDevice.DeviceID, usbDevice.PnpDeviceID, usbDevice.Description);
                //listbox.Items.Add("Device ID: "+usbDevice.DeviceID+", PNP Device ID: "+ usbDevice.PnpDeviceID + ", Description: "+ usbDevice.Description + ", Name:"+usbDevice.Name);
                listBox.Items.Add(string.Format("Device ID: {0}, PNP Device ID: {1}, Description: {2}", usbDevice.DeviceID, usbDevice.PnpDeviceID, usbDevice.Description));
            }
            GetLogicalDisks();
        }

        static List<USBDeviceInfo> GetUSBDevices()
        {
            List<USBDeviceInfo> devices = new List<USBDeviceInfo>();
            // ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity WHERE DeviceID = 'USB\\VID_8087&PID_0024\\5&38CA7A24&0&1'");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"Select * From Win32_USBHub");
            // ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"Select * SELECT * FROM Win32_USBControllerDevice");
            //ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"Select * From Win32_USBHub");
            // ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"Select * From Win32_USBHub");
            foreach (var device in searcher.Get())
            {
                devices.Add(new USBDeviceInfo(
                (string)device.GetPropertyValue("DeviceID"),
                (string)device.GetPropertyValue("PNPDeviceID"),
                (string)device.GetPropertyValue("Description"),
                (string)device.GetPropertyValue("Name")
                ));
            }

            return devices;
        }

        private void GetLogicalDisks()
        {
            try
            {
                // Create a StringBuilder to store the text
                StringBuilder sb = new StringBuilder();
                ManagementObjectSearcher searcher =
                new ManagementObjectSearcher("root\\CIMV2",
                "SELECT * FROM Win32_LogicalDisk");
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    // Append data to the StringBuilder
                    sb.AppendLine("-----------------------------------");
                    sb.AppendLine("Win32_LogicalDisk instance");
                    sb.AppendLine("-----------------------------------");
                    sb.AppendLine("Access: " + queryObj["Access"]);
                    sb.AppendLine("Availability: " + queryObj["Availability"]);
                    sb.AppendLine("BlockSize: " + queryObj["BlockSize"]);
                    sb.AppendLine("Caption: " + queryObj["Caption"]);
                    sb.AppendLine("Compressed: " + queryObj["Compressed"]);
                    sb.AppendLine("Description: " + queryObj["Description"]);
                    sb.AppendLine("DeviceID: " + queryObj["DeviceID"]);
                    sb.AppendLine("DriveType: " + queryObj["DriveType"]);
                    sb.AppendLine("ErrorCleared: " + queryObj["ErrorCleared"]);
                    sb.AppendLine("ErrorDescription: " + queryObj["ErrorDescription"]);
                    sb.AppendLine("ErrorMethodology: " + queryObj["ErrorMethodology"]);
                    sb.AppendLine("FileSystem: " + queryObj["FileSystem"]);
                    sb.AppendLine("FreeSpace: " + queryObj["FreeSpace"]);
                    sb.AppendLine("InstallDate: " + queryObj["InstallDate"]);
                    sb.AppendLine("MediaType: " + queryObj["MediaType"]);
                    sb.AppendLine("Name: " + queryObj["Name"]);
                    sb.AppendLine("NumberOfBlocks: " + queryObj["NumberOfBlocks"]);
                    sb.AppendLine("PNPDeviceID: " + queryObj["PNPDeviceID"]);
                    sb.AppendLine("QuotasDisabled: " + queryObj["QuotasDisabled"]);
                    sb.AppendLine("QuotasIncomplete: " + queryObj["QuotasIncomplete"]);
                    sb.AppendLine("QuotasRebuilding: " + queryObj["QuotasRebuilding"]);
                    sb.AppendLine("Size: " + queryObj["Size"]);
                    sb.AppendLine("Status: " + queryObj["Status"]);
                    sb.AppendLine("StatusInfo: " + queryObj["StatusInfo"]);
                    sb.AppendLine("SystemName: " + queryObj["SystemName"]);
                    sb.AppendLine("VolumeDirty: " + queryObj["VolumeDirty"]);
                    sb.AppendLine("VolumeName: " + queryObj["VolumeName"]);
                    // Append an empty line to separate entries
                    sb.AppendLine();
                }
                // Set the text of the TextBox to the content of the StringBuilder
                tbxDisks.Text = sb.ToString();
            }
            catch (ManagementException e)
            {
                MessageBox.Show("An error occurred while querying for WMI data: " + e.Message);
            }
        }
    }
}
