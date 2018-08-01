using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static System.Environment;

namespace YNABSMSImport.ImportSettings
{
    internal static class SettingsManager
    {
        static SettingsManager()
        {
            SerializerSettings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto };
            SettingsFolderPath = GetFolderPath(SpecialFolder.LocalApplicationData, SpecialFolderOption.Create) + "/UserSettings";
            SettingsFolder = new DirectoryInfo(SettingsFolderPath);
        }

        public static async Task<UserSetting> FindSettingAsync(string sender)
        {
            var query = await QueryForSettingsAsync(sender);

            return query.FirstOrDefault();
        }

        public static void SaveSetting(UserSetting setting)
        {
            var filePath = Path.Combine(SettingsFolderPath, $"{setting.Id}.json");
            Directory.CreateDirectory(SettingsFolderPath);
            using (var writer = File.CreateText(filePath))
            {
                var serialized = JsonConvert.SerializeObject(setting, SerializerSettings);
                var writingTask = writer.WriteAsync(serialized);
            }
        }

        internal static async Task<IEnumerable<UserSetting>> GetAllAsync()
        {
            return await QueryAllSettingsAsync();
        }

        private static readonly JsonSerializerSettings SerializerSettings;
        private static readonly DirectoryInfo SettingsFolder;
        private static readonly string SettingsFolderPath;

        private static async Task<IEnumerable<UserSetting>> QueryAllSettingsAsync()
        {
            var tasks = from file in SettingsFolder.GetFiles()
                        select file.OpenText().ReadToEndAsync();

            var result = from json in await Task.WhenAll(tasks)
                         select JsonConvert.DeserializeObject<UserSetting>(json, SerializerSettings);

            return result;
        }

        private static Task<IEnumerable<UserSetting>> QueryForSettings(string sender)
        {
            return Task.Run(() =>

            from file in SettingsFolder.GetFiles()
            let value = file.OpenText().ReadToEnd()
            let deserialized = JsonConvert.DeserializeObject<UserSetting>(value, SerializerSettings)
            where deserialized.Sender == sender
            select deserialized
            );
        }
    }
}