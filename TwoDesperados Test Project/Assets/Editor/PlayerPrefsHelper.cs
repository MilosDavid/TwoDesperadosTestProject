using UnityEditor;
using UnityEngine;

public class PlayerPrefsHelper : MonoBehaviour
{
    [MenuItem("HelperMenue/ResetPlayerPrefs")]
    public static void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        FileUtil.DeleteFileOrDirectory(Application.persistentDataPath);
        Debug.Log("<color=red>Player Prefs Deleted</color>");
    }
}
