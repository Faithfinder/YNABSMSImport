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
        public static async Task<UserSetting> FindSettingAsync(string sender)
        {
            var directory = new DirectoryInfo(SettingsFolder);
            IEnumerable<UserSetting> query = await QueryForSettingsAsync(sender, directory);

            return query.FirstOrDefault();
        }

        public static void SaveSetting(UserSetting setting)
        {
            var filePath = Path.Combine(SettingsFolder, $"{setting.Id}.json");
            Directory.CreateDirectory(SettingsFolder);
            using (var writer = File.CreateText(filePath))
            {
                var serialized = JsonConvert.SerializeObject(setting, SerializerSettings);
                var writingTask = writer.WriteAsync(serialized);
            }
        }

        internal static async Task<IEnumerable<UserSetting>> GetAllAsync()
        {
            var directory = new DirectoryInfo(SettingsFolder);
            return await QueryAllSettingsAsync(directory);
        }

        private static JsonSerializerSettings SerializerSettings => new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.Auto
        };

        private static string SettingsFolder => GetFolderPath(SpecialFolder.LocalApplicationData, SpecialFolderOption.Create) + "/UserSettings";

        private static async Task<IEnumerable<UserSetting>> QueryAllSettingsAsync(DirectoryInfo directory)
        {
            var settingStrings = new List<string>();
            foreach (var file in directory.GetFiles())
            {
                settingStrings.Add(await file.OpenText().ReadToEndAsync());
            }
            return from settingString in settingStrings
                   select JsonConvert.DeserializeObject<UserSetting>(settingString, SerializerSettings);
        }

        private static async Task<IEnumerable<UserSetting>> QueryForSettingsAsync(string sender, DirectoryInfo directory)
        {
            return await Task.Run(() =>

            from file in directory.GetFiles()
            let value = file.OpenText().ReadToEnd()
            let deserialized = JsonConvert.DeserializeObject<UserSetting>(value, SerializerSettings)
            where deserialized.Sender == sender
            select deserialized
            );
        }
    }
}