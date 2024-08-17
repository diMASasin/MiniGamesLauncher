using ResourceLoaders;
using UnityEngine;

namespace Infrastructure
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private Game _clickerGame;
        [SerializeField] private Game _walkingSimulator;
    
        private void Awake()
        {
            DontDestroyOnLoad(this);
            
            _clickerGame.Init();
            _walkingSimulator.Init();
        }
    }
}