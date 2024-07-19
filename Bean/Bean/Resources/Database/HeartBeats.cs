using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Bean.Resources.Database
{
    public class HeartBeat
    { // This is called an Entity
        [Key]
        public int HeartRateID { get; set; }
        public int HeartRate { get; set; }
        public ulong HeartRateTimestamp { get; set; }
        public string HeartRateGamePlayed { get; set; }
    }
}