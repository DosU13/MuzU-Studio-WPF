using MuzUStandard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzU_Studio.model
{
    internal class EmptyProjectModel : IProjectModel
    {
        public MuzUProject MuzUProject { get; } = new MuzUProject();
    }
}
