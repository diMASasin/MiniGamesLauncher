using TMPro;
using UnityEngine;

namespace Timers
{
    public class TimeView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        
        private Stopwatch _stopwatch;
        private ITimeFormatter _timeFormatter;

        public void Init(Stopwatch stopwatch, ITimeFormatter timeFormatter)
        {
            _timeFormatter = timeFormatter;
            _stopwatch = stopwatch;

            _stopwatch.Updated += OnUpdated;
        }

        private void OnDestroy()
        {
            if (_stopwatch != null) _stopwatch.Updated -= OnUpdated;
        }

        private void OnUpdated(double timeLeft) => ShowTime(timeLeft);

        public void ShowTime(double timeLeft) => _text.text = _timeFormatter.Format(timeLeft);
    }
}