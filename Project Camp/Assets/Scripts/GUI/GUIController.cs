using UnityEngine;

namespace GUI
{
    public class GUIController : MonoBehaviour
    {
        [SerializeField] private ProgressableObject progressbar;

        private Core.Player player;
        private Environment.LevelEnvironment level;

        public void Init(Core.Player player, Environment.LevelEnvironment level)
        {
            this.player = player;
            this.level = level;

            progressbar.SetProgress(0, true);

            player.Inventory.InventoryStateChanged += UpdatePlayerProgress;
        }

        private void UpdatePlayerProgress()
        {
            progressbar.SetProgress(player.Inventory.GetArtifactsCount() / (float)level.ArtifactsTotalCount);
        }
    }
}
