using System;



public interface IEventBinding<T>
{
    Action<T> OnEventArgs { get; set; }
    Action OnEventNoArgs { get; set; }
}
public class EventBinding<T> : IEventBinding<T> where T : IEvent
{
    Action<T> onEventArgs = _ => { };
    Action onEventNoArgs = () => { };
    public Action<T> OnEventArgs
    {
        get
        {
            return onEventArgs;
        }
        set
        {
            onEventArgs = value;
        }
    }
    public Action OnEventNoArgs
    {
        get
        {
            return onEventNoArgs;
        }
        set
        {
            onEventNoArgs = value;
        }
    }

    public EventBinding(Action<T> @event) => this.onEventArgs = @event;
    public EventBinding(Action @event) => this.onEventNoArgs = @event;

    public void Add(Action<T> @event) => this.onEventArgs += @event;
    public void Remove(Action<T> @event) => this.onEventArgs -= @event;
    public void Add(Action @event) => this.onEventNoArgs += @event;
    public void Remove(Action @event) => this.onEventNoArgs -= @event;

}


