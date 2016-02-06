<<<<<<< HEAD
﻿using System.Web.Mvc;

namespace MySurveys.Web.Areas.Administration
{
    public class AdministrationAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
=======
﻿namespace MySurveys.Web.Areas.Administration
{
    using System.Web.Mvc;

    public class AdministrationAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
>>>>>>> master
            {
                return "Administration";
            }
        }

<<<<<<< HEAD
        public override void RegisterArea(AreaRegistrationContext context) 
=======
        public override void RegisterArea(AreaRegistrationContext context)
>>>>>>> master
        {
            context.MapRoute(
                "Administration_default",
                "Administration/{controller}/{action}/{id}",
<<<<<<< HEAD
                new { action = "Index", id = UrlParameter.Optional }
            );
=======
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "MySurveys.Web.Areas.Administration.Controllers" });
>>>>>>> master
        }
    }
}