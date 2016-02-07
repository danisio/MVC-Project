namespace MySurveys.Web.Areas.Administration.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Web;
    using Base;
    using Models;
    using Infrastructure.Mapping;
    using System.Web.Mvc;

    public class UserViewModel : AdministrationViewModel, IMapFrom<User>
    {
        [HiddenInput(DisplayValue = false)]
        public int? UserId { get; set; }
    }
}