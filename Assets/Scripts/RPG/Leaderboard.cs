using System;
using System.Collections.Generic;
using System.Linq;

namespace RPG
{
    public class Leaderboard
    {
        public SortedList<double, Record> Leaders { get; set; } = new();

        [field: NonSerialized] public int? CurrentPlayerRecord { get; private set; } = null;

        public void Add(Record record)
        {
            Leaders.Add(record.TimeInSeconds, record);
            CurrentPlayerRecord = Leaders.IndexOfValue(record);
        }
    }
}