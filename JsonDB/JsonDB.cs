using System.Text.Json;

namespace JsonDB;

public class JsonDB<T> 
{
    private string _path;
    public List<T> Context { get; set; }

    public JsonDB(string dbPath)
    {
        _path = dbPath;
        Context = GetContext();
    }

    public void SaveChanges()
    {
        using (var stream = new StreamWriter(_path))
        {
            var json = JsonSerializer.Serialize(Context);
            stream.Write(json);
        }
    }

    private List<T> GetContext()
    {
        try
        {
            return JsonSerializer.Deserialize<List<T>>(File.ReadAllText(_path)) ?? new List<T>();
        }
        catch (Exception e)
        {
            return new List<T>();
        }
    }
}
