using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManagerApp.Models
{
    public class Property
    {
        public int PropertyID { get; set; }
        public string PropertyName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string UnitNumber { get; set; } = string.Empty;
        public decimal MonthlyRent { get; set; }
    }
}
