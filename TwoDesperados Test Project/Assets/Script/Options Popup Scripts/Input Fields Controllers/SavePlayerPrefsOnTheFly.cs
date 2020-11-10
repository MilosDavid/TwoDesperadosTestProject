using UnityEngine;

public class SavePlayerPrefsOnTheFly : MonoBehaviour
{
    public void SavePlayerPrefs()
    {
        CustomEvents.setGameDataEvent.Invoke();
    }
}
