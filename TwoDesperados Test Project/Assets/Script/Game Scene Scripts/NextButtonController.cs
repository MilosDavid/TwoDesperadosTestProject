using UnityEngine;
using UnityEngine.UI;

public class NextButtonController : MonoBehaviour
{
    /*    Clicking on the “Next” button will trigger the creation of a new board 
          with randomly generated obstacles (one more obstacle than the previous level), 
          recreate the runners, and enable the user to click “Go” and start a new run.
    */

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(GenerateNewGame);
    }

    private void GenerateNewGame()
    {
        var maxNumberOfObstacles = GameManagerData.GetBoardSize() * GameManagerData.GetBoardSize() / 2;

        int newNumberOfObstacles = GameManagerData.GetNumberOfObstacles() + 1;

        if (maxNumberOfObstacles < newNumberOfObstacles)
        {
            CustomEvents.showWarningDialogEvent.Invoke("Number of obstacles can't be larger than: " + maxNumberOfObstacles); //half of the board size!
        }
        else
        {
            GameManagerData.SetNumberOfObstacles(newNumberOfObstacles);
            CustomEvents.createTableEvent.Invoke();
        }
    }
}

