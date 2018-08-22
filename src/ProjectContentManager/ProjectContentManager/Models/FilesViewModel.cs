using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectContentManager.Models
{
    public class FilesViewModel
    {
        public int CategoryId { get; set; }
        public List<Content> Files { get; set; }
    }
}