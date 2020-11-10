using UnityEngine;
using UnityEngine.UI;

public class InputFieldController : MonoBehaviour
{
    private void OnEnable()
    {
        CustomEvents.setGameDataEvent.AddListener(SetInputValue);
    }

    private void OnDisable()
    {
        CustomEvents.setGameDataEvent.RemoveListener(SetInputValue);
    }

    void SetInputValue()
    {
        var fieldType = GetComponent<InputFieldTyperController>().fieldType;

        if (fieldType == InputFieldEnums.InputFieldsTypes.BoardInputField)
        {
            int value = (!string.IsNullOrEmpty(GetComponent<InputField>().text))
                ? int.Parse(GetComponent<InputField>().text)
                : GameManagerData.GetBoardSize();

            GameManagerData.SetBoardSize(value);
        }
        else if (fieldType == InputFieldEnums.InputFieldsTypes.ObstaclesInputField)
        {
            int value = (!string.IsNullOrEmpty(GetComponent<InputField>().text))
                ? int.Parse(GetComponent<InputField>().text)
                : GameManagerData.GetNumberOfObstacles();

            GameManagerData.SetNumberOfObstacles(value);
        }
        else if (fieldType == InputFieldEnums.InputFieldsTypes.StartPosXInputField)
        {
            int value = (!string.IsNullOrEmpty(GetComponent<InputField>().text))
                ? int.Parse(GetComponent<InputField>().text) 
                : GameManagerData.GetStartPointX();

            GameManagerData.SetStartPointX(value);
        }
        else if (fieldType == InputFieldEnums.InputFieldsTypes.StartPosYInputField)
        {
            int value = (!string.IsNullOrEmpty(GetComponent<InputField>().text))
                ? int.Parse(GetComponent<InputField>().text)
                : GameManagerData.GetStartPointY();

            GameManagerData.SetStartPointY(value);
        }
        else if (fieldType == InputFieldEnums.InputFieldsTypes.EndPosXInputField)
        {
            int value = (!string.IsNullOrEmpty(GetComponent<InputField>().text))
                ? int.Parse(GetComponent<InputField>().text)
                : GameManagerData.GetEndPointX();

            GameManagerData.SetEndPointX(value);
        }
        else if (fieldType == InputFieldEnums.InputFieldsTypes.EndPosYInputField)
        {
            int value = (!string.IsNullOrEmpty(GetComponent<InputField>().text))
                ? int.Parse(GetComponent<InputField>().text)
                : GameManagerData.GetEndPointY();

            GameManagerData.SetEndPointY(value);
        }
    }
}
