using UnityEngine;
using UnityEngine.UI;

public class EntryChecker : MonoBehaviour
{
    public void CheckSign()
    {
        if (GetComponent<InputField>().text.Length > 0 && GetComponent<InputField>().text[0] == '-')
        {
            CustomEvents.showWarningDialogEvent.Invoke("Please enter the number greater than zero!");
            GetComponent<InputField>().text = GetComponent<InputField>().text.Remove(0, 1);
        }
    }

    public void CheckBoardSize()
    {
        if (GetComponent<InputField>().text.Length > 0 && int.Parse(GetComponent<InputField>().text.ToString()) > Constants.MaxNumberOfBoardSize)
        {
            CustomEvents.showWarningDialogEvent.Invoke("Max board size is: " + Constants.MaxNumberOfBoardSize);
            GetComponent<InputField>().text = GetComponent<InputField>().text.Remove(0);
        }
    }

    public void CheckNumberOfObstacles()
    {
        var maxNumberOfObstacles = GameManagerData.GetBoardSize() * GameManagerData.GetBoardSize() / 2;

        if (GetComponent<InputField>().text.Length > 0 && int.Parse(GetComponent<InputField>().text.ToString()) > maxNumberOfObstacles)
        {
            CustomEvents.showWarningDialogEvent.Invoke("Number of obstacles can't be larger than: " + maxNumberOfObstacles); //half of the board size!
            GetComponent<InputField>().text = GetComponent<InputField>().text.Remove(0);
        }
    }

    public void CheckStartPointXY()
    {
        if (GetComponent<InputField>().text.Length > 0 && GetComponent<InputField>().text[0] != '-')
        {
            if (int.Parse(GetComponent<InputField>().text.ToString()) > GameManagerData.GetBoardSize()-1)
            {
                CustomEvents.showWarningDialogEvent.Invoke("Start point can't be bigger or same as a board size!");
                if (GetComponent<InputField>().text.Length > 0)
                {
                    GetComponent<InputField>().text = GetComponent<InputField>().text.Remove(0);
                }
            }
        }
    }

    public void CheckEndPointXY()
    {
        if (GetComponent<InputField>().text.Length > 0 && GetComponent<InputField>().text[0] != '-')
        {
            if (int.Parse(GetComponent<InputField>().text.ToString()) > GameManagerData.GetBoardSize() - 1)
            {
                CustomEvents.showWarningDialogEvent.Invoke("End point can't be bigger or same as a board size!");
                if (GetComponent<InputField>().text.Length > 0)
                {
                    GetComponent<InputField>().text = GetComponent<InputField>().text.Remove(0);
                }
            }
        }
    }

    public static bool CheckIfStartAndEndPointAreNotTheSame()
    {
        return GameManagerData.GetStartPointX() == GameManagerData.GetEndPointX()
            &&
            GameManagerData.GetStartPointY() == GameManagerData.GetEndPointY();
    }
}
