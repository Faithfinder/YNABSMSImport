using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
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
            var serialized = JsonConvert.SerializeObject(setting);
            using (var writer = File.CreateText(filePath))
            {
                var writingTask = writer.WriteAsync(serialized);
            }
        }

        private static string SettingsFolder => GetFolderPath(SpecialFolder.LocalApplicationData) + "/UserSettings";

        private static async Task<IEnumerable<UserSetting>> QueryForSettingsAsync(string sender, DirectoryInfo directory)
        {
            return await Task.Run(() =>

            from file in directory.GetFiles()
            let deserialized = JsonConvert.DeserializeObject<UserSetting>(file.FullName)
            where deserialized.Sender == sender
            select deserialized
            );
        }
    }
}