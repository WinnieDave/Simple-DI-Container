using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC
{
    class BindingInfo
    {
        public Type Target
        { get; private set; }
        public Type Source
        { get; private set; }
        public BindingType BindingType
        { get; private set; }
        public BindingInfo(Type t, Type s, BindingType bt)
        {
            Target = t;
            Source = s;
            BindingType = bt;
        }
        public BindingInfo()
        { }
    }
}
