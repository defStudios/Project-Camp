using UnityEngine;
using GUI;

public class ParticlesProgressbar : ProgressableObject
{
    [SerializeField] private ParticleSystem leftTail;
    [SerializeField] private ParticleSystem rightTail;

    [SerializeField] private Vector2 progressBounds;
    
    public override void SetProgress(float progress, bool instantAnimation = false)
    {
        progress = Mathf.Clamp01(progress);
        
        if (progress == 0)
        {
            leftTail.Stop();
            rightTail.Stop();
            
            return;
        }

        if (!leftTail.isPlaying)
        {
            leftTail.Play();
            rightTail.Play();
        }

        var leftMain = leftTail.main;
        leftMain.startLifetime = Mathf.Lerp(progressBounds.x, progressBounds.y, progress);
        
        var rightMain = rightTail.main;
        rightMain.startLifetime = Mathf.Lerp(progressBounds.x, progressBounds.y, progress);
    }
}
