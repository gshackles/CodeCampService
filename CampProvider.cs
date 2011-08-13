using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace CodeCampService
{
    public class CampProvider
    {
        private Dictionary<string, CampCacheItem> _cache;

        public CampProvider(string folderPath)
        {
            _cache = new Dictionary<string, CampCacheItem>();

            foreach (var file in Directory.GetFiles(folderPath, "*.xml"))
            {
                updateCache(file);
            }
        }

        public int GetVersionNumber(string campKey)
        {
            return _cache[campKey.ToLower()].Version;
        }

        public string GetXml(string campKey)
        {
            return _cache[campKey.ToLower()].Xml;
        }

        private void updateCache(string filePath)
        {
            try
            {
                var file = new FileInfo(filePath);
                var xml = XDocument.Load(filePath);

                _cache[getFileNameWithoutExtension(file).ToLower()] = 
                    new CampCacheItem
                    {
                        Version = int.Parse(xml.Root.Attribute("version").Value),
                        Xml = xml.ToString()
                    };    
            }
            catch
            {
            }
        }

        private string getFileNameWithoutExtension(FileInfo file)
        {
            return file.Name.Substring(0, file.Name.Length - file.Extension.Length);
        }

        private class CampCacheItem
        {
            public int Version { get; set; }
            public string Xml { get; set; }
        }
    }
}