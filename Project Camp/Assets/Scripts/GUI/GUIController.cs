using Core.Services;
using UnityEngine;

namespace GUI
{
    public class GUIController : MonoBehaviour
    {
        [SerializeField] private ProgressableObject progressbar;
        [SerializeField] private MessageQueue message;
        
        private Core.Player player;
        private Environment.LevelEnvironment level;

        public void Init(Core.Player player, Environment.LevelEnvironment level)
        {
            this.player = player;
            this.level = level;

            progressbar.SetProgress(0, true);
            
            ServiceManager.Container.Register<IMessageHandler>(message);

            player.Inventory.InventoryStateChanged += UpdatePlayerProgress;
        }
        
        private void UpdatePlayerProgress()
        {
            progressbar.SetProgress(player.Inventory.GetArtifactsCount() / (float)level.ArtifactsTotalCount);
        }
    }
}
