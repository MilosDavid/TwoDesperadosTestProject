using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeButtonController : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(GoToHomeScreen); 
    }

    private void GoToHomeScreen()
    {
        SceneManager.LoadScene(0);
    }
}
