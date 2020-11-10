using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButtonController : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(LoadGameScene);
    }

    private void LoadGameScene()
    {
        CustomEvents.showLoaderEvent.Invoke();
        SceneManager.LoadScene(1);
    }
}
