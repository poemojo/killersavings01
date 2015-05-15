using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Reflection;

using GameObjectGroup = System.Collections.Generic.List<UnityEngine.GameObject>;

public static class Utilities
{
   public static GameObjectGroup GetChildGameObjects(Transform parent)
   {
      GameObjectGroup result = new GameObjectGroup();

      foreach (Transform child in parent)
      {
         result.Add(child.gameObject);

         if (child.childCount > 0)
            result.AddRange(GetChildGameObjects(child));
      }

      return result;
   }

   public static GameObject FindChildGameObjectWithTag(this GameObject parent, string tag, bool grandchildren = true)
   {
      foreach (Transform child in parent.transform)
      {
         if (child.gameObject.tag.Equals(tag))
            return child.gameObject;

         if (grandchildren && (child.childCount > 0))
            return FindChildGameObjectWithTag(child.gameObject, tag);
      }

      return null;
   }

   public static bool validate(string value)
   { return ((value != "") && (value != null)); }

   public static bool validate<T>(this T value)
   {
      if (IsNull(value))
         return false;
      if (value is string)
         return !(string.IsNullOrEmpty(value as string));

      return !(EqualityComparer<T>.Default.Equals(value, default(T)));
   }

   public static void sort(this Array arr)
   {  Array.Sort(arr); }
   public static bool IsNull<T>(this T value)
   {
      if (value is ValueType)
         return false;

      return null == (object)value;
   }

   public static T[] shorten<T>(this T[] arr, int cut)
   {
      int length = arr.Length;

      if (cut >= length || cut <= 0)
         throw new IndexOutOfRangeException("Shorten length {0} is out of bounds! Must be between 0 and {1}".fmt(cut, length - 1));

      int newlen = length - cut;
      T[] newArr = new T[newlen];

      for (int i=0; i<newlen; i++)
         newArr[i] = arr[i];

      return newArr;
   }

   public static string fmt(this string str, params object[] args)
   { return String.Format(str, args); }

   public static string join(this string[] arr, string seperator)
   { return String.Join(seperator, arr); }

   public static string SwapRichTags(this string text)
   {
      return text.Replace('[', '<').Replace(']', '>');
   }

   public static string GetPrototype(this MethodInfo mi)
   {
      string[] args = mi.GetParameters()
         .Select(p => String.Format("{0} {1}", p.ParameterType.Name, p.Name))
         .ToArray();

      string prototype = String.Format("{0} {1}::{2}({3})", mi.ReturnType.Name, mi.DeclaringType.Name, mi.Name, String.Join(", ", args));
      return prototype;
   }

   public static Color lerp(this Color a, Color b, float t)
   {
      return Color.Lerp(a, b, t);
   }

}

public static class EnumUtils
{
   public static string idAbbr(this Categories cat)
   {
      string str = cat.ToString().ToLower();
      string search = @"^(.+)(actors|props|items)$";

      string top = Regex.Replace(str, search,"$2");
      string tmp = Regex.Replace(str, search, "$1");
      string sub = Regex.Replace(tmp, "(?<!^)[aouieyAOUIEY](?!$)", "");
 
      return string.Format("{0}x{1}",top,sub);
   }

   public static T FromString<T>(this T self, string value, bool ignoreCase = true) where T : struct, IConvertible
   {
      if (!typeof(T).IsEnum)
         throw new ArgumentException("T must be an enum!");

      return (T)Enum.Parse(typeof(T), value, ignoreCase);
   }

}


public static class MathUtils
{
   public static double clamp(double value, double min, double max)
   {
      return (value < min) ? min : (value > max) ? max : value;
   }

   public static float clamp(float value, float min, float max)
   {
      return (value < min) ? min : (value > max) ? max : value;
   }

   public static long clamp(long value, long min, long max)
   {
      return (value < min) ? min : (value > max) ? max : value;
   }

   public static int clamp(int value, int min, int max)
   {
      return (value < min) ? min : (value > max) ? max : value;
   }

   public static short clamp(short value, short min, short max)
   {
      return (value < min) ? min : (value > max) ? max : value;
   }

   public static byte clamp(byte value, byte min, byte max)
   {
      return (value < min) ? min : (value > max) ? max : value;
   }
}

