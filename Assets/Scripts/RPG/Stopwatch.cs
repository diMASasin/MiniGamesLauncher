using System;
using System.Collections;
using UnityEngine;

namespace Timers
{
    public class Stopwatch
    {
        private double _interval;
        private Coroutine _coroutine;
        private bool _paused;
        private readonly ICoroutinePerformer _coroutinePerformer;
        
        public double TimePassed { get; private set; } = 0;
        public bool Started { get; private set; }
        
        public event Action<double> Updated;

        public Stopwatch(ICoroutinePerformer coroutinePerformer)
        {
            _coroutinePerformer = coroutinePerformer;
        }
        
        public void Start()
        {
            Stop();
            Reset();
            _coroutine = _coroutinePerformer.StartCoroutine(StartStopwatch());
            Started = true;
            Updated?.Invoke(TimePassed);
        }

        public void Stop()
        {
            if(_coroutine != null)
                _coroutinePerformer.StopCoroutine(_coroutine);
        }

        private IEnumerator StartStopwatch()
        {
            while (true)
            {
                yield return null;
                
                TimePassed += Time.deltaTime;
                Updated?.Invoke(TimePassed);
            }
        }

        private void Reset()
        {
            TimePassed = _interval;
            Started = false;
        }
    }
}