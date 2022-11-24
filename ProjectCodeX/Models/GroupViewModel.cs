using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ProjectCodeX.Models
{
    public class GroupViewModel
    {
        public List<Group> Groups { get; set; } = new();
        public Group GroupDetail { get; set; }
    }
}
