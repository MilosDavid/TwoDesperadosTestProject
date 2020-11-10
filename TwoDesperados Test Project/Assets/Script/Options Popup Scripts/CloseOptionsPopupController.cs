using UnityEngine;
using UnityEngine.UI;

public class CloseOptionsPopupController : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(CloseOptionsDialog);
    }

    private void CloseOptionsDialog()
    {
        CustomEvents.showOptionsPopupEvent.Invoke(false);
    }

}
