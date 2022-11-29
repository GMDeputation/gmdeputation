using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.Models
{
    public class ChurchAccommodation
    {
        public int Id { get; set; }
        public int ChurchId { get; set; }
        public string ChurchName { get; set; }
        public string AccomType { get; set; }
        public string AccomNotes { get; set; }
    }

    public class ChurchAccommodationQuery : Query
    {
        public string Name { get; set; }
    }
}
