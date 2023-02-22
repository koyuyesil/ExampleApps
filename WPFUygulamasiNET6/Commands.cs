using CliWrap;
using CliWrap.Buffered;
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
        public BufferedCommandResult? _result;

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
                _result = result;
                return result.StandardOutput;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}
