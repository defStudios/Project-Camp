using UnityEngine;

namespace GUI
{
    public class GUIController : MonoBehaviour
    {
        [SerializeField] private Progressbar progressbar;

        private Core.Player player;
        private Environment.LevelEnvironment level;

        public void Init(Core.Player player, Environment.LevelEnvironment level)
        {
            this.player = player;
            this.level = level;

            progressbar.SetProgress(0, true);

            player.OnArtifactGot += UpdatePlayerProgress;
        }

        private void UpdatePlayerProgress()
        {
            progressbar.SetProgress(player.ArtifactsCount / (float)level.ArtifactsTotalCount);
        }
    }
}
