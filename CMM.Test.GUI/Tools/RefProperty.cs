namespace CMM.Test.GUI.Tools
{
    public class RefProperty<T>
    {
        private RefProperty(T value)
        {
            Value = value;
        }

        public static implicit operator T(RefProperty<T> p) => p.Value;

        public static implicit operator RefProperty<T>(T p) => new RefProperty<T>(p);

        public T Value { get; set; }
    }
}