public class __State__StateContext
{
    public I__State__State CurrentState { get; set; }

    private readonly __State__Controller _controller;

    public __State__StateContext(__State__Controller controller)
    {
        _controller = controller;
    }

    public void Transition()
    {
        CurrentState.Handle(_controller);
    }

    public void Transition(I__State__State state)
    {
        CurrentState = state;
        CurrentState.Handle(_controller);
    }
}
