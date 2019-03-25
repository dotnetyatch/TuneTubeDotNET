using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuneTube.ViewModels
{
    public class SearchViewModel
    {
        [Required]
        public string Title { get; set; }
        public string Check { get; set; }

    }
}
