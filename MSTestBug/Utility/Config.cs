using System.Collections.Specialized;
using System.Configuration;

namespace Utility
{
    /// <summary>
    /// Config class
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// Configuration mapping for the custom Magenic Maqs section
        /// </summary>
        private static NameValueCollection config = ConfigurationManager.GetSection("SAMPLE") as NameValueCollection;


        /// <summary>
        /// Get the configuration value for a specific key
        /// </summary>
        /// <param name="key">Config file key</param>
        /// <returns>The configuration value - Returns the empty string if the key is not found</returns>
        public static string GetValue(string key)
        {
            return GetValue(key, string.Empty);
        }

        /// <summary>
        /// Get the configuration value for a specific key
        /// </summary>
        /// <param name="key">Config file key</param>
        /// <param name="defaultValue">Default value - Returned the key cannot be found</param>
        /// <returns>The configuration value</returns>
        public static string GetValue(string key, string defaultValue)
        {
            return config[key] ?? defaultValue;
        }
    }
}
