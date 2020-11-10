using UnityEngine;

public class CameraSetter : MonoBehaviour
{
    void Start()
    {
        Camera.main.orthographicSize = GameManagerData.GetBoardSize() * 1.1f;
        Camera.main.transform.localPosition = new Vector3(GameManagerData.GetBoardSize() / (float)2, Camera.main.transform.localPosition.y, Camera.main.transform.localPosition.z);
    }
}
