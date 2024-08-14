using MainMenu_;
using ResourceLoaders;
using UnityEngine;

namespace ClickerSystem
{
    public abstract class GameBootstrapper : MonoBehaviour
    {
        public abstract void Init(GameStaticData staticData, GameConfig config);
    }
}