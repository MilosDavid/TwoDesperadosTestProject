using UnityEngine;
using UnityEngine.UI;

public class SaveButtonController : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(SaveGameData);
    }

    private void SaveGameData()
    {
        if (EntryChecker.CheckIfStartAndEndPointAreNotTheSame())
        {
            CustomEvents.showWarningDialogEvent.Invoke("Start and End point can't be the same!");
        }
        else
        {
            CustomEvents.setGameDataEvent.Invoke();
            CustomEvents.showOptionsPopupEvent.Invoke(false);
        }
    }
}
