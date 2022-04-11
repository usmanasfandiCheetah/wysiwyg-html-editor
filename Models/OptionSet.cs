using mages.framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace mages.editor.Models
{
    public class OptionSet : BaseModel
    {
        private string _key;

        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        private string _value;

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public OptionSet()
        {

        }

        public OptionSet(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
