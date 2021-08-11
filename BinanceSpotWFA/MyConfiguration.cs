using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace BinanceSpotWFA
{
    static class MyConfiguration
    {
        public static string ReadSetting(string key)
        {
            string value = "";
            try
            {
                var _appSettings = ConfigurationManager.AppSettings;
                value = _appSettings[key] ?? "";// "Not Found";
                Console.WriteLine(value);                
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
            return value;
        }

        public static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }
    }
}
