using UnityEngine;

public class CheckResults : MonoBehaviour
{
    private void OnEnable()
    {
        CustomEvents.raceFinishedEvent.AddListener(RacedFinished);
    }

    private void OnDisable()
    {
        CustomEvents.raceFinishedEvent.RemoveListener(RacedFinished);
    }

    bool firstPlayerFinished = false;
    bool secondPlayerFinished = false;

    private void RacedFinished(string playerName, bool finished)
    {
        if (playerName == "Player1")
        {
            firstPlayerFinished = true;
        }
        if (playerName == "Player2")
        {
            secondPlayerFinished = true;
        }

        InvokeRepeating("CheckRaceResult", 0, 2);
    }

    void CheckRaceResult()
    {
        if (firstPlayerFinished && secondPlayerFinished)
        {
            CustomEvents.showGameCompletedPopupEvent.Invoke();
            CancelInvoke("CheckRaceResult");
        }
    }
}
