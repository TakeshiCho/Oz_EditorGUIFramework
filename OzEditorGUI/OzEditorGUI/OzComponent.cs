namespace OzEditorGUI
{ 
    public interface IOzInitor<out T>
    {
        T Init();
    }

    public interface IOzComponent
    {
        string GetName();
        bool CannotOperate();
        void Initialize();
        void DrawGUI();
        void DrawGizoms();
        void Destroy();
    }

    public abstract class OzComponent<TClass> : IOzComponent where TClass : OzComponent<TClass>, new()
    {
        public class OzInitor<T> :IOzInitor<T> where T : OzComponent<T>,new()
        {
            public T Init()
            {
                T component = new T();
                component.Initialize();
                return component;
            }
        }
        
        public static OzInitor<TClass> initor = new OzInitor<TClass>();

        public abstract void Initialize();
        public abstract string GetName();
        public abstract bool CannotOperate();
        public abstract void DrawGUI();
        public abstract void DrawGizoms();
        public abstract void Destroy();
        
    }
}