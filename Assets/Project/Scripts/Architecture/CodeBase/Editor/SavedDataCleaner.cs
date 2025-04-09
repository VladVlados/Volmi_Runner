using System.IO;
using Project.Scripts.Architecture.CodeBase.ConstLogic;
using UnityEditor;
using UnityEngine;

namespace Project.Scripts.Architecture.CodeBase.Editor {
  public static class SavedDataCleaner {
    [MenuItem("Tools/Clear Saved Data")]
    private static void ClearData() {
#if !UNITY_EDITOR
        string filePath = Application.persistentDataPath + Constants.Paths.DATA_PATH;
#else
      string filePath = Application.dataPath + Constants.Paths.DATA_PATH;
#endif
      if (File.Exists(filePath)) {
        File.Delete(filePath);
        Debug.Log("Saved Data cleared!");
      } else {
        Debug.LogWarning("Saved Data does not exist or is inaccessible.");
      }
    }
  }
}