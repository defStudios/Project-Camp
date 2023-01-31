using System.Collections;
using UnityEngine;
using Core;

namespace Environment
{
    public class LevelEnvironment : MonoBehaviour
    {
        public int ArtifactsTotalCount { get; private set; }

        [SerializeField] private float transitionDuration;
        [SerializeField] private Data data;

        [Space]
        [SerializeField] private Light light;
        [SerializeField] private Material skyboxMaterial;
        
        [Space]
        [SerializeField] private GameObject[] artifacts;
        [SerializeField] private Lighthouse lighthouse;

        private Player player;
        private Coroutine transition;

        public void Init(Player player)
        {
            this.player = player;
            player.Inventory.InventoryStateChanged += PlayerGotElement;

            ArtifactsTotalCount = artifacts.Length;

            DisableLighthouse();
            SetNight(true);
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

            transition = StartCoroutine(ApplyData(data.NightTime, instant ? 0 : transitionDuration));
        }

        public void EnableLighthouse()
        {
            lighthouse.TurnOnLights();
        }

        public void SetDay(bool instant = false)
        {
            if (transition != null)
                StopCoroutine(transition);

            transition = StartCoroutine(ApplyData(data.DayTime, instant ? 0 : transitionDuration));
        }

        private void PlayerGotElement()
        {
            if (player.Inventory.GetArtifactsCount() == ArtifactsTotalCount)
            {
                SetDay();
                EnableLighthouse();
            }
        }

        private IEnumerator ApplyData(Data.Settings data, float duration)
        {
            float t = 0;

            if (duration == 0)
            {
                light.intensity = data.LightIntensity;
                skyboxMaterial.SetFloat("_AtmosphereThickness", data.AtmosphereThickness);
                skyboxMaterial.SetFloat("_Exposure",  data.Exposure);

                yield break;
            }

            float startIntensity = light.intensity;
            float thickness = skyboxMaterial.GetFloat("_AtmosphereThickness");
            float exposure = skyboxMaterial.GetFloat("_Exposure");

            while (t < duration)
            {
                light.intensity = Mathf.Lerp(startIntensity, data.LightIntensity, t / duration);
                skyboxMaterial.SetFloat("_AtmosphereThickness", Mathf.Lerp(thickness, data.AtmosphereThickness, t / duration));
                skyboxMaterial.SetFloat("_Exposure", Mathf.Lerp(exposure, data.Exposure, t / duration));

                t += Time.deltaTime;
                yield return null;
            }
        }
    }
}
