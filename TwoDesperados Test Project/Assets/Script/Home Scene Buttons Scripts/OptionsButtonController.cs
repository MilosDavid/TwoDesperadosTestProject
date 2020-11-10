using UnityEngine;
using UnityEngine.UI;

public class OptionsButtonController : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ShowOptionsPopup);
    }

    private void ShowOptionsPopup()
    {
        CustomEvents.showOptionsPopupEvent.Invoke(true);
    }
}
