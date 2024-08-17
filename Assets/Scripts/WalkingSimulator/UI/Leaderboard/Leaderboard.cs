using System.Collections.Generic;

namespace WalkingSimulator.UI.Leaderboard
{
    public class Leaderboard
    {
        public SortedList<double, string> Leaders { get; set; } = new();

        private int? _currentPlayerIndex;
        
        public void Add(string playerName, double time)
        {
            Leaders.Add(time, playerName);
            _currentPlayerIndex = Leaders.IndexOfValue(playerName);
        }

        public int? GetCurrentPlayerIndex() => _currentPlayerIndex;
    }
}