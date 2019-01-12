using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Threading.Tasks;

using Android.OS;

using Newtonsoft.Json;

using Environment = System.Environment;

namespace YNABSMSImport.ImportSettings
{

    internal class SettingsManager
    {
        private readonly JsonSerializerSettings _serializerSettings;
        private readonly DirectoryInfoBase _settingsFolder;
        private readonly IFileSystem _fileSystem;

        public SettingsManager() : this(new FileSystem()) { }

        public SettingsManager(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
            _serializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
            SettingsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData, Environment.SpecialFolderOption.Create) +
                                 "/UserSettings";
            _settingsFolder = _fileSystem.DirectoryInfo.FromDirectoryName(SettingsFolderPath);
            EnsureSettingsDirExists();
        }

        private void EnsureSettingsDirExists()
        {
            if (!_fileSystem.Directory.Exists(SettingsFolderPath))
                _fileSystem.Directory.CreateDirectory(SettingsFolderPath);
        }

        private string SettingsFolderPath { get; }

        public async Task<UserSetting> FindSettingBySenderAsync(string sender)
        {
            var query = await QueryForSettingsBySender(sender);

            return query.FirstOrDefault();
        }

        public async Task<UserSetting> FindSettingByIdAsync(string id)
        {
            var query = await QueryForSettingByID(id);

            return query.FirstOrDefault();
        }

        public async Task SaveSettingAsync(UserSetting setting)
        {
            var filePath = _fileSystem.Path.Combine(SettingsFolderPath, $"{setting.Id}.json");
            
            using (var writer = _fileSystem.File.CreateText(filePath))
            {
                var serialized = JsonConvert.SerializeObject(setting, _serializerSettings);
                await writer.WriteAsync(serialized);
            }
        }

        public void DeleteSetting(UserSetting setting)
        {
            var filePath = _fileSystem.Path.Combine(SettingsFolderPath, $"{setting.Id}.json");
            if (_fileSystem.File.Exists(filePath))
            {
                _fileSystem.File.Delete(filePath);
            }
        }

        public async Task<IEnumerable<UserSetting>> GetAllAsync()
        {
            return await QueryAllSettingsAsync();
        }

        private async Task<IEnumerable<UserSetting>> QueryAllSettingsAsync()
        {
            var tasks = from file in _settingsFolder.GetFiles()
                        select file.OpenText().ReadToEndAsync();

            var result = from json in await Task.WhenAll(tasks)
                         let deserialized = TryDeserializeSetting(json)
                         where deserialized.Active
                         select deserialized;

            return result;
        }

        private Task<IEnumerable<UserSetting>> QueryForSettingsBySender(string sender)
        {
            return Task.Run(() =>
                from file in _settingsFolder.GetFiles()
                where file.Extension == ".json"
                let value = file.OpenText().ReadToEnd()
                let deserialized = TryDeserializeSetting(value)
                where deserialized.Active && deserialized.Sender == sender
                select deserialized
            );
        }

        private Task<IEnumerable<UserSetting>> QueryForSettingByID(string id)
        {
            return Task.Run(() =>
                from file in _settingsFolder.GetFiles()
                where file.Name == id + ".json"
                let value = file.OpenText().ReadToEnd()
                let deserialized = TryDeserializeSetting(value)
                select deserialized
            );
        }

        private UserSetting TryDeserializeSetting(string fileContents)
        {
            try
            {
                return JsonConvert.DeserializeObject<UserSetting>(fileContents, _serializerSettings);
            }
            catch
            {
                Console.WriteLine("Met malformed file");
                return new UserSetting();
            }
        }
    }
}