using UnityEngine;
using WalkingSimulator.UI.Leaderboard;

namespace WalkingSimulator.UI
{
    public class WinCanvas : MonoBehaviour
    {
        // [SerializeField] private TimeView _timeView;
        [SerializeField] private LeaderboardView _leaderboardView;
        
        public void Show(double result, Leaderboard.Leaderboard leaderBoard)
        {
            gameObject.SetActive(true);
            // _timeView.ShowTime(result);

            _leaderboardView.Show(leaderBoard);
        }
    }
}