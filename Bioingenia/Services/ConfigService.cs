using Bioingenieria.Models;

namespace Bioingenieria.Services;

public static class ConfigService
{
    private static readonly string ConfigFilePath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "Bioingenieria",
        "config.json");

    public static AppConfigModel Load()
    {
        return JsonFileStore.Read<AppConfigModel>(ConfigFilePath) ?? new AppConfigModel();
    }

    public static void Save(AppConfigModel config)
    {
        JsonFileStore.Write(ConfigFilePath, config);
    }

    public static bool IsEquipmentRootValid(string? path)
    {
        return !string.IsNullOrWhiteSpace(path) && Directory.Exists(path);
    }

    public static string UsersFilePath(string equipmentRootPath)
    {
        return Path.Combine(equipmentRootPath, "_system", "users.json");
    }
}
