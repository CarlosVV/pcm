using ProjectContentManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectContentManager
{
    public static class NavigationManager
    {
        private static ContentManagerModel db = new ContentManagerModel();
        public static NavigationViewModel GetNavigationCategories()
        {
            var firstLevelCategories = db.Categories.Where(m => m.Root == null).ToList();
            var menus = new NavigationViewModel();
            menus.Items = new List<NavigationItemViewModel>();

            foreach (Category c in firstLevelCategories)
            {
                menus.Items.Add(new NavigationItemViewModel
                {
                    Id = c.CategoryId.ToString(),
                    Name = c.Name,
                    SubItems = db.Categories.Where(m => m.Root == c.CategoryId).
                        Select(m => new NavigationItemViewModel
                        {
                            Id = m.CategoryId.ToString(),
                            Name = m.Name
                        }).ToList()
                });
            }

            return menus;
        }
    }
}