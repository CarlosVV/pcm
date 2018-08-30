using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web;
using System.Web.Mvc;
using ProjectContentManager.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;


namespace ProjectContentManager
{
    public static class NavigationManager
    {
        public static NavigationViewModel GetNavigationCategories()
        {
            var db = new ContentManagerModelContext();
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

        public static ApplicationUser User
        {
            get
            {
                var user = ApplicationDbContext.Create().Users.Where(m => m.Email == HttpContext.Current.User.Identity.Name).FirstOrDefault();
                return user;
            }
        }
    }
}