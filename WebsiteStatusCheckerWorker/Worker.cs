using Microsoft.Extensions.Hosting;
namespace WebsiteStatusCheckerWorker
{

    public class Worker : BackgroundService
    {
        //default
        private readonly ILogger<Worker> _logger;
        //added
        private HttpClient _httpClient;
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _httpClient = new HttpClient();
            _logger.LogInformation("{time}: Nebula Qr Hizmeti Baþlatýldý.", DateTimeOffset.Now);
            return base.StartAsync(cancellationToken);
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogWarning("{time}: Worker service stopping.", DateTimeOffset.Now);
            _logger.LogInformation("{time}: Nebula Qr Hizmeti Sonlandýrýlýyor... ", DateTimeOffset.Now);
            _httpClient.Dispose();
            _logger.LogWarning("{time}: Worker service stopped.", DateTimeOffset.Now);
            _logger.LogInformation("{time}: Nebula Qr Hizmeti Durduruldu.", DateTimeOffset.Now);
            return base.StopAsync(cancellationToken);
        }
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var result = await _httpClient.GetAsync("http://anitr.net");

                if (result.IsSuccessStatusCode)
                {
                    _logger.LogInformation("{time}: Website is Up! Status code: {StatusCode}", DateTimeOffset.Now, result.StatusCode);
                }
                else
                {
                    _logger.LogError("{time}: Website is down! Status code: {StatusCode}", DateTimeOffset.Now, result.StatusCode);
                }
                _logger.LogWarning("{time}: Nebula Qr Hizmeti Veri Topluyor...", DateTimeOffset.Now);
                _logger.LogInformation("{time}: Nebula Qr Hizmeti Veri Topluyor...", DateTimeOffset.Now);
                //default
                _logger.LogInformation("{time}: Worker running...", DateTimeOffset.Now);
                await Task.Delay(5000, cancellationToken);
            }
        }
    }
}