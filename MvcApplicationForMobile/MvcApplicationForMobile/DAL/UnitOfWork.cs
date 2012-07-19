using System;
using MvcApplicationForMobile.Models;

namespace MvcApplicationForMobile.DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private UserContext context = new UserContext();
        private IUserRepository userRepository = null;
        private IAddressRepository addressRepository = null;

        public IUserRepository UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new UserRepository(context);
                }
                return userRepository;
            }
        }

        public IAddressRepository AddressRepository
        {
            get
            {
                if (this.addressRepository == null)
                {
                    this.addressRepository = new AddressRepository(context);
                }
                return addressRepository;
            }
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