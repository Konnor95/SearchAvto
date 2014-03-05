using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using SearchAvto.Models.DataModels;
using SearchAvto.Models.ViewModels;

namespace SearchAvto.Models.LogicModels
{
    public class UserManager : Manager
    {
        public UserManager(DatabaseEntities data)
            : base(data)
        {
        }

        public IEnumerable<User> GetAll()
        {
            return Data.Users;
        }
        public User Define(HttpContextBase context)
        {
            var cookieUser = context.Request.Cookies["UserId"];
            var cookieKey = context.Request.Cookies["Key"];
            if (cookieUser != null && cookieKey != null)
            {
                int id = Convert.ToInt32(cookieUser.Value);
                User user = Find(id);
                if (user == null) return null;
                return (cookieKey.Value == user.Password) ? user : null;
            }
            return null;
        }

        public User Find(int userId)
        {
            return Data.Users.FirstOrDefault(x => x.Id == userId);
        }

        public User Find(string userEmail)
        {
            return Data.Users.FirstOrDefault(x => x.Email == userEmail);
        }

        public bool RegistrateUser(RegistrationModel registrationModel)
        {
            User existingUser;
            try
            {
                existingUser = Find(registrationModel.Email);
            }
            catch
            {
                existingUser = null;
            }

            if (existingUser != null)
                return false;
            var user = new User
            {
                Name = registrationModel.Name,
                Email = registrationModel.Email,
                Password = SecurityManager.GetHashString(registrationModel.Password),
                Status = UserStatus.Unconfirmed
            };


            try
            {
                Data.Users.Add(user);
                Data.SaveChanges();
                SendConfirmationMail(user);
            }
            catch
            {
                Data.Users.Remove(user);
                return false;
            }

            return true;
        }

        private void SendConfirmationMail(User user)
        {
            ISender confirmationMessageSender = new ConfirmationMailSender();
            string token = SecurityManager.GetHashString(user.Email + user.Password);
            string method = ConfigurationManager.AppSettings["method"] + "/Confirm?hash=" + token;

            string message = String.Format(
                "Подтвердите, пожалуйста регистрацию. Для подтверждения перейдите по ссылке : {0}",
                method);
            confirmationMessageSender.Send("Подтверждение регистрации", message, user.Email);
        }


        public bool ConfirmRegistration(string hash)
        {
            User user = Enumerable.FirstOrDefault(Data.Users, x => (hash == SecurityManager.GetHashString(x.Email + x.Password)) && x.HasNoAccess);
            if (user!=null)
            {
                user.Status = UserStatus.User;
                Data.SaveChanges();
                return true;
            }
            return false;
        }

        public User LogInUser(LogInModel model)
        {
            var email = model.Email;
            var password = model.Password;
            User user = Find(email);
            if (user == null) return null;
            if (user.Password == SecurityManager.GetHashString(password))
            {
                return user;
            }
            return null;
        }
    }
}