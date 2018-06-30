using System.Collections.Generic;
using System.IO;
using CitizenFX.Core.Native;

namespace DC
{
    public class ConfigParser
    {
        private readonly Dictionary<string, string> dict = new Dictionary<string, string>();

        public ConfigParser(string filename)
        {
            bool result = Read(filename);
        }

        private bool Read(string filename)
        {
            string data = Function.Call<string>(Hash.LOAD_RESOURCE_FILE, Function.Call<string>(Hash.GET_CURRENT_RESOURCE_NAME), filename);
            if (data == null || data.Length == 0) return false;

            using (StringReader reader = new StringReader(data))
            {
                string line = null, section = "";
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (line.Length == 0) continue;
                    if (line.StartsWith(";") || line.StartsWith("#")) continue;                  
                    if (line.StartsWith("[") && line.Contains("]"))
                    {
                        section = line.Substring(1, line.IndexOf("]") - 1).Trim();
                        continue;
                    }

                    if (line.Contains("="))
                    {
                        string key = string.Format("[{0}]{1}", section, line.Substring(0, line.IndexOf("=")).Trim()).ToLower();
                        string value = line.Substring(line.IndexOf("=") + 1).Trim();

                        if (!dict.ContainsKey(key))
                            dict.Add(key, value);
                    }
                }
            }
            return true;
        }

        public string GetStringValue(string key, string defaultValue = "")
        {
            return GetStringValue("", key, defaultValue);
        }

        public string GetStringValue(string section, string key, string defaultValue = "")
        {
            if (dict.TryGetValue("[" + section + "]" + key, out string result))
                return result;
            return defaultValue;
        }

        public double GetDoubleValue(string key, double defaultValue = 0)
        {
            return GetDoubleValue("", key, defaultValue);
        }

        public double GetDoubleValue(string section, string key, double defaultValue = 0)
        {
            if (dict.ContainsKey("[" + section + "]" + key))
            {
                if (double.TryParse(dict["[" + section + "]" + key], out double result))
                    return result;
            }
            return defaultValue;
        }

        public float GetFloatValue(string key, float defaultValue = 0f)
        {
            return GetFloatValue("", key, defaultValue);
        }

        public float GetFloatValue(string section, string key, float defaultValue = 0f)
        {
            if (dict.ContainsKey("[" + section + "]" + key))
            {
                if (float.TryParse(dict["[" + section + "]" + key], out float result))
                    return result;
            }
            return defaultValue;
        }

        public int GetIntValue(string key, int defaultValue = 0)
        {
            return GetIntValue("", key, defaultValue);
        }

        public int GetIntValue(string section, string key, int defaultValue = 0)
        {
            if (dict.ContainsKey("[" + section + "]" + key))
            {
                if (int.TryParse(dict["[" + section + "]" + key], out int result))
                    return result;
            }
            return defaultValue;
        }

        public bool GetBoolValue(string key, bool defaultValue = false)
        {
            return GetBoolValue("", key, defaultValue);
        }

        public bool GetBoolValue(string section, string key, bool defaultValue = false)
        {
            if (dict.ContainsKey("[" + section + "]" + key))
            {
                if (bool.TryParse(dict["[" + section + "]" + key], out bool result))
                    return result;
            }
            return defaultValue;
        }
    }
}
