using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSailor.NPCs
{
    interface IInteractable
    {
        IInteraction Interaction { get; }
        void Interact();

    }
}
