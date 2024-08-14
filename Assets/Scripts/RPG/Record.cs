using System;

namespace RPG
{
    [Serializable]
    public record Record
    {
        public string Name { get; }
        public double TimeInSeconds { get; }

        [field: NonSerialized] public bool IsCurrentPlayer { get; }

        public Record(string name, double timeInSeconds, bool isCurrentPlayer = false)
        {
            Name = name;
            TimeInSeconds = timeInSeconds;
            IsCurrentPlayer = isCurrentPlayer;
        }
    }
}