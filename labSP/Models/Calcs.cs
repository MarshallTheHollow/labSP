using Microsoft.SolverFoundation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labSP.Models
{
    public class Calcs
    {
        public MathOutput Calc(MathInput mi)
        {
            List<SolverRow> solverList = new List<SolverRow>();

            double cusum = 0;
            double snsum = 0;
            double pbsum = 0;
            double znsum = 0;
            double TotalCost = 0;

            // Исходные данные задачи
            for (int i = 0; i<3; i++)
            {
                solverList.Add(mi.Alloy[i]);
            }           

            SolverContext context = SolverContext.GetContext();
            Model model = context.CreateModel();

            Set users = new Set(Domain.Any, "users");

            Parameter cu = new Parameter(Domain.Real, "Cu", users);
            cu.SetBinding(solverList, "Cu", "xid");

            Parameter pb = new Parameter(Domain.Real, "Pb", users);
            pb.SetBinding(solverList, "Pb", "xid");

            Parameter sn = new Parameter(Domain.Real, "Sn", users);
            sn.SetBinding(solverList, "Sn", "xid");

            Parameter zn = new Parameter(Domain.Real, "Zn", users);
            zn.SetBinding(solverList, "Zn", "xid");

            Parameter cost = new Parameter(Domain.Real, "Cost", users);
            cost.SetBinding(solverList, "Cost", "xid");

            model.AddParameters(cu, pb, sn, zn, cost);

            Decision choose = new Decision(Domain.IntegerNonnegative, "choose", users);
            model.AddDecision(choose);

            model.AddGoal("goal", GoalKind.Minimize, Model.Sum(Model.ForEach(users, xid => choose[xid] * cost[xid]))); 

            // Ограничения по сплавам
            model.AddConstraints("cumax", Model.Sum(Model.ForEach(users, xid => choose[xid] * cu[xid])) <= mi.Cuvalue.max, Model.Sum(Model.ForEach(users, xid => choose[xid] * cu[xid])) >= mi.Cuvalue.min);
            model.AddConstraints("snmax", Model.Sum(Model.ForEach(users, xid => choose[xid] * sn[xid])) <= mi.Snvalue.max, Model.Sum(Model.ForEach(users, xid => choose[xid] * sn[xid])) >= mi.Snvalue.min);
            model.AddConstraints("pbmax", Model.Sum(Model.ForEach(users, xid => choose[xid] * pb[xid])) <= mi.Pbvalue.max, Model.Sum(Model.ForEach(users, xid => choose[xid] * pb[xid])) >= mi.Pbvalue.min);
            model.AddConstraints("znmax", Model.Sum(Model.ForEach(users, xid => choose[xid] * zn[xid])) <= mi.Znvalue.max, Model.Sum(Model.ForEach(users, xid => choose[xid] * zn[xid])) >= mi.Znvalue.min);

            //if (mi.Cuvalue.max < mi.Cuvalue.min)
            //{
            //    model.AddConstraints("cumax", Model.Sum(Model.ForEach(users, xid => choose[xid] * cu[xid])) <= mi.Cuvalue.max);
            //}
            //if (mi.Snvalue.max < mi.Snvalue.min)
            //{
            //    model.AddConstraints("snmax", Model.Sum(Model.ForEach(users, xid => choose[xid] * sn[xid])) <= mi.Snvalue.max);
            //}
            //if (mi.Pbvalue.max < mi.Pbvalue.min)
            //{
            //    model.AddConstraints("pbmax", Model.Sum(Model.ForEach(users, xid => choose[xid] * pb[xid])) <= mi.Pbvalue.max);
            //}
            //if (mi.Znvalue.max < mi.Znvalue.min)
            //{
            //    model.AddConstraints("znmax", Model.Sum(Model.ForEach(users, xid => choose[xid] * zn[xid])) <= mi.Znvalue.max);
            //}

            model.AddConstraint("sum", Model.Sum(Model.ForEach(users, xid => choose[xid])) == mi.requiredweight);
        
            Solution solution = context.Solve();
            Report report = solution.GetReport();
            List<double> answers = new List<double>();
            for (int i = 0; i < solverList.Count; i++)
            {
                double answr = choose.GetDouble(solverList[i].xid);
                answers.Add(answr);
                cusum += answr * mi.Alloy[i].Cu;
                pbsum += answr * mi.Alloy[i].Pb;
                snsum += answr * mi.Alloy[i].Sn;
                znsum += answr * mi.Alloy[i].Zn;
                TotalCost += answr* mi.Alloy[i].Cost;
            }
            double totalweight = cusum + pbsum + snsum + znsum;
            MathOutput mo = new MathOutput() { totalcost = TotalCost, totalweight = totalweight, answers = answers, cusum = cusum, pbsum = pbsum, snsum = snsum, znsum = znsum, inputs = mi };
            return mo;
        }
    }
}