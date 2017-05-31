using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Practicum4
{
    class Program
    {
        static void Main(string[] args)
        {
            CD xd = new CD
            {
                Title = "Awake",
                Artist = "Dream Theater",
                Tracks = new List<Track>
                {
                    new Track{Title = "BOOM", Artist = "Dream Theater", Length = new TimeSpan(0,03,00) },
                    new Track { Title = "XD", Artist = "Dream Theater", Length = new TimeSpan(0, 03, 12) },
                    new Track{Title = "RealMusic", Artist = "Dream Theater" , Length = new TimeSpan(0,03,06) }
                }
            };

            String xmlString;
            using (var wc = new System.Net.WebClient())
            {
                xmlString = wc.DownloadString(@"http://ws.audioscrobbler.com/2.0/?method=album.getInfo&album=awake&artist=Dream%20Theater&api_key=b5cbf8dcef4c6acfc5698f8709841949");
            }
            XDocument xdXml = XDocument.Parse(xmlString);

            Console.WriteLine("Parsing XML Document");


            var query = from track in xdXml.Element("lfm").Element("album").Element("tracks").Elements("track") where !xd.Tracks.Select(t => t.Title).Contains(track.Element("name").Value) select track;

            foreach (var track in query)
            {
                xd.Tracks.Add(new Track
                {
                    Title = track.Element("name").Value,
                    Artist = track.Element("artist").Element("name").Value,
                    Length = TimeSpan.FromSeconds(Convert.ToDouble(track.Element("duration").Value))
                });
            }

            var xddd = new XDocument(new XElement("CDs", new XElement("CD", new XAttribute("Title", xd.Title), new XAttribute("Artist", xd.Artist), new XElement("Tracks",
                        from t in xd.Tracks
                        select new XElement("Track", new XElement("TitelTrack", t.Title), new XElement("ArtistTrack", t.Artist), new XElement("LengthTrack", Convert.ToString(t.Length)))))));



            Console.WriteLine(xddd.ToString());
            Console.Read();
        }
    }
}


