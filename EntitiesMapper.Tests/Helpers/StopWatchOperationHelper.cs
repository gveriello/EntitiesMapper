using System.Diagnostics;

namespace EntitiesMapper.Tests.Helpers
{
    public class StopWatchOperationHelper : IDisposable
    {
        private readonly Stopwatch _stopwatch;
        private readonly string _operationName;

        public StopWatchOperationHelper(string? operationName = null)
        {
            operationName ??= Guid.NewGuid().ToString();
            _operationName = operationName;
            _stopwatch = Stopwatch.StartNew();
        }

        public void Dispose()
        {
            _stopwatch.Stop();
            Console.WriteLine($"{_operationName} completed in {_stopwatch.ElapsedMilliseconds}ms");
        }
    }
}
