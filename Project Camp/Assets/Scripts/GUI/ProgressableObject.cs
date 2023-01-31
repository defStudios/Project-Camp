using UnityEngine;

namespace GUI
{
    public abstract class ProgressableObject : MonoBehaviour
    {
        public abstract void SetProgress(float progress, bool instantAnimation = false);
    }
}