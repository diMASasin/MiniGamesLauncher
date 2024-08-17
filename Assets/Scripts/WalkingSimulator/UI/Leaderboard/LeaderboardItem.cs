using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WalkingSimulator.UI.Leaderboard
{
    public class LeaderboardItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _place;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _time;
        [SerializeField] private Image _background;
    
        public void Init(int place, string playerName, double time, ITimeFormatter timeFormatter)
        {
            _place.text = place.ToString();
            _name.text = playerName;
            _time.text = timeFormatter.Format(time);
        
            RemoveHighlight();
        }

        public void Highlight() => _background.enabled = true;
        public void RemoveHighlight() => _background.enabled = false;
    }
}