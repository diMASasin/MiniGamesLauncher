using TMPro;
using UnityEngine;

namespace Timers
{
    public class TimeView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        
        private Stopwatch _stopwatch;

        public void Init(Stopwatch stopwatch)
        {
            _stopwatch = stopwatch;

            _stopwatch.Updated += OnUpdated;
        }

        private void OnDestroy()
        {
            if (_stopwatch != null) _stopwatch.Updated -= OnUpdated;
        }

        private void OnUpdated(double timeLeft) => ShowTime(timeLeft);

        public void ShowTime(double timeLeft)
        {
            double minutes = timeLeft / 60;
            double seconds = timeLeft % 60;
            _text.text = $"{minutes:F0}:{seconds:00}";
        }
    }
}