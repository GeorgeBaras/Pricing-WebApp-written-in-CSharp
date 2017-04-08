using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Tutorial.EF_DBContext;
using Tutorial.EF_DBContext_Repositories;
using Tutorial.MyClasses;

namespace Tutorial.My_EntityClasses
{
    public class MasterRepository
    {
        public static IUnityContainer myContainer { get; set; } = UnityContainerSingleton.getContainer();
        public static VehicleRepository vehicleRepository { get; set; } = (VehicleRepository)myContainer.Resolve<IRepository>("vehicleRepository");
        public static PriceRecordRepository priceRecordRepository { get; set; } = (PriceRecordRepository)myContainer.Resolve<IRepository>("priceRecordRepository");
        public static appDBContext db { get; set; } = (appDBContext)myContainer.Resolve<DbContext>("applicationDBContext");
    }
}