using UnityEngine;

public class GameManagerData 
{
    public static void SetBoardSize(int size)
    {
        PlayerPrefs.SetInt(PrefsKeys.BoardSizeKey, size);
        PlayerPrefs.Save();
    }

    public static int GetBoardSize()
    {
        return PlayerPrefs.GetInt(PrefsKeys.BoardSizeKey, DefaultGameValues.BoardSize);
    }

    public static void SetNumberOfObstacles(int numberOfObstacles)
    {
        PlayerPrefs.SetInt(PrefsKeys.NumberOfObstaclesKey, numberOfObstacles);
        PlayerPrefs.Save();
    }

    public static int GetNumberOfObstacles()
    {
        return PlayerPrefs.GetInt(PrefsKeys.NumberOfObstaclesKey, DefaultGameValues.NumOfObstacles);
    }

    public static void SetStartPointX(int point)
    {
        PlayerPrefs.SetInt(PrefsKeys.StartPointX_Key, point);
        PlayerPrefs.Save();
    }

    public static int GetStartPointX()
    {
        return PlayerPrefs.GetInt(PrefsKeys.StartPointX_Key, DefaultGameValues.StartPosX);
    }

    public static void SetStartPointY(int point)
    {
        PlayerPrefs.SetInt(PrefsKeys.StartPointY_Key, point);
        PlayerPrefs.Save();
    }

    public static int GetStartPointY()
    {
        return PlayerPrefs.GetInt(PrefsKeys.StartPointY_Key, DefaultGameValues.StartPosY);
    }

    public static void SetEndPointX(int point)
    {
        PlayerPrefs.SetInt(PrefsKeys.EndPointX_Key, point);
        PlayerPrefs.Save();
    }

    public static int GetEndPointX()
    {
        return PlayerPrefs.GetInt(PrefsKeys.EndPointX_Key, DefaultGameValues.EndPosX);
    }

    public static void SetEndPointY(int point)
    {
        PlayerPrefs.SetInt(PrefsKeys.EndPointY_Key, point);
        PlayerPrefs.Save();
    }

    public static int GetEndPointY()
    {
        return PlayerPrefs.GetInt(PrefsKeys.EndPointY_Key, DefaultGameValues.EndPosY);
    }
}
