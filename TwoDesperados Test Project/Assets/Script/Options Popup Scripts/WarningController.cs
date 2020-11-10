using UnityEngine;
using UnityEngine.UI;

public class WarningController : MonoBehaviour
{
    public GameObject WarningDialog;

    public Button OkButton;
    public Text ErrorText;

    private void OnEnable()
    {
        CustomEvents.showWarningDialogEvent.AddListener(ShowWarningDialog);
    }

    private void OnDisable()
    {
        CustomEvents.showWarningDialogEvent.RemoveListener(ShowWarningDialog);
    }

    private void Start()
    {
        OkButton.onClick.AddListener(CloseDialog);
    }

    private void ShowWarningDialog(string errorText)
    {
        ErrorText.text = errorText;
        WarningDialog.SetActive(true);
    }

    private void CloseDialog()
    {
        WarningDialog.SetActive(false);
    }
}
