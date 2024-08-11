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
        private readonly Stopwatch _stopwatch = new();
        private readonly Canvas _gameplayCanvas;

        public Level(Finish finish, WinCanvas winCanvas, Canvas gameplayCanvas, TimeView timeView)
        {
            _gameplayCanvas = gameplayCanvas;
            _winCanvas = winCanvas;
            _finish = finish;
            
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
            _gameplayCanvas.gameObject.SetActive(false);
            _winCanvas.Show(_stopwatch.TimePassed);
        }
    }
}