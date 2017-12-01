namespace KitchenUtensils.Common
{
    /// <summary>
    /// A type for safely avoiding null references
    /// </summary>
    /// <typeparam name="T">The type to contain</typeparam>
    public abstract class Option<T>
    {
        /// <summary>
        /// Indicates that a value has been created
        /// </summary>
        /// <param name="value">Value to store</param>
        /// <returns>A new <see cref="Fulfilled{T}"/> instance containing the value</returns>
        public static Option<T> Fulfill(T value) => new Fulfilled<T>(value);

        /// <summary>
        /// Indicates that value has not been created
        /// </summary>
        /// <returns>A fresh <see cref="Reject{T}"/> to indicate failure</returns>
        public static Reject<T> Reject() => new Reject<T>();
    }

    /// <summary>
    /// Variant of <see cref="Option{T}"/> that indicates a value has not been created
    /// </summary>
    /// <typeparam name="T">Type that was not created</typeparam>
    public class Reject<T>
    {
        /// <summary>
        /// Creates a <see cref="Reject{T}"/>
        /// </summary>
        public Reject()
        {
        }
    }

    /// <summary>
    /// Variant of <see cref="Option{T}"/> that indicates a value was created
    /// </summary>
    /// <typeparam name="T">Type of the value created</typeparam>
    public class Fulfilled<T> : Option<T>
    {
        /// <summary>
        /// Creates a <see cref="Fulfilled{T}"/>
        /// </summary>
        /// <param name="value">Value to store</param>
        public Fulfilled(T value)
        {
            Value = value;
        }

        /// <summary>
        /// Value created when <see cref="Option{T}"/> was fulfilled
        /// </summary>
        public T Value { get; }
    }
}
