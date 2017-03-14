using System;
using System.Threading;

namespace Services
{
    class TestService
    {
        private volatile bool _stopFlag;
        private Thread _thread;
        private volatile bool _processingError;

        public void Start()
        {
            _thread = new Thread(x =>
            {
                do
                {
                    for (int i = 0; i < 10000; i++)
                    {
                        Console.WriteLine($"TestService: {i}");
                        SrvUtils.Retarder(1, ref _stopFlag);
                    }
                } while (!SrvUtils.Retarder(30, ref _stopFlag));
            });
            _thread.Start();
        }

        public void Stop()
        {
            _stopFlag = true;
        }
    }
}
