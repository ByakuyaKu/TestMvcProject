using TestMvcProject.Jikan.Interfaces;

namespace TestMvcProject.Jikan
{
    public class JikanService : IHostedService
    {
        private readonly IMangaLib _IMangaLib;
        private readonly IAnimeLib _IAnimeLib;

        public JikanService(IServiceScopeFactory factory)
        {
            _IMangaLib = factory.CreateScope().ServiceProvider.GetRequiredService<IMangaLib>();
            _IAnimeLib = factory.CreateScope().ServiceProvider.GetRequiredService<IAnimeLib>();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Не блокируем поток выполнения: StartAsync должен запустить выполнение фоновой задачи и завершить работу
            GetTopAnimeAndManga(cancellationToken);
            return Task.CompletedTask;
        }
        private async Task GetTopAnimeAndManga(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                   // await _IAnimeLib.GetTopAnimeAsync();
                   // await _IMangaLib.GetTopMangaAsync();

                    

                }
                catch (Exception ex)
                {
                    // обработка ошибки однократного неуспешного выполнения фоновой задачи
                }
                //stoppingToken.ThrowIfCancellationRequested();
                await Task.Delay(TimeSpan.FromDays(10), stoppingToken);
            }
        }


        private async Task GetTopAnime(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await _IAnimeLib.GetTopAnimeAsync();
                    
                }
                catch (Exception ex)
                {
                    // обработка ошибки однократного неуспешного выполнения фоновой задачи
                }
                //stoppingToken.ThrowIfCancellationRequested();
                await Task.Delay(TimeSpan.FromDays(10), stoppingToken);
            }
        }

        private async Task GetTopManga(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await _IMangaLib.GetTopMangaAsync();
                }
                catch (Exception ex)
                {
                    // обработка ошибки однократного неуспешного выполнения фоновой задачи
                }
                //stoppingToken.ThrowIfCancellationRequested();
                await Task.Delay(TimeSpan.FromDays(10), stoppingToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // Если нужно дождаться завершения очистки, но контролировать время, то стоит предусмотреть в контракте использование CancellationToken
            //await someService.DoSomeCleanupAsync(cancellationToken);
            return Task.CompletedTask;
        }
    }
}
