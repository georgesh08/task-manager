using Newtonsoft.Json;

namespace TaskManager.Generators;

/// <summary>
/// This singleton class solution helps us to get unique int values for ID even after program restart.
/// After incrementing current ID value for our entity, we save it in our DTO and export data in file.
/// </summary>

public sealed class IdGenerator
{
    private const string configPath = "IdConfig.txt";
    private static IdGenerator _instance;
    private DataHolder _holder;

    private IdGenerator() { _holder = new DataHolder(); }

    public static IdGenerator GetInstance()
    {
        return _instance ??= new IdGenerator();
    }

    public int GenerateTaskId()
    {
        UpdateData();
        _holder.LastTaskId++;
        LoadData();
        return _holder.LastTaskId;
    }

    public int GenerateGroupId()
    {
        UpdateData();
        _holder.LastGroupId++;
        LoadData();
        return _holder.LastGroupId;
    }

    private void UpdateData()
    {
        _holder = JsonConvert.DeserializeObject<DataHolder>(File.ReadAllText(configPath))!;
    }

    private void LoadData()
    {
        File.WriteAllText(configPath, JsonConvert.SerializeObject(_holder));
    }
}