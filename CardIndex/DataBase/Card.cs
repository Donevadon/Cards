using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardIndex.DataBase
{
    public class Card
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public string Gender { get; set; }
        public string Birthday { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
