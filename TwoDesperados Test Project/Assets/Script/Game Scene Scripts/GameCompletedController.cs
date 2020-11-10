using UnityEngine;

public class GameCompletedController : MonoBehaviour
{
    public GameObject GameCompletedPopoupWindow;
    private void OnEnable()
    {
        CustomEvents.showGameCompletedPopupEvent.AddListener(ShowGameCompletedPopup);
    }

    private void OnDisable()
    {
        CustomEvents.showGameCompletedPopupEvent.RemoveListener(ShowGameCompletedPopup);
    }

    private void ShowGameCompletedPopup()
    {
        GameCompletedPopoupWindow.SetActive(true);
    }
}
