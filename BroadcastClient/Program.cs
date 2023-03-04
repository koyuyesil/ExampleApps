using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BroadcastClient
{
    internal class Program
    {
        const int BROADCAST_PORT = 12345;

        static void Main(string[] args)
        {
            try
            {
                // Broadcast adresi tanımlanıyor
                IPAddress broadcastAddress = IPAddress.Parse("192.168.1.103");

                // Broadcast mesajı oluşturuluyor
                string broadcastMessage = "SimpleTCP/IP serverınız var mı?";
                byte[] broadcastBytes = Encoding.ASCII.GetBytes(broadcastMessage);

                // UdpClient nesnesi oluşturuluyor
                UdpClient udpClient = new UdpClient();

                // Socket ayarları yapılandırılıyor
                udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
                udpClient.EnableBroadcast = true;

                // Broadcast mesajı gönderiliyor
                IPEndPoint endPoint = new IPEndPoint(broadcastAddress, BROADCAST_PORT);
                udpClient.Send(broadcastBytes, broadcastBytes.Length, endPoint);
                Console.WriteLine($"Broadcast mesajı gönderildi: {broadcastMessage}");

                // Serverdan yanıt mesajı alınıyor
                byte[] responseBytes = udpClient.Receive(ref endPoint);
                string response = Encoding.ASCII.GetString(responseBytes);
                Console.WriteLine($"Server IP adresi: {response}");

                // UdpClient kapatılıyor
                udpClient.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata oluştu: {ex.Message}");
            }
        }
    }
}