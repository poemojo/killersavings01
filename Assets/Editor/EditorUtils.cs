#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Linq;

using GameObjectGroup = System.Collections.Generic.List<UnityEngine.GameObject>;
using Process = System.Diagnostics.Process;
using ProcStartInfo = System.Diagnostics.ProcessStartInfo;
using Regex = System.Text.RegularExpressions.Regex;

public class EditorUtils : MonoBehaviour
{
   [MenuItem("Utils/Search and Replace Names")]
   public static void BatchRename()
   {
      EditorWindow window = new RegexRename();
      window.position = new Rect(Screen.width / 2, Screen.height / 2, 360, 150);
      window.ShowUtility();
   }
   [MenuItem("Utils/Enumerate Objects")]
   public static void EnumerateObjects()
   {
      
   }
   [MenuItem("Assets/Open in Sublime Text")]
   public static void OpenInSublime()
   {
      Object obj = Selection.activeObject;
      if (obj == null) return;

      string path = Application.dataPath.Split('/').shorten(1).join("/")+'/'+AssetDatabase.GetAssetPath(obj.GetInstanceID());
      path = path.Replace('/', '\\');

      ProcStartInfo startInfo = new ProcStartInfo();
      startInfo.FileName = @"K:\Program Files\Sublime Text 3\subl.exe";
      startInfo.Arguments = path;
      Process.Start(startInfo);
   }
   
}
public abstract class SearchWindow : EditorWindow
{
   protected static string _search, _replace;
   protected static SearchType _stype = SearchType.Hierarchy;

   protected virtual string search 
   { 
      get { return _search; }
      set { _search = value; }
   }
   protected virtual string replace 
   { 
      get { return _replace; }
      set { _replace = value; }
   }
   protected virtual SearchType stype
   {
      get { return _stype; }
      set { _stype = value; }
   }

   protected GameObjectGroup GetGameObjects()
   {
      GameObjectGroup result = new GameObjectGroup();

      switch (stype)
      {
         case SearchType.Hierarchy:
            GameObject pgo = Selection.activeGameObject;
            result.Add(pgo);

            Transform parent = pgo.transform;
            result.AddRange(Utilities.GetChildGameObjects(parent));
            break;

         case SearchType.Selection:
            result.AddRange(Selection.gameObjects);
            break;

         case SearchType.All:
            result.AddRange(FindObjectsOfType<GameObject>());
            break;

         default:
            throw new System.Exception("RegexRename.stype is invalidly defined!");

      }

      return result;
   }

   public void OnGUI()
   {
      string[] sel = { "Hierarchy", "Selected", "All" };

      search = EditorGUILayout.TextField("Search Tag:", search);
      replace = EditorGUILayout.TextField("Replace Tag:", replace);

      stype = (SearchType)(GUILayout.SelectionGrid((int)stype, sel, 3, EditorStyles.radioButton));
   }
}
public class RegexRename : SearchWindow
{

   protected new static string _search, _replace;
   protected new static SearchType _stype = SearchType.Hierarchy;

   protected override string search
   {
      get { return _search; }
      set { _search = value; }
   }
   protected override string replace
   {
      get { return _replace; }
      set { _replace = value; }
   }
   protected override SearchType stype
   {
      get { return _stype; }
      set { _stype = value; }
   }

   public void OnGUI()
   {
      base.OnGUI();
      if(GUILayout.Button("Rename"))
      {
         if (!(search.validate() && replace.validate()))
            return;
         int renCount = 0;

         GameObjectGroup gameObjs = GetGameObjects();

         foreach (GameObject go in gameObjs)
         {
            string oname = go.name;

            string nname = Regex.Replace(oname, search, replace);

            if (!nname.Equals(oname))
            {
               renCount++;
               go.name = nname;
            }
         }
         this.Close();
         EditorUtility.DisplayDialog("Rename Report", System.String.Format("{0} item(s) renamed!\n\nSearch Term: \"{1}\"\nReplace Term: \"{2}\"", renCount, search, replace), "Close");
      }

   }
}

#endif
