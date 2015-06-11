﻿using FHE.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace FHE
{
    class Process
    {
        private List<HierarchyGoal> GoalsView;
        private List<Goal> GoalsModel;

        public Process(List<HierarchyGoal> Goals)
        {
            this.GoalsView = Goals;
        }

        public void StartCalculation()
        {
            GoalsModel = ConvertModel.FromViewToModel(GoalsView);

            foreach (Goal goal in GoalsModel)
            {
                goal.calcMembershipFunc();
            }

        }

        public List<Point> GetResultMembershipFunction(int index)
        {
            List<Point> result = new List<Point>();

            foreach (MFPoint point in GoalsModel[index].achievementGoal.points)
            {
                result.Add(new Point(point.x, point.y));
            }

            return result;
        }
    }
}
