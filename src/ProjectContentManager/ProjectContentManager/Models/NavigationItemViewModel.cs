using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectContentManager.Models
{
    public class NavigationItemViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<NavigationItemViewModel> SubItems { get; set; }
    }
}