using System;

namespace WalkingSimulator.UI.Leaderboard
{
    [Serializable]
    public record Record
    {
        public string Name { get; }


        public Record(string name)
        {
            Name = name;
        }
    }
}