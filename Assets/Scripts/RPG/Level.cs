using System;
using RPG.UI;
using Timers;
using UnityEngine;

namespace RPG
{
    public class Level : IDisposable
    {
        private readonly Finish _finish;
        private readonly WinCanvas _winCanvas;
        private readonly Stopwatch _stopwatch;

        public Level(Finish finish, WinCanvas winCanvas, ICoroutinePerformer coroutinePerformer, TimeView timeView)
        {
            _winCanvas = winCanvas;
            _finish = finish;
            
            _stopwatch = new Stopwatch(coroutinePerformer);
            timeView.Init(_stopwatch);
            
            _finish.Finished += OnFinished;
        }
        
        public void Dispose()
        {
            if (_finish != null) _finish.Finished -= OnFinished;
        }

        public void Start()
        {
            _stopwatch.Start();
        }

        private void OnFinished()
        {
            _stopwatch.Stop();
            _winCanvas.Show(_stopwatch.TimePassed);
        }
    }
}