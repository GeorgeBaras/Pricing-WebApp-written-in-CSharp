using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tutorial.EF_DBContext;
using Tutorial.MyClasses;

namespace Tutorial.EF_DBContext_Repositories
{
    public class VehicleRepository : IRepository
    {
    public enum VehicleFields {make, model, derivative, lookupCode, mileage, value};

    private MyApplicationDBContext db;
    public VehicleRepository(MyApplicationDBContext db)
    {
            this.db = db;
    }

        public Vehicle addVehicle(Vehicle vehicle) {
            Vehicle addedVehicle = db.Vehicles.Add(vehicle);
            db.SaveChanges();
            return addedVehicle;
        }

        public int addVehicleList(List<Vehicle> vehicles) {
            IEnumerable<Vehicle> addedVehicles = db.Vehicles.AddRange(vehicles);
            return addedVehicles.Count<Vehicle>();
        }

        public List<Vehicle> getAllEntries()
        {
            return db.Vehicles.ToList();
        }

        public Vehicle getVehicleByLookupCode(String lookupCode)
        {
            var query = from v in db.Vehicles
                        orderby v.lookupCode
                        select v;

            foreach (Vehicle vehicle in query) {
                if (vehicle.lookupCode.Equals(lookupCode)) {
                    return vehicle;
                }
            }
            return null;
        }

        public Boolean deleteVehicleBylookupCode(string lookupCode)
        {
            // Alternative way
            // db.Vehicles.RemoveRange(db.Vehicles.Where(v => v.lookupCode == lookupCode));

            Vehicle vehicle = getVehicleByLookupCode(lookupCode);
            Boolean deleted = vehicle.Equals(db.Vehicles.Remove(vehicle));
            db.SaveChanges();
            return deleted;
        }

        public Boolean updateFieldBylookupCode<T>(string lookupCode, VehicleFields field, T updatedFieldValue )
        {
            Vehicle vehicle = (from v in db.Vehicles
                             where v.lookupCode.Equals(lookupCode)
                             select v).SingleOrDefault();
            switch (field)
            {
                case VehicleFields.make:
                    vehicle.make = updatedFieldValue.ToString();
                    break;
                case VehicleFields.model:
                    vehicle.model = updatedFieldValue.ToString();
                    break;
                case VehicleFields.derivative:
                    vehicle.derivative = updatedFieldValue.ToString();
                    break;
                case VehicleFields.lookupCode:
                    vehicle.lookupCode = updatedFieldValue.ToString();
                    break;
                case VehicleFields.mileage:
                    vehicle.mileage = Int32.Parse(updatedFieldValue.ToString());
                    break;
                case VehicleFields.value:
                    vehicle.value = Decimal.Parse(updatedFieldValue.ToString());
                    break;
                default:
                    return false;
            }
            db.SaveChanges();
            return true;
        }


        public Boolean deleteAllEntries()
        {
            Boolean deleted = (getAllEntries().Count == db.Vehicles.RemoveRange(getAllEntries()).ToList<Vehicle>().Count);
            db.SaveChanges();
            return deleted;
        }

        public Boolean dbIsEmpty()
        {
            return getAllEntries().Count==0;
        }

    }
}