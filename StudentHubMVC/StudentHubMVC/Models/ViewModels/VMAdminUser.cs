using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentHubMVC.Models.ViewModels
{
    public class VMAdminUser
    {

    }

    public class VMAdminModulePermission
    {
        public string PermissionId { get; set; }
        public string ModuleName { get; set; }
        public string ModuleDescription { get; set; }
    }
}