using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShabanguGroupApplication.Interface.Models
{
    public class SideMenuModel
    {
        public SideMenuModel()
        {
            _items = new List<SideMenuItem>();
        }
        List<SideMenuItem> _items;

        public List<SideMenuItem> Items
        {
            get { return _items; }
            set { _items = value; }
        }
    }

    public class SideMenuItem
    {
        public SideMenuItem()
        {
            _items = new List<SideMenuItem>();
        }
        List<SideMenuItem> _items;

        public List<SideMenuItem> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        public string ModuleName { get; set; }
        public string ModuleHref { get; set; }
        public string ModuleClass { get; set; }
        public string ModuleSelected { get; set; }
    }
}