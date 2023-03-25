using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SFA
{
    public class TimerJobs : IHostedService, IDisposable
    {
        private Timer timer;
        private readonly ILogger<TimerJobs> _logger;
        private int executionCount = 0;
        public TimerJobs(ILogger<TimerJobs> logger)
        {
            _logger = logger;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            timer = new Timer(DoWork,null,TimeSpan.Zero, TimeSpan.FromSeconds(10));
            Console.WriteLine("Timer was called");
            return Task.CompletedTask;
            throw new System.NotImplementedException();
        }
        private void DoWork(object state)
        {
            var count = Interlocked.Increment(ref executionCount);

            _logger.LogInformation(
                "Timed Hosted Service is working. Count: {Count}", count);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");
            Console.WriteLine("Task was stopped");
            timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
            throw new System.NotImplementedException();
        }
        public void Dispose()
        {
            timer?.Dispose();
        }
            
    }
}
