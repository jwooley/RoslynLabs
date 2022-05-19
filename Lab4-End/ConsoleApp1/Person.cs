using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvSerializer;

namespace ConsoleApp1
{
    [CsvSerializable]
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
