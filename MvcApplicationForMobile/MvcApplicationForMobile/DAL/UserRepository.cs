using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MvcApplicationForMobile.Models;

namespace MvcApplicationForMobile.DAL
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private UserContext context;

        public UserRepository(UserContext context)
        {
            this.context = context;
        }

        public IEnumerable<User> GetUsers()
        {
            return context.Users.Where(u => u.IsDeleted == false || u.IsDeleted == null).OrderBy(u => u.FirstName).ThenBy(u => u.LastName).ToList();
        }

        public User GetUserByID(int id)
        {
            return context.Users.Find(id);
        }

        public void InsertUser(User user)
        {
            user.DateAdded = DateTime.Now;
            context.Users.Add(user);
        }

        public void DeleteUser(int userID)
        {
            User user = context.Users.Find(userID);
            user.IsDeleted = true;
            user.DateModified = DateTime.Now;
        }

        public void UpdateUser(User user)
        {
            user.DateModified = DateTime.Now;
            context.Entry(user).State = EntityState.Modified;
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
   