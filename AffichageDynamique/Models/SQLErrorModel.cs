using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AffichageDynamique.Models
{
    public class SQLErrorModel
    {
        public int Number { get; set; }
        public int Class { get; set; }
        public int State { get; set; }
        public int LineNumber { get; set; }
        public string Message { get; set; }
        public string Procedure { get; set; }
    }
}
