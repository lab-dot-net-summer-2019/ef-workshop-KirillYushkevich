using System;
using System.Collections.Generic;
using System.Text;

namespace SamuraiApp.Domain
{
    public class Ronin
    {
        public Ronin()
        {
   
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime FreeDate { get; set; }
        public string SuzerainName { get; set; }
    }
}
