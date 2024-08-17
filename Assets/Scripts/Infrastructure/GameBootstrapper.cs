using MainMenu;
using ResourceLoaders;
using UnityEngine;

namespace Infrastructure
{
    public abstract class GameBootstrapper : MonoBehaviour
    {
        public abstract void Init(GameConfig config, ResourceLoader resourceLoader, string nickname);
    }
}