using System.IO;
using ClickerSystem;
using Newtonsoft.Json;

public class SaveSystem
{
    private readonly string _savePath;

    public SaveSystem(string savePath)
    {
        _savePath = savePath;
    }

    public void Save<T>(T objectToSave)
    {
        File.WriteAllText(_savePath, JsonConvert.SerializeObject(objectToSave));
    }

    public bool TryLoad<T>(string savePath, out T result)
    {
        result = JsonConvert.DeserializeObject<T>(File.ReadAllText(_savePath));
        
        return result != null;
    }
}