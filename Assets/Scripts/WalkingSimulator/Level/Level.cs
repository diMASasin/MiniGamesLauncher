using System;
using UnityEngine;
using WalkingSimulator.Timers;
using WalkingSimulator.UI;
using WalkingSimulator.UI.Leaderboard;

namespace WalkingSimulator.Level
{
    public class Level : IDisposable
    {
        private readonly Finish _finish;
        private readonly WinCanvas _winCanvas;
        private readonly Stopwatch _stopwatch = new();
        private readonly Canvas _gameplayCanvas;
        private readonly Leaderboard _leaderboard;
        private readonly string _playerName;

        public Level(Finish finish, WinCanvas winCanvas, Canvas gameplayCanvas, TimeView timeView,
            Leaderboard leaderboard, string playerName)
        {
            _leaderboard = leaderboard;
            _playerName = playerName;
            _gameplayCanvas = gameplayCanvas;
            _winCanvas = winCanvas;
            _finish = finish;
            
            timeView.Init(_stopwatch, new TimeFormatter());
            
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
            double passedTime = _stopwatch.TimePassed;
            
            _stopwatch.Stop();
            _gameplayCanvas.gameObject.SetActive(false);
            _leaderboard.Add(_playerName, _stopwatch.TimePassed);
            _winCanvas.Show(passedTime, _leaderboard);
        }
    }
}