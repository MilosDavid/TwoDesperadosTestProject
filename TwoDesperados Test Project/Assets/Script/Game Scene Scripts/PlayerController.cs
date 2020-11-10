using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AlgorithmType;

public class PlayerController : MonoBehaviour
{
    public CurrentAlgorithmType playerAlgorithmType;

    private void OnEnable()
    {
        CustomEvents.startPathSearchEvent.AddListener(StartPathfinding);
    }

    private void OnDisable()
    {
        CustomEvents.startPathSearchEvent.RemoveListener(StartPathfinding);
    }

    string thisPlayerName = "";
    private void StartPathfinding(List<Node> path, string playerName)
    {
        if (this.transform.name == playerName)
        {
            thisPlayerName = playerName;
            StartCoroutine(StartWalk(path));
        }
    }

    IEnumerator StartWalk(List<Node> pathNodes)
    {
        foreach (var node in pathNodes)
        {
            var localPosition = GetWorldPosition(node.coorX, node.coorY) + new Vector3(1f, 1f) * 0.5f;

            CustomEvents.colorPathEvent.Invoke(this.transform.name, node, pathNodes);

            LeanTween.move(gameObject, localPosition, Time.deltaTime);

            yield return new WaitForSeconds(0.5f);
        }

        CustomEvents.raceFinishedEvent.Invoke(thisPlayerName, true);
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * 1f;
    }
}
