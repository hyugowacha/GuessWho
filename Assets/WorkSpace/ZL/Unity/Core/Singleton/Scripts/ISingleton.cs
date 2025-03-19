namespace ZL.Unity
{
    public interface ISingleton<T>

        where T : class, ISingleton<T>
    {
        public static T Instance { get; protected set; } = null;

        protected static bool TrySetInstance(T instance)
        {
            return instance.TrySetInstance();
        }

        bool TrySetInstance()
        {
            if (IsDuplicated() == true)
            {
                return false;
            }

            Instance = (T)this;

            return true;
        }

        bool IsDuplicated()
        {
            if (Instance == null)
            {
                return false;
            }

            FixedDebug.LogWarning($"{typeof(T)} instance is duplicated.");

            return true;
        }

        protected static void Release(T instance)
        {
            if (Instance == instance)
            {
                Instance = null;
            }
        }
    }
}