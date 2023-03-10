using System.Collections;
using UnityEngine;
using Core;
using DG.Tweening;

namespace Environment
{
    public class LevelEnvironment : MonoBehaviour
    {
        public int ArtifactsTotalCount { get; private set; }

        [SerializeField] private Data data;

        [Space, SerializeField] private Light light;
        [SerializeField] private Material skyboxMaterial;
        
        [Space, SerializeField] private GameObject[] artifacts;
        [SerializeField] private Lighthouse lighthouse;

        [Space, SerializeField] private GameObject levelFinishMarker;
        [SerializeField] private float initTimeout;
        [SerializeField] private float cameraBlendDuration;
        [SerializeField] private float playerInDuration;
        [SerializeField] private float playerOutDuration;
        [SerializeField] private float playerLiftUpDuration;
        [SerializeField] private float playerLiftDownDuration;
        
        private Player _player;
        private Coroutine _transition;
        private Coroutine _rotator;
        
        private static readonly int Rotation = Shader.PropertyToID("_Rotation");
        private static readonly int Exposure = Shader.PropertyToID("_Exposure");

        public void Init(Player player)
        {
            _player = player;
            player.Inventory.InventoryStateChanged += PlayerGotElement;

            ArtifactsTotalCount = artifacts.Length;

            DisableLighthouse();
            SetNight(true);

            _rotator = StartCoroutine(SkyboxRotator());

            StartLevel();
        }

        private void OnDestroy()
        {
            _player.Inventory.InventoryStateChanged -= PlayerGotElement;
        }

        public void DisableLighthouse()
        {
            lighthouse.TurnOnLights();
        }

        public void SetNight(bool instant = false)
        {
            if (_transition != null)
                StopCoroutine(_transition);

            _transition = StartCoroutine(ApplyData(data.NightTime, instant ? 0 : data.TransitionDuration));
        }

        public void EnableLighthouse()
        {
            SetDay();
            //lighthouse.TurnOnLights();
        }

        public void SetDay(bool instant = false)
        {
            if (_transition != null)
                StopCoroutine(_transition);

            _transition = StartCoroutine(ApplyData(data.DayTime, instant ? 0 : data.TransitionDuration));
        }

        public void OnFinishMarkerTriggerEnter(Collider other)
        {
            bool isPlayer = other.TryGetComponent<Player>(out var player);
            if (!isPlayer)
                return;
            
            CompleteLevel();
        }

        private void StartLevel()
        {
            levelFinishMarker.SetActive(false);

            var sequence = DOTween.Sequence();
            sequence
                .AppendInterval(initTimeout)
                .AppendCallback(() =>
                {
                    lighthouse.LiftDown(playerLiftDownDuration, Bootstrap.Game.Player.transform);
                })
                .AppendInterval(playerLiftDownDuration + .5f)
                .AppendCallback(() =>
                {
                    Bootstrap.Game.Player.transform.DOMove(levelFinishMarker.transform.position, playerOutDuration);
                })
                .AppendInterval(playerOutDuration + .5f)
                .AppendCallback(() =>
                {
                    Bootstrap.Game.PlayerCamera.Enable();
                })
                .AppendInterval(cameraBlendDuration + .5f)
                .AppendCallback(() =>
                {
                    Bootstrap.Game.Player.EnableMovement();
                });
        }

        private void CompleteLevel()
        {
            levelFinishMarker.SetActive(false);

            Bootstrap.Game.PlayerCamera.Disable();
            Bootstrap.Game.Player.DisableMovement();
            
            Bootstrap.Game.Player.transform.position = levelFinishMarker.transform.position;

            var sequence = DOTween.Sequence();
            sequence
                .AppendInterval(cameraBlendDuration)
                .AppendCallback(() =>
                {
                    Bootstrap.Game.Player.transform.DOMove(lighthouse.LiftPlayerPosition.position, playerInDuration);
                })
                .AppendInterval(playerInDuration + .5f)
                .AppendCallback(() =>
                {
                    lighthouse.LiftUp(playerLiftUpDuration, Bootstrap.Game.Player.transform);
                })
                .AppendInterval(playerLiftUpDuration + .5f)
                .AppendCallback(() =>
                {
                    EnableLighthouse();
                });
        }

        private void PlayerGotElement()
        {
            if (_player.Inventory.GetArtifactsCount() == ArtifactsTotalCount)
                AllowLevelCompletion();
        }

        private void AllowLevelCompletion()
        {
            levelFinishMarker.SetActive(true);
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
