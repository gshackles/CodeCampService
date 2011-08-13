using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace CodeCampService
{
    public class CampProvider
    {
        private FileSystemWatcher _watcher;
        private Dictionary<string, CampCacheItem> _cache;

        public CampProvider(string folderPath)
        {
            _cache = new Dictionary<string, CampCacheItem>();
            _watcher = new FileSystemWatcher(folderPath);

            _watcher.Filter = "*.xml";
            _watcher.Created += onFileCreatedOrChanged;
            _watcher.Changed += onFileCreatedOrChanged;
            _watcher.Deleted += onFileDeleted;

            _watcher.EnableRaisingEvents = true;

            initCache();
        }

        public int GetVersionNumber(string campKey)
        {
            return _cache[campKey.ToLower()].Version;
        }

        public string GetXml(string campKey)
        {
            return _cache[campKey.ToLower()].Xml;
        }

        private void initCache()
        {
            foreach (var file in Directory.GetFiles(_watcher.Path, "*.xml"))
            {
                updateCache(file);
            }
        }

        private void onFileDeleted(object sender, FileSystemEventArgs e)
        {
            _cache.Remove(e.Name);
        }

        private void onFileCreatedOrChanged(object sender, FileSystemEventArgs e)
        {
            updateCache(e.FullPath);
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