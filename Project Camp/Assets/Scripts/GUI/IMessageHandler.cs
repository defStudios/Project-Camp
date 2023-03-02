using Core.Services;

namespace GUI
{
    public interface IMessageHandler : ISingleService
    {
        public void ShowMessage(string message, float duration, bool interruptQueue);
    }
}