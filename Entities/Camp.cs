using System;
using System.Xml.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Collections.Generic;

namespace CodeCampService
{
	public class Camp
	{
		[BsonId, XmlIgnore]
		public ObjectId Id { get; set; }
	
		[XmlAttribute]
		public int Version { get; set; }
		
		[XmlIgnore]
		public string Key { get; set; }
		
		public List<Session> Sessions { get; set; }
		public List<Speaker> Speakers { get; set; }
		public List<Sponsor> Sponsors { get; set; }
		
		[XmlArrayItem("Tier")]
		public List<string> SponsorTiers { get; set; }
		
		[XmlArrayItem("Room")]
		public List<Room> Rooms { get; set; }
	}
}

