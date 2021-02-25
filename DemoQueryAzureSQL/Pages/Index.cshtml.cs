using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoursesWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoursesWebApp.Pages
{
    public class CoursesAndModulesModel : PageModel
    {
        DataAccessController dac = new DataAccessController();

        public List<CoursesAndModules> CoursesAndModules;
        
        public void OnGet()
        {
            CoursesAndModules = dac.GetAllCoursesAndModules().ToList();
        }
    }
}
