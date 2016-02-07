using SFML.Window;

namespace Orbs
{
    public abstract class State
    {
        public virtual void Update() { }
        public virtual void Render() { }
        public virtual void HandleKeyPressed(KeyEventArgs i) { }
    }
}
