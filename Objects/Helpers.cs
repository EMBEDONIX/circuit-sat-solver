using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using SatSolver.Objects.Gates;

namespace SatSolver.Objects
{
    public static class Helpers
    {

        #region Extensions
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetGateNameString(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            GateAttribute[] attributes =
                (GateAttribute[])fi.GetCustomAttributes(
                typeof(GateAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
            {
                return attributes[0].Name;
            }

            return value.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GetGateMaxInputs(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            GateAttribute[] attributes =
                (GateAttribute[])fi.GetCustomAttributes(
                typeof(GateAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
            {
                return attributes[0].MaxInputs;
            }

            return 0; //if attribute is not set return 0
        }

        /// <summary>
        /// Take a range from an array and return it
        /// </summary>
        /// <typeparam name="T">Type of arry</typeparam>
        /// <param name="data">The array to operate on</param>
        /// <param name="index">Starting index where the sub array should be taken from</param>
        /// <param name="length">The length to take from array</param>
        /// <returns></returns>
        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

        /// <summary>
        /// Converts a given string to an enum from the given enum type
        /// </summary>
        /// <typeparam name="T">Type of the enum</typeparam>
        /// <param name="value">The string to </param>
        /// <returns></returns>
        public static T ParseEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        #endregion

        /// <summary>
        ///     Check if a string is only consist of digits
        /// </summary>
        /// <param name="str">the string to perform the check on</param>
        /// <returns>True if string only contains digits, otherwise false</returns>
        public static bool IsDigitsOnly(string str)
        {
            return str.All(c => c >= '0' && c <= '9');
        }

        /// <summary>
        ///     Check if all strings inside an array of strings are only consist of digits
        /// </summary>
        /// <param name="strArray">the string array to perform the check on</param>
        /// <returns>True if string only contains digits, otherwise false</returns>
        public static bool IsDigitsOnly(string[] strArray)
        {
            if (strArray == null) throw new ArgumentNullException("strArray");
            return strArray.All(IsDigitsOnly);
        }
    }
}
