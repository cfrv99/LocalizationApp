using LocalizationApp.Helpers;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;

namespace LocalizationApp.Resources
{
    public interface ILocalizationService
    {
        string GetByKey(string key);
    }
    public class LocalizationService : ILocalizationService
    {
        private readonly IMemoryCache memoryCache;

        public LocalizationService(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }
        public string GetByKey(string key)
        {
            var currentCulture = CultureInfo.CurrentCulture;

            JObject jsonObj;

            string cacheKey = "L" + currentCulture.Name;

            if (!memoryCache.TryGetValue(cacheKey, out jsonObj))
            {
                string jsonContent;
                switch (currentCulture.Name)
                {
                    case "az":
                        jsonContent = FileReader.ReadFileContentFromEmbededResources("LocalizationApp.Resources.az.json");
                        break;
                    case "en":
                        jsonContent = FileReader.ReadFileContentFromEmbededResources("LocalizationApp.Resources.en.json");
                        break;
                    default:
                        jsonContent = FileReader.ReadFileContentFromEmbededResources("LocalizationApp.Resources.az.json");
                        break;
                }

                jsonObj = JObject.Parse(jsonContent);

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromDays(1));

                memoryCache.Set(cacheKey, jsonObj, cacheOptions);
            }
            var translatedWord = jsonObj[key]?.Value<string>();

            if (string.IsNullOrEmpty(translatedWord))
            {
                translatedWord = key;
            }

            return translatedWord;
        }
    }
}
