using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using TMPro;

namespace GUI
{
    public class MessageQueue : MonoBehaviour, IMessageHandler
    {
        private class TextData
        {
            public string Message { get; }
            public float Duration { get; }

            public TextData(string message, float duration) => (Message, Duration) = (message, duration);
        }
        
        [SerializeField] private TMP_Text text;
        [SerializeField] private float fadeDuration;
        
        private readonly Queue<TextData> _messageQueue = new();
        private bool _isShown;

        private void Awake()
        {
            Hide();
        }

        public void ShowMessage(string message, float duration, bool interruptQueue)
        {
            if (interruptQueue)
                _messageQueue.Clear();
            
            _messageQueue.Enqueue(new TextData(message, duration));

            if (!_isShown)
                Show();
            else if (interruptQueue)
                OnMessageDurationExpired();
        }

        private void Show()
        {
            if (_messageQueue.Count == 0)
                return;

            var message = _messageQueue.Dequeue();

            text.DOKill();
            text.text = message.Message;
            text.DOFade(1, message.Duration).OnComplete(OnMessageDurationExpired);

            _isShown = true;
        }

        private void Hide(Action onHid = null)
        {
            text.DOKill();
            var tween = text.DOFade(0, fadeDuration);
            
            if (onHid != null)
                tween.OnComplete(() => onHid.Invoke());

            _isShown = false;
        }

        private void OnMessageDurationExpired()
        {
            Hide(_messageQueue.Count == 0 ? null : Show);
        }
    }
}
