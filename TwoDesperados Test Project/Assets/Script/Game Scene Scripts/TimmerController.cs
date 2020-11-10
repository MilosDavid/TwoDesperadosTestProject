using UnityEngine;

public class TimmerController : MonoBehaviour
{
    private float secondsCount;

    private void OnEnable()
    {
        CustomEvents.raceFinishedEvent.AddListener(SavePlayerTime);
    }

    private void OnDisable()
    {
        CustomEvents.raceFinishedEvent.RemoveListener(SavePlayerTime);
    }

    private void SavePlayerTime(string playerName, bool finished)
    {
        if (finished && this.transform.name == playerName)
        {
            if (playerName == "Player1")
            {
                PlayerPrefs.SetFloat(PrefsKeys.TimeForPlayerOne_Key, this.secondsCount);
                PlayerPrefs.Save();
            }
            if (playerName == "Player2")
            {
                PlayerPrefs.SetFloat(PrefsKeys.TimeForlayerTwo_Key, this.secondsCount);
                PlayerPrefs.Save();
            }
        }
    }

    void Update()
    {
        UpdateTimerUI();
    }

    public void UpdateTimerUI()
    {
        secondsCount += Time.deltaTime;
    }
}
