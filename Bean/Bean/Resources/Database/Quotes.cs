using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Bean.Resources.Database
{
    public class Quote
    { // This is called an Entity
        [Key]
        public int QuoteId { get; set; }
        public string QuoteText { get; set; }
        public string QuoteAuthor { get; set; }
        public string QuoteSource { get; set; }
        public string QuoteDate { get; set; }
        public string QuoteContributor { get; set; }
        public string DateAdded { get; set; }
    }
}
