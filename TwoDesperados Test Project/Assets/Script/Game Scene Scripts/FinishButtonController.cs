using UnityEngine;
using UnityEngine.UI;

public class FinishButtonController : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ShowResults);
    }

    private void ShowResults()
    {
        CustomEvents.showResultsPopupEvent.Invoke(true);
    }
}
