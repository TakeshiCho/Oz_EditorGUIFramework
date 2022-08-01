#if UNITY_EDITOR
using UnityEngine;

namespace OzEditorGUIRuntime
{
    public class OzGizmosComponent : MonoBehaviour
    {
        public DrawGizmos drawGizmos = () => {};
        private void OnDrawGizmos()
        {
            drawGizmos();
        }

        public void DestroySelf()
        {
            if (!Application.isPlaying)
            {
                DestroyImmediate(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
    
    public delegate void DrawGizmos();
}
#endif