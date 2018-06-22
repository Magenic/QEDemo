using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class  FileReader
     {
        /// <summary>
        /// Get text from a file
        /// </summary>
        /// <param name="relativePath">Relative path to a file</param>
        /// <returns>The file content</returns>
        public static string GetText(string relativePath)
        {
            string defaultPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            Console.WriteLine("Open file: " + Path.Combine(defaultPath, relativePath));
            return File.ReadAllText(Path.Combine(defaultPath, relativePath));
        }
    }
}
