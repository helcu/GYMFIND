using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GYMFIND.Models;

namespace GYMFIND.ViewModel
{
    public class VmCompraPlan
    {
        public int planID { get; set; }

        public Planes plan { get; set; }

        public void fill(int planID) {

            GYMEntities context = new GYMEntities();

            plan = context.Planes.FirstOrDefault(x => x.PlanID == planID);

            this.planID = plan.PlanID;

        }

    }
}