using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MvcApplicationForMobile.Models;

namespace MvcApplicationForMobile.DAL
{
    public class AddressRepository : IAddressRepository
    {
        private UserContext context;

        public AddressRepository(UserContext context)
        {
            this.context = context;
        }

        public IEnumerable<Address> GetAddresses(int userID)
        {
            return context.Addresses.Where(a => a.UserID == userID && (a.IsDeleted == false || a.IsDeleted == null)).ToList();
        }

        public Address GetAddressByID(int id)
        {
            return context.Addresses.Find(id);
        }

        public void InsertAddress(Address address)
        {
            address.DateAdded = DateTime.Now;
            context.Addresses.Add(address);
        }

        public void DeleteAddress(int addressID)
        {
            Address address = context.Addresses.Find(addressID);
            address.IsDeleted = true;
            address.DateModified = DateTime.Now;
        }

        public void UpdateAddress(Address address)
        {
            address.DateModified = DateTime.Now;
            context.Entry(address).State = EntityState.Modified;
        }
    }
}
