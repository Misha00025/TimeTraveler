using System;

namespace Model
{
    public abstract class Observed<T> where T : class
    {
        protected T _observed { get; private set; }
        private event Action _event;

        public Observed(T observed)
        {
            _observed = observed;
        }

        public void AddListener(Action action)
        {
            _event += action;
        }

        public void RemoveListener(Action action)
        {
            _event -= action;
        }

        protected void Invoke()
        {
            _event?.Invoke();
        }
    }
}
