namespace OzEditorGUI
{ 
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
        public static TClass Init(bool drawGUI)
        {
            TClass component = new TClass();
            component._drawGUI = drawGUI;
            component.Initialize();
            return component;
        }
        
        private bool _drawGUI;

        public abstract void Initialize();
        public abstract string GetName();
        public abstract bool CannotOperate();

        public void DrawGUI()
        {
            if (_drawGUI)
            {
                OnDrawGUI();
            }
        }
        
        public abstract void OnDrawGUI();

        public void DrawGizoms()
        {
            if (OnDrawGizoms())
            {
                
            }
        }
        public virtual bool OnDrawGizoms()
        {
            return false;
        }
        public abstract void Destroy();
        
    }
}
