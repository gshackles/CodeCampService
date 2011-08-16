using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.Xml.Serialization;

namespace CodeCampService
{
    public class CampProvider
    {
        private Dictionary<string, CampCacheItem> _cache;
		private MongoDatabase _database;
		private const string _databaseName = "CodeCamps";
		private const string _collectionName = "Camps";

        public CampProvider(string connectionString)
        {
            _cache = new Dictionary<string, CampCacheItem>();
			
			_database = MongoServer
							.Create(connectionString)
							.GetDatabase(_databaseName);
        }
		
		private CampCacheItem getCamp(string campKey)
		{
			if (!_cache.ContainsKey(campKey))
			{
				var collection = _database.GetCollection<Camp>(_collectionName);
				var camp = collection.FindOne(Query.EQ("Key", campKey));
				var serializer = new XmlSerializer(typeof(Camp));

				using (var writer = new StringWriter())
				{
					serializer.Serialize(writer, camp);
					
					_cache[campKey] = new CampCacheItem
					{
						Version = camp.Version,
						Xml = writer.ToString()
					};
				}
			}
			
			return _cache[campKey];
		}
		
		public void ResetCache(string campKey)
		{
			if (!_cache.ContainsKey(campKey))
				return;
			
			_cache[campKey] = getCamp(campKey);
		}
		
        public int GetVersionNumber(string campKey)
        {
            return getCamp(campKey).Version;
        }

        public string GetXml(string campKey)
        {
            return getCamp(campKey).Xml;
        }

        private class CampCacheItem
        {
            public int Version { get; set; }
            public string Xml { get; set; }
        }
    }
}