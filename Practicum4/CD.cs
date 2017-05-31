using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practicum4
{
    class CD
    {
        private List<Track> tracks = new List<Track>();

        public List<Track> Tracks
        {
            get;
            set;
        }

        public String Title { get; set; }
        public String Artist { get; set; }
    }
}
