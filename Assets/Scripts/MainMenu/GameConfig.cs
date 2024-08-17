using Infrastructure.SceneLoader;
using UnityEngine;

namespace MainMenu
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/Game", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public string StorageUrl { get; private set; } = "gs://minigames-a639c.appspot.com";
        [field: SerializeField] public string BundleName { get; private set; } = "walkingsimulator";
        [field: SerializeField] public string ProgressSavePath { get; private set; } = "leaderboard.json";
        [field: SerializeField] public string SceneName { get; private set; } = SceneNames.Game1;

        public string SavePathWithoutExtension
        {
            get
            {
                int index = ProgressSavePath.LastIndexOf('.');
                return ProgressSavePath.Remove(index, ProgressSavePath.Length - index);
            }
        }
    }
}