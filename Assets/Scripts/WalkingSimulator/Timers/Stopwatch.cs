using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace WalkingSimulator.Timers
{
    public class Stopwatch
    {
        private CancellationTokenSource _tokenSource;
        
        public double TimePassed { get; private set; } = 0;
        
        public event Action<double> Updated;
        
        public void Start()
        {
            Reset();

            _tokenSource = new CancellationTokenSource();
            StartStopwatch().Forget();
            
            Updated?.Invoke(TimePassed);
        }

        public void Stop() => _tokenSource.Cancel();

        private async UniTaskVoid StartStopwatch()
        {
            await UniTask.WaitWhile(() =>
            {
                TimePassed += Time.deltaTime;
                Updated?.Invoke(TimePassed);
                
                return true;
            }, cancellationToken: _tokenSource.Token);
        }

        private void Reset()
        {
            TimePassed = 0;
        }
    }
}