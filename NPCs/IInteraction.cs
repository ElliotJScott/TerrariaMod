using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSailor.NPCs
{
    public interface IInteraction
    {
        bool Enabled { get; set; }
        void Update();
        void Execute();
    }
}
