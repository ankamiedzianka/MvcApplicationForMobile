using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcApplicationForMobile.Models;

namespace MvcApplicationForMobile.DAL
{
    public interface IAddressRepository : IDisposable
    {
        IEnumerable<Address> GetAddresses(int userID);
        Address GetAddressByID(int addressID);
        void InsertAddress(Address address);
        void DeleteAddress(int addressID);
        void UpdateAddress(Address address);
        void Save();
    }
}