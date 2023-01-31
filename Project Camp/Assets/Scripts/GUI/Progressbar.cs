using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

namespace GUI
{
    public class Progressbar : ProgressableObject
    {
        [SerializeField] private Image bar;
        [SerializeField] private float animationDuration;

        public override void SetProgress(float progress, bool instant = false)
        {
            progress = Mathf.Clamp01(progress);
            DOTween.To(() => bar.fillAmount, x => bar.fillAmount = x, progress, instant ? 0 : animationDuration);
        }
    }
}
