using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace SMVC
{
    internal abstract class DataMember
    {
        public abstract string Name { get; }
        public abstract Type DataType { get; }
        public abstract object GetValue(object obj);
        public abstract void SetValue(object item, object value);
    }

    internal class PropertyMember : DataMember
    {
        PropertyInfo pi;

        public PropertyMember(PropertyInfo pi)
        {
            this.pi = pi;            
        }

        public override string Name
        {
            get { return pi.Name; }
        }

        public override Type DataType
        {
            get { return pi.PropertyType; }
        }

        public override object GetValue(object obj)
        {
            return pi.GetValue(obj, null);
        }

        public override void SetValue(object item, object value)
        {
            try
            {
                pi.SetValue(item, value, null);
            }
            catch
            {
                ExceptionHelper.Throw405Exception(null);
            }
        }
    }

    internal class FieldMember : DataMember
    {
        FieldInfo fi;

        public FieldMember(FieldInfo fi)
        {
            this.fi = fi;
        }

        public override string Name
        {
            get { return fi.Name; }
        }

        public override Type DataType
        {
            get { return fi.FieldType; }
        }

        public override object GetValue(object obj)
        {
            return fi.GetValue(obj);
        }

        public override void SetValue(object item, object value)
        {
            try
            {
                fi.SetValue(item, value);
            }
            catch
            {
                ExceptionHelper.Throw405Exception(null);
            }
        }
    }

}
