using GUI;
using UnityEngine;

namespace Collectibles
{
    public class ArtifactSymbol : ProgressableObject
    {
        [SerializeField] private Renderer[] renderers;

        [Space, SerializeField] private Vector2 highlightRange; 
    
        private MaterialPropertyBlock[] _materialPropertyBlocks;
    
        private static readonly int DecalEmissionIntensity = Shader.PropertyToID("_DecalEmissionIntensity");

        private void Start()
        {
            _materialPropertyBlocks = new MaterialPropertyBlock[renderers.Length];

            for (int i = 0; i < renderers.Length; i++)
                _materialPropertyBlocks[i] = new MaterialPropertyBlock();
        }

        public override void SetProgress(float progress, bool instantAnimation = false)
        {
            for (int i = 0; i < _materialPropertyBlocks.Length; i++)
            {
                _materialPropertyBlocks[i].SetFloat(DecalEmissionIntensity,
                        Mathf.Lerp(highlightRange.x, highlightRange.y, 1 - progress));
                
                renderers[i].SetPropertyBlock(_materialPropertyBlocks[i]);
            }
        }
    }
}
