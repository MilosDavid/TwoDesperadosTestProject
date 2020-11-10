using System;
using UnityEngine;
using UnityEngine.UI;
using static AlgorithmType;

public class ResultsController : MonoBehaviour
{
    /*
 Clicking on the “Finish” button opens the “Results” popup, where you have a list of the played
    runs, with information about each run:
    - number of the run (1, 2, 3…)
    - board size
    - obstacle count
    - algorithms used
    - how many fields each algorithm had to check to find the shortest path for its runner
    - time spent in reaching the goal for each runner
 */

    public GameObject ResultWindow;

    public Button CloseResultWindow;

    public Text numberOfTheRuns;
    public Text boardSize;
    public Text obstacleCount;
    public Text algorithmsUsed;
    public Text numberOfFieldsCheckedForFirstAlghoritm;
    public Text numberOfFieldsCheckedForSecondAlghoritm;
    public Text timeSpentForFirstRunner;
    public Text timeSpentForSecondRunner;

    private void OnEnable()
    {
        CustomEvents.showResultsPopupEvent.AddListener(ShowResultsWindow);
    }

    private void OnDisable()
    {
        CustomEvents.showResultsPopupEvent.RemoveListener(ShowResultsWindow);
    }

    private void ShowResultsWindow(bool show)
    {
        PopulateData();

        ResultWindow.SetActive(show);
    }

    public void CloseWindow()
    {
        CustomEvents.showResultsPopupEvent.Invoke(false);
    }

    private void Start()
    {
        CloseResultWindow.onClick.AddListener(CloseWindow);
    }

    private void PopulateData()
    {
        numberOfTheRuns.text = GetNumberOfTheRuns();
        boardSize.text = GameManagerData.GetBoardSize().ToString();
        obstacleCount.text = GameManagerData.GetNumberOfObstacles().ToString();
        algorithmsUsed.text = GetUsedAlgorithms();
        numberOfFieldsCheckedForFirstAlghoritm.text = GetNumberOfCheckedFields(CurrentAlgorithmType.A_star);
        numberOfFieldsCheckedForSecondAlghoritm.text = GetNumberOfCheckedFields(CurrentAlgorithmType.Greedy_suboptimal);
        timeSpentForFirstRunner.text = GetTimeSpent("Runner1");
        timeSpentForSecondRunner.text = GetTimeSpent("Runner2");
    }

    private string GetNumberOfTheRuns()
    {
        return PlayerPrefs.GetInt(PrefsKeys.NumberOfPlayerGames_Key).ToString();
    }

    private string GetUsedAlgorithms()
    {
        return "A* and Greedy";
    }

    private string GetNumberOfCheckedFields(CurrentAlgorithmType algorithmType)
    {
        if (algorithmType == CurrentAlgorithmType.A_star)
        {
            return PlayerPrefs.GetInt(PrefsKeys.NumberOfFieldsForPlayerOne_Key).ToString() +  " fields";
        }
        else
        {
            return PlayerPrefs.GetInt(PrefsKeys.NumberOfFieldsForPlayerTwo_Key).ToString() + " fields";
        }
    }

    private string GetTimeSpent(string RunnerNo)
    {
        if (RunnerNo == "Runner1")
        {
            return PlayerPrefs.GetFloat(PrefsKeys.TimeForPlayerOne_Key) + " s";
        }
        else
        {
            return PlayerPrefs.GetFloat(PrefsKeys.TimeForlayerTwo_Key) + " s";
        }
    }
}
