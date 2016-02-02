using SFML.Window;

namespace Orbs
{
    public interface IState
    {
        void Update();
        void Render();
        void HandleKeyPressed(KeyEventArgs i);
    }
}
