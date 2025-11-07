using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrowEye_Automation_Portal.PageRepository.Objects
{
    public class BcssConfigurationRecord
    {
        private string _name;

        private string _description;

        private string _profileName;

        public string Name { get { return _name; } set { this._name = value; } }

        public string Description { get { return _description; } set { this._description = value; } }

        public string ProfileName { get { return _profileName; } set { this._profileName = value; } }
    }

    
}
