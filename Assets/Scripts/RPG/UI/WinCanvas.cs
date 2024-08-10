using Timers;
using UnityEngine;

namespace RPG.UI
{
    public class WinCanvas : MonoBehaviour
    {
        [SerializeField] private TimeView _timeView;
        
        public void Show(double result)
        {
            gameObject.SetActive(true);
            _timeView.ShowTime(result);
        }
    }
}