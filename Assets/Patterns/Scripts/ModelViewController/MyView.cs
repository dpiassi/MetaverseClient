using Patterns.MVC;

public class MyView : View
{
    /* ============================================================
     * ABSTRACT FUNCTIONS
     * ============================================================*/
    internal override void SetupUserInterface(Model model)
    {
        throw new System.NotImplementedException();
    }

    internal override void UpdateUserInterface(Model model)
    {
        throw new System.NotImplementedException();
    }

    /* ============================================================
     * CUSTOM EVENTS
     * ============================================================*/
    // TODO Implement here your own custom events and trig them.
    // TODO Don't forget to listen to your View events in the Controller script.
}
