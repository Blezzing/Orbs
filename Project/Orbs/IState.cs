using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
