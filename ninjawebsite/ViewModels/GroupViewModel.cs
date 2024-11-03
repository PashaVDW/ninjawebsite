using ninjawebsite.Models;
using System.Collections.Generic;

namespace ninjawebsite.ViewModels
{
    public class GroupViewModel<ListItemType>
    {
        public List<ListItemType> List { get; set; }
        public Ninja Ninja { get; set; }
        public List<Category> Categories { get; set; }
        public int SelectedCategoryId { get; set; }
    }
}
