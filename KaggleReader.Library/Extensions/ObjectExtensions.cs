using System;
using System.Collections;

namespace KaggleReader.Library.Extensions
{
    public static class ObjectExtensions
    {
        public static bool AnyPropertyContains(this object o, string pattern, bool recursive = false)
        {
            if (o == null)
                return false;
            if ((o.GetType().IsPrimitive) || (o.GetType() == typeof(string)))
                return o.ToString().WildContains(pattern);
            var properties = o.GetType().GetProperties();
            foreach (var property in properties)
            {
                object propValue = null;
                try
                {
                    propValue = property.GetValue(o, null);
                }
                catch (Exception)
                {

                    break;
                }

                if (propValue == null)
                    continue;
                if (property.PropertyType.IsPrimitive || property.PropertyType == typeof(string))
                {
                    //Simple Property, do value check
                    var value = propValue.ToString();
                    if (value.WildContains(pattern))
                        return true;

                }
                else if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
                {
                    if (!recursive)
                        continue;
                    IEnumerable enumerable = (IEnumerable)propValue;
                    if (enumerable != null)
                    {
                        foreach (object child in enumerable)
                        {
                            var subResult = child.AnyPropertyContains(pattern: pattern, recursive: recursive);
                            if (subResult == true)
                                return true;
                        }
                    }
                }
                else
                {
                    //Complex
                    if (!recursive)
                        continue;

                    var subResult = propValue.AnyPropertyContains(pattern: pattern, recursive: recursive);
                    if (subResult == true)
                        return true;
                }
            }
            return false;
        }

        public static bool AnyPropertyCombiContains(this object o, string patterns, bool recursive = false)
        {
            if (o == null)
                return false;
            if ((o.GetType().IsPrimitive) || (o.GetType() == typeof(string)))
                return o.ToString().WildCombiContains(patterns);
            var properties = o.GetType().GetProperties();
            foreach (var property in properties)
            {
                object propValue = null;
                try
                {
                    propValue = property.GetValue(o, null);
                }
                catch (Exception)
                {

                    break;
                }

                if (propValue == null)
                    continue;
                if (property.PropertyType.IsPrimitive || property.PropertyType == typeof(string))
                {
                    //Simple Property, do value check
                    var value = propValue.ToString();
                    if (value.WildCombiContains(patterns))
                        return true;

                }
                else if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
                {
                    if (!recursive)
                        continue;
                    IEnumerable enumerable = (IEnumerable)propValue;
                    if (enumerable != null)
                    {
                        foreach (object child in enumerable)
                        {
                            var subResult = child.AnyPropertyCombiContains(patterns: patterns, recursive: recursive);
                            if (subResult == true)
                                return true;
                        }
                    }
                }
                else
                {
                    //Complex
                    if (!recursive)
                        continue;

                    var subResult = propValue.AnyPropertyCombiContains(patterns: patterns, recursive: recursive);
                    if (subResult == true)
                        return true;
                }
            }
            return false;
        }

        public static bool PropertyWildCombiEquals(this object o, string propertyName, string values)
        {
            if (o == null)
                return false;
            if ((o.GetType().IsPrimitive) || (o.GetType() == typeof(string)))
                return o.ToString().WildCombiEquals(values);
            //var properties = o.GetType().GetProperties();
            var property = o.GetType().GetProperty(name: propertyName);
            if (property == null)
                return false;
            try
            {
                var propertyValue = property.GetValue(o, null);
                return propertyValue.ToString().WildCombiEquals(values);
            }
            catch (Exception)
            {

                return false;
            }
            return false;
        }

        public static bool PropertyWildEquals(this object o, string propertyName, string value)
        {
            if (o == null)
                return false;
            if ((o.GetType().IsPrimitive) || (o.GetType() == typeof(string)))
                return o.ToString().WildEquals(value);
            //var properties = o.GetType().GetProperties();
            var property = o.GetType().GetProperty(name: propertyName);
            if (property == null)
                return false;
            try
            {
                var propertyValue = property.GetValue(o, null);
                return propertyValue.ToString().WildEquals(value);
            }
            catch (Exception)
            {

                return false;
            }
            return false;
        }

        public static bool IsComplex(this Type type)
        {
            if (type.UnderlyingSystemType.Name.WildEquals("string"))
                return false;
            return !type.UnderlyingSystemType.IsPrimitive;
            return false;
        }
    }
}
