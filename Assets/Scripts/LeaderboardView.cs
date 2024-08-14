using System.Collections.Generic;
using RPG;
using UnityEngine;

public class LeaderboardView : MonoBehaviour
{
    [SerializeField] private LeaderboardItem _itemPrefab;
    [SerializeField] private Transform _parent;
    [SerializeField] private int _itemsNumber = 100;

    private readonly List<LeaderboardItem> _items = new();

    private void Awake()
    {
        for (int i = 0; i < _itemsNumber; i++)
        {
            var item = Instantiate(_itemPrefab, _parent);
            _items.Add(item);
        }
    }

    public void Show(Leaderboard leaderboard)
    {
        _items.ForEach(item => item.gameObject.SetActive(false));

        int i = 0;
        foreach (var record in leaderboard.Leaders)
        {
            if (i >= _itemsNumber - 1)
                return;

            var item = _items[i];
            item.Init(i + 1, record.Value.Name, record.Key, new TimeFormatter());
            item.gameObject.SetActive(true);

            i++;
        }

        if (leaderboard.CurrentPlayerRecord != null)
            _items[leaderboard.CurrentPlayerRecord.Value].Highlight();
    }
}