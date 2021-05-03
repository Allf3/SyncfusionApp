using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Models
{
    public class Measurement
    {
        public Guid ID { get; set; }
        public DateTime Date { get; set; }
        public float Humidity { get; set; }
        public float Temperatur { get; set; }
    }
}
