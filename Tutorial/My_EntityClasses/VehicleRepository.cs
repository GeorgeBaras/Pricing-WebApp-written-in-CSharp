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

    private VehicleDAO vehicleDAO;

    public VehicleRepository(VehicleDAO vehicleDAO)
    {
            this.vehicleDAO = vehicleDAO;
    }

        public Vehicle addVehicle(Vehicle vehicle) {
            return vehicleDAO.addVehicle(vehicle);
        }

        public int addVehicleList(List<Vehicle> vehicles) {
            return vehicleDAO.addVehicleList(vehicles);
        }

        public List<Vehicle> getAllEntries()
        {
            return vehicleDAO.getAllEntries();
        }

        public Vehicle getVehicleByLookupCode(String lookupCode)
        {
            return vehicleDAO.getVehicleByLookupCode(lookupCode);
        }

        public Boolean deleteVehicleBylookupCode(string lookupCode)
        {
            return vehicleDAO.deleteVehicleBylookupCode(lookupCode);
        }

        public Boolean updateFieldBylookupCode<T>(string lookupCode, VehicleDAO.VehicleFields field, T updatedFieldValue )
        {
            return vehicleDAO.updateFieldBylookupCode<T>(lookupCode, field, updatedFieldValue);
        }


        public Boolean deleteAllEntries()
        {
            return vehicleDAO.deleteAllEntries();
        }

        public Boolean dbIsEmpty()
        {
            return vehicleDAO.dbIsEmpty();
        }

        public int getCount() {
            return vehicleDAO.getCount();
        }

    }
}