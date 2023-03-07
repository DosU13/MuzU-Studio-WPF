using MuzUStandard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzU_Studio.model
{
    public class ProjectModel : IProjectModel
    {
        public MuzUProject MuzUProject { get; }

        public ProjectModel(MuzUProject muzUProject)
        {
            MuzUProject = muzUProject;
        }
    }
}
