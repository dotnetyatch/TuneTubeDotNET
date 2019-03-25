using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuneTube.Models
{
    public class QueryResult
    {
       public string url { get; set; }
        public bool Empty
        {
            get
            {
                return (
                        string.IsNullOrWhiteSpace(url)
                );
            }
        }
    }
}
