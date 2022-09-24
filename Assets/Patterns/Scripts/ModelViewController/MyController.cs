using Patterns.MVC;

public class MyController : Controller
{
    /* ============================================================
     * GETTERS AND SETTERS
     * ============================================================*/
    // TODO Pull data from Model via getters.
    // TODO Push data to Model via setters.

    /* ============================================================
     * OVERRIDE ABSTRACT FUNCTIONS
     * ============================================================*/
    protected override void Setup(ref Model model, View view)
    {
        throw new System.NotImplementedException();
    }

    protected override void ReadInputsAndUpdateModel(ref Model model)
    {
        throw new System.NotImplementedException();
    }

    protected override void SubscribeToViewEvents(View view)
    {
        throw new System.NotImplementedException();
    }

    protected override void UnsubscribeFromViewEvents(View view)
    {
        throw new System.NotImplementedException();
    }
}
