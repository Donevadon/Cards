using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardIndex.DataBase
{
    public enum TypeVisit
    {
        Primary,
        Secondary
    }
    public class Visit
    {
        public int Id { get; set; }
        public int IdCard { get; set; }
        public string DateVitis { get; set; }
        public int TypeVisit { get; set; }
        public string Diagnosis { get; set; }

    }
}
