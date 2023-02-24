using CliWrap;
using CliWrap.Buffered;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUygulamasiNET6
{
    internal class Commands
    {
    }
    // Komut arayüzü
    public interface ICommand
    {
        Task<string> ExecuteAsync();
    }

    // Merhaba komutu sınıfı
    public class Command : ICommand
    {
        private readonly string _targetFilePath;
        private readonly string _parameter;

        public Command(string targetFilePath, string parameter)
        {
            _parameter = parameter;
            _targetFilePath = targetFilePath;
        }

        public async Task<string> ExecuteAsync()
        {
            try
            {
                var result = await Cli.Wrap(_targetFilePath)
                                    .WithArguments(_parameter)
                                    .ExecuteBufferedAsync();
                switch (result.ExitCode)
                {
                    case 0:
                        Log.Information("Komut başarıyla tamamlandı.");
                        return result.StandardOutput;
                    case 1:
                        Log.Error("Geçersiz argümanlar.");
                        return result.StandardError;
                    case 2:
                        Log.Error("Girdi dosyası açılamadı veya okunamadı.");
                        return result.StandardError;
                    case 3:
                        Log.Error("Çıktı dosyası oluşturulamadı veya yazılamadı.");
                        return result.StandardError;
                    case 4:
                        Log.Warning("İşlem iptal edildi veya kesinti oldu.");
                        return result.StandardError;
                    default:
                        Log.Error("Bilinmeyen exit kodu: {ExitCode}", result.ExitCode);
                        return result.StandardError;
                }

            }
            catch (Exception ex)
            {
                Log.Error("Exception: {Message}", ex.Message);
                return ex.Message;
            }

        }
    }
}
