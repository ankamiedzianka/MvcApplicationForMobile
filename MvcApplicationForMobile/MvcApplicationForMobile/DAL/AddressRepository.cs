using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MvcApplicationForMobile.Models;

namespace MvcApplicationForMobile.DAL
{
    public class AddressRepository : IAddressRepository, IDisposable
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
            //context.Addresses.Remove(address);
        }

        public void UpdateAddress(Address address)
        {
            address.DateModified = DateTime.Now;
            context.Entry(address).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
