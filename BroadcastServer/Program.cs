using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BroadcastServer
{
    internal class Program
    {
        const int BROADCAST_PORT = 12345;
        const int SERVER_PORT = 54321;

        static void Main(string[] args)
        {
            try
            {
                // UdpClient nesnesi oluşturuluyor
                UdpClient udpClient = new UdpClient(BROADCAST_PORT);

                // Socket ayarları yapılandırılıyor
                udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                udpClient.EnableBroadcast = true;

                // Broadcast mesajı bekleniyor
                Console.WriteLine("Broadcast mesajı bekleniyor...");
                IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, BROADCAST_PORT);
                byte[] receivedBytes = udpClient.Receive(ref remoteEndPoint);

                // Broadcast mesajı alındı, yanıt mesajı gönderiliyor
                string message = Encoding.ASCII.GetString(receivedBytes);
                Console.WriteLine($"Broadcast mesajı alındı: {remoteEndPoint}:{BROADCAST_PORT}");

                string responseMessage = GetLocalIPAddress();
                byte[] responseBytes = Encoding.ASCII.GetBytes(responseMessage);
                udpClient.Send(responseBytes, responseBytes.Length, remoteEndPoint);
                Console.WriteLine($"Yanıt mesajı gönderildi: {responseMessage}");

                // TcpListener nesnesi oluşturuluyor
                TcpListener tcpListener = new TcpListener(IPAddress.Any, SERVER_PORT);
                tcpListener.Start();
                Console.WriteLine($"TCP/IP server dinleniyor :{GetLocalIPAddress()}:{SERVER_PORT} ...");

                // Client bağlantısı bekleniyor
                TcpClient tcpClient = tcpListener.AcceptTcpClient();
                Console.WriteLine($"Client bağlandı ({((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address}:{((IPEndPoint)tcpClient.Client.RemoteEndPoint).Port})");

                // TcpClient nesnesi kapatılıyor
                tcpClient.Close();

                // TcpListener nesnesi kapatılıyor
                tcpListener.Stop();

                // UdpClient nesnesi kapatılıyor
                udpClient.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata oluştu: {ex.Message}");
            }
        }

        static string GetLocalIPAddress()
        {
            // Bilgisayarın IP adresi alınıyor
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "IP adresi bulunamadı!";
        }
    }
    
}