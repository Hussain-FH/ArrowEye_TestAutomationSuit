using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrowEye_Automation_Portal.PageRepository.Objects
{
    public class PrintSettingsRecord
    {
        private string _key;

        private string _description;

        private string _valueType;

        private string _value;

        public string Key { get { return _key; } set { this._key = value; } }

        public string Description { get { return _description; } set { this._description = value; } }

        public string ValueType { get { return _valueType; } set { this._valueType = value; } }

        public string Value{ get { return _value; } set { this._value = value; } }
    }
}
