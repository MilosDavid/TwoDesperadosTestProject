using UnityEngine;

public class OptionsPopupController : MonoBehaviour
{
    public GameObject OptionsPopupBackground;

    private void OnEnable()
    {
        CustomEvents.showOptionsPopupEvent.AddListener(ShowOptionsPopupDialog);
    }

    private void OnDisable()
    {
        CustomEvents.showOptionsPopupEvent.RemoveListener(ShowOptionsPopupDialog);
    }

    private void ShowOptionsPopupDialog(bool show)
    {
        OptionsPopupBackground.SetActive(show);
    }
}
