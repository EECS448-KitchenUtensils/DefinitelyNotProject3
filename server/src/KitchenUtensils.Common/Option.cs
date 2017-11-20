namespace KitchenUtensils.Common
{
    public abstract class Option<T>
    {
        public static Option<T> Fulfill(T value) => new Fulfilled<T>(value);
        public static Reject<T> Reject() => new Reject<T>();
    }

    public class Reject<T>
    {
        public Reject()
        {
        }
    }

    public class Fulfilled<T> : Option<T>
    {
        public Fulfilled(T value)
        {
            Value = value;
        }
        public T Value { get; }
    }
}
