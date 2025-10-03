using System.IO;
using FpsEcs.Runtime.Utils;
using UnityEditor;
using UnityEngine;

namespace FpsEcs.Editor
{
    public static class EditorTools 
    {
        [MenuItem("Tools/Common/Delete Progress")]
        public static void DeleteProgress()
        {
            string path = Path.Combine(Application.persistentDataPath, Constants.Utils.SaveKey);

            bool fileExists  = File.Exists(path);

            string message = "Delete Progress?";
            if (fileExists)
            {
                var details = $"File path:\n{path}\n\n" + (fileExists ? "File will be deleted.\n" : "File not found.\n");
                if (!EditorUtility.DisplayDialog("Delete Progress", message + "\n\n" + details, "Delete", "Cancel"))
                    return;
            }
            else
            {
                EditorUtility.DisplayDialog("Delete Progress", "Nothing to delete.", "OK");
                return;
            }

            if (fileExists)
            {
                try
                {
                    File.Delete(path);
                    Debug.Log($"[DeleteProgress] File has been deleted: {path}");
                }
                catch (IOException ex)
                {
                    Debug.LogError($"[DeleteProgress] Could not delete file: {path}\n{ex}");
                }
            }

            EditorUtility.DisplayDialog("Delete Progress", "Progress has been deleted.", "OK");
        }
    }
}