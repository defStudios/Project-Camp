using System.Collections;
using UnityEngine;
using Core;

namespace Environment
{
    public class LevelEnvironment : MonoBehaviour
    {
        public int ArtifactsTotalCount { get; private set; }

        [SerializeField] private Data data;

        [Space]
        [SerializeField] private Light light;
        [SerializeField] private Material skyboxMaterial;
        
        [Space]
        [SerializeField] private GameObject[] artifacts;
        [SerializeField] private Lighthouse lighthouse;

        private Player player;
        private Coroutine transition;
        private Coroutine rotator;
        private static readonly int Rotation = Shader.PropertyToID("_Rotation");
        private static readonly int Exposure = Shader.PropertyToID("_Exposure");

        public void Init(Player player)
        {
            this.player = player;
            player.Inventory.InventoryStateChanged += PlayerGotElement;

            ArtifactsTotalCount = artifacts.Length;

            DisableLighthouse();
            SetNight(true);

            rotator = StartCoroutine(SkyboxRotator());
        }

        private void OnDestroy()
        {
            player.Inventory.InventoryStateChanged -= PlayerGotElement;
        }

        public void DisableLighthouse()
        {
            lighthouse.TurnOnLights();
        }

        public void SetNight(bool instant = false)
        {
            if (transition != null)
                StopCoroutine(transition);

            transition = StartCoroutine(ApplyData(data.NightTime, instant ? 0 : data.TransitionDuration));
        }

        public void EnableLighthouse()
        {
            lighthouse.TurnOnLights();
        }

        public void SetDay(bool instant = false)
        {
            if (transition != null)
                StopCoroutine(transition);

            transition = StartCoroutine(ApplyData(data.DayTime, instant ? 0 : data.TransitionDuration));
        }

        private void PlayerGotElement()
        {
            if (player.Inventory.GetArtifactsCount() == ArtifactsTotalCount)
            {
                SetDay();
                EnableLighthouse();
            }
        }

        private IEnumerator SkyboxRotator()
        {
            while (true)
            {
                float currentRot = skyboxMaterial.GetFloat(Rotation);
                skyboxMaterial.SetFloat(Rotation,currentRot + data.SyboxRotationSpeed * Time.deltaTime);
                yield return null;
            }
        }

        private IEnumerator ApplyData(Data.Settings data, float duration)
        {
            float t = 0;

            if (duration == 0)
            {
                light.intensity = data.LightIntensity;
                skyboxMaterial.SetFloat(Exposure,  data.Exposure);

                yield break;
            }

            float startIntensity = light.intensity;
            float exposure = skyboxMaterial.GetFloat(Exposure);

            while (t < duration)
            {
                light.intensity = Mathf.Lerp(startIntensity, data.LightIntensity, t / duration);
                skyboxMaterial.SetFloat(Exposure, Mathf.Lerp(exposure, data.Exposure, t / duration));

                t += Time.deltaTime;
                yield return null;
            }
        }
    }
}
