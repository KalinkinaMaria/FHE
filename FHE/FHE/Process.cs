using FHE.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FHE
{
    class Process
    {
        private List<HierarchyGoal> GoalsView;

        public Process(List<HierarchyGoal> Goals)
        {
            this.GoalsView = Goals;
        }

        public void StartCalculation()
        {
            List<Node> GoalsModel = ConvertModel.FromViewToModel(GoalsView);



            foreach (Goal goal in GoalsModel)
            {
                goal.calcMembershipFunc();
            }

        }
    }
}
