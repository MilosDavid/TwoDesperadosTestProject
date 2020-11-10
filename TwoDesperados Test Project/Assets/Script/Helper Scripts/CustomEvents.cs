using System.Collections.Generic;
using UnityEngine.Events;

public class CustomEvents
{
    public class ShowLoaderEvent : UnityEvent { }
    public static ShowLoaderEvent showLoaderEvent = new ShowLoaderEvent();

    public class ShowWarningDialogEvent : UnityEvent<string> { }
    public static ShowWarningDialogEvent showWarningDialogEvent = new ShowWarningDialogEvent();

    public class ShowOptionsPopupEvent : UnityEvent<bool> { }
    public static ShowOptionsPopupEvent showOptionsPopupEvent = new ShowOptionsPopupEvent();

    public class ShowGameCompletedPopupEvent : UnityEvent { }
    public static ShowGameCompletedPopupEvent showGameCompletedPopupEvent = new ShowGameCompletedPopupEvent();

    public class ShowResultsPopupEvent : UnityEvent<bool> { }
    public static ShowResultsPopupEvent showResultsPopupEvent = new ShowResultsPopupEvent();

    public class SetGameDataEvent : UnityEvent { }
    public static SetGameDataEvent setGameDataEvent = new SetGameDataEvent();

    public class CreateTableEvent : UnityEvent { }
    public static CreateTableEvent createTableEvent = new CreateTableEvent();

    public class StartPathSearchEvent : UnityEvent<List<Node>, string> { }
    public static StartPathSearchEvent startPathSearchEvent = new StartPathSearchEvent();

    public class ColorPathEvent : UnityEvent<string, Node, List<Node>> { }
    public static ColorPathEvent colorPathEvent = new ColorPathEvent();

    public class RaceFinishedEvent : UnityEvent<string, bool> { }
    public static RaceFinishedEvent raceFinishedEvent = new RaceFinishedEvent();
}
