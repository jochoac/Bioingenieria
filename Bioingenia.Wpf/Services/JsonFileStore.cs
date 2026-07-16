using System.Text.Json;

namespace Bioingenieria.Services;

public static class JsonFileStore
{
    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true
    };

    public static T? Read<T>(string filePath) where T : class
    {
        if (!File.Exists(filePath))
        {
            return null;
        }

        return WithRetries(() =>
        {
            using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            return JsonSerializer.Deserialize<T>(stream, Options);
        });
    }

    public static void Write<T>(string filePath, T data) where T : class
    {
        var directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }

        WithRetries<object?>(() =>
        {
            using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            JsonSerializer.Serialize(stream, data, Options);
            return null;
        });
    }

    private static T WithRetries<T>(Func<T> action, int attempts = 3, int delayMs = 150)
    {
        for (var i = 0; i < attempts; i++)
        {
            try
            {
                return action();
            }
            catch (IOException) when (i < attempts - 1)
            {
                Thread.Sleep(delayMs);
            }
        }

        return action();
    }
}
