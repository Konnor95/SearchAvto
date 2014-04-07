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

        public User Find(string userEmail)
        {
            return Data.Users.FirstOrDefault(x => x.Email == userEmail);
        }

        public User Find(int userId)
        {
            return Data.Users.FirstOrDefault(x => x.Id == userId);
        }

        public User FindByEmail(string email)
        {
            return Data.Users.FirstOrDefault(x => x.Email == email);
        }

        public User FindByName(string name)
        {
            return Data.Users.FirstOrDefault(x => x.Name == name);
        }

        public User FindByNameOrEmail(string login)
        {
            return Data.Users.FirstOrDefault(x => x.Name == login || x.Email == login);
        }

        public ProcessResult RegistrateUser(HttpContextBase context, RegistrationModel registrationModel, HttpServerUtilityBase server, HttpPostedFileBase imageUpload)
        {
            User existingUser = Find(registrationModel.Email);
            if (existingUser != null)
                return ProcessResults.UserWithSuchEmailExists;
            existingUser = Find(registrationModel.Name);
            if (existingUser != null)
                return ProcessResults.UserWithSuchNameExists;
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
                if (imageUpload != null)
                {
                    if (imageUpload.ContentLength <= 0 || !SecurityManager.IsImage(imageUpload))
                    {
                        return ProcessResults.InvalidImageFormat;
                    }
                    user.Avatar = SaveImage(user.Id, StaticSettings.AvatarsUploadFolderPath, imageUpload, server);
                    Data.SaveChanges();
                }
                SendConfirmationMail(context, user);
            }
            catch
            {
                Data.Users.Remove(user);
                return ProcessResults.RegistrationError;
            }

            return ProcessResults.UserRegistered;
        }

        private void SendConfirmationMail(HttpContextBase context, User user)
        {
            var confirmationMessageSender = new ConfirmationMailSender();
            string token = SecurityManager.GetHashString(user.Email + user.Password);
            if (context.Request.Url != null)
            {
                string path = context.Request.Url.GetLeftPart(UriPartial.Authority) + "/User/Confirm?hash=" + token;
                string message = String.Format("Для подтверждения регистрации перейдите по ссылке : {0}", path);
                confirmationMessageSender.Send("Подтверждение регистрации", message, user.Email);
            }
        }


        public bool ConfirmRegistration(string hash)
        {
            User user = Enumerable.FirstOrDefault(Data.Users, x => (hash == SecurityManager.GetHashString(x.Email + x.Password)) && x.HasNoAccess);
            if (user != null)
            {
                user.Status = UserStatus.User;
                Data.SaveChanges();
                return true;
            }
            return false;
        }

        public ProcessResult LogInUser(LogInModel model,out User user)
        {
            user = FindByNameOrEmail(model.Login);
            if (user == null) return ProcessResults.InvalidLoginOrEmail;
            if (user.Password == SecurityManager.GetHashString(model.Password))
            {
                return ProcessResults.UserLogedIn;
            }
            return ProcessResults.InvalidPassword;
        }

        public ProcessResult ChangeUserStatus(int id, int status)
        {
            User user = Find(id);
            if (user == null)
                return ProcessResults.NoSuchUser;
            user.Status = (short)status;
            Data.SaveChanges();
            return ProcessResults.UserStatusChanged;
        }
    }
}