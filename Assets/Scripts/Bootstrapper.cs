using MainMenu_;
using ResourceLoaders;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private MainMenu _mainMenu;
    
    private readonly ClickerResourceLoader _clickerResourceLoader = new();
    private readonly WalkingSimulatiorResourceLoader _simulatiorResourceLoader = new();
    
    private void Awake()
    {
        DontDestroyOnLoad(this);
        
        _mainMenu.Init(_clickerResourceLoader, _simulatiorResourceLoader);
        
    }
}