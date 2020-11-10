using UnityEngine;

public class LoadingController : MonoBehaviour
{
    public GameObject Loader;

    private void OnEnable()
    {
        CustomEvents.showLoaderEvent.AddListener(ShowLoader);
    }

    private void OnDisable()
    {
        CustomEvents.showLoaderEvent.RemoveListener(ShowLoader);
    }

    private void ShowLoader()
    {
        Loader.SetActive(true);
    }
}
