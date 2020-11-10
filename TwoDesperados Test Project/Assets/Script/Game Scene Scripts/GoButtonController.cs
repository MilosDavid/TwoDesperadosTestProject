using System;
using UnityEngine;
using UnityEngine.UI;

public class GoButtonController : MonoBehaviour
{
    public GridGenerator gridGenerator;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(StartPathfinding);
    }

    private void StartPathfinding()
    {
        int numberOfPlayedGames = 1;
        if (PlayerPrefs.HasKey(PrefsKeys.NumberOfPlayerGames_Key))
        {
            numberOfPlayedGames = PlayerPrefs.GetInt(PrefsKeys.NumberOfPlayerGames_Key, 1);
            numberOfPlayedGames++;
        }
        else
        {
            numberOfPlayedGames++;
        }

        PlayerPrefs.SetInt(PrefsKeys.NumberOfPlayerGames_Key, numberOfPlayedGames);
        PlayerPrefs.Save();

        var player1Path = gridGenerator.GetComponent<FindPathAlgorithm>().FindPath(gridGenerator.nodeList, gridGenerator.StartNode, gridGenerator.EndNode, AlgorithmType.CurrentAlgorithmType.A_star);
        CustomEvents.startPathSearchEvent.Invoke(player1Path, "Player1");
        var player2Path = gridGenerator.GetComponent<FindPathAlgorithm>().FindPath(gridGenerator.nodeList, gridGenerator.StartNode, gridGenerator.EndNode, AlgorithmType.CurrentAlgorithmType.Greedy_suboptimal);
        CustomEvents.startPathSearchEvent.Invoke(player2Path, "Player2");

        SaveNumberOfFieldsForEvryPlayer(player1Path.Count, player2Path.Count);

        foreach (var node in player1Path)
        {
            Debug.Log("<color=red>" + node.name + "</color>");
        }

        foreach (var node in player2Path)
        {
            Debug.Log("<color=cyan>" + node.name + "</color>");
        }
    }

    private void SaveNumberOfFieldsForEvryPlayer(int player1Path, int player2Path)
    {
        PlayerPrefs.SetInt(PrefsKeys.NumberOfFieldsForPlayerOne_Key, player1Path);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt(PrefsKeys.NumberOfFieldsForPlayerTwo_Key, player2Path);
        PlayerPrefs.Save();
    }
}
