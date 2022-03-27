using System.IO;
using System.Reflection;

namespace LocalizationApp.Helpers
{
    public class FileReader
    {
        public static string ReadFileContentFromEmbededResources(string fileAddress)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = fileAddress;
            string result = string.Empty;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }
            return result;
        }
    }
}
