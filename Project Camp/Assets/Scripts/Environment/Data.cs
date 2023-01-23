using System.Collections.Generic;
using UnityEngine;

namespace Environment
{
    [CreateAssetMenu(fileName = "Environment", menuName = "Envoronment/New Preferences", order = 0)]
    public class Data : ScriptableObject
    {
        [System.Serializable]
        public class Settings
        {
            [field: SerializeField] public float LightIntensity { get; private set; }
            [field: SerializeField] public float AtmosphereThickness { get; private set; }
            [field: SerializeField] public float Exposure { get; private set; }

        }

        [field: SerializeField] public Settings DayTime { get; private set; }
        [field: SerializeField] public Settings NightTime { get; private set; }
    }
}
