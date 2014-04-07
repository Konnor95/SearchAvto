using System.Collections.Generic;

namespace SearchAvto.Models.DataModels
{
    public class Status
    {
        public int Id { get; private set; }
         public string Name { get; private set; }

        public Status(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
    public class UserStatus
    {
        public static short Unconfirmed
        {
            get { return -1; }
        }

        public static short User
        {
            get { return 0; }
        }

        public static short Moderator
        {
            get { return 1; }
        }

        public static short Admin
        {
            get { return 2; }
        }

        public static short Root
        {
            get { return 3; }
        }

        public static string GetName(short status)
        {
            return status == Unconfirmed ? "Неподтвержден"
                 : status == User ? "Пользователь"
                 : status == Moderator ? "Модератор"
                 : status == Admin ? "Администратор"
                 : status == Root ? "Суперпользователь":"";
        }

        public static IEnumerable<Status> GetAll()
        {
            return new[]
            {
                new Status(User, GetName(User)),
                new Status(Moderator, GetName(Moderator)),
                new Status(Admin, GetName(Admin))
            };
        }
    }

    public partial class User
    {
        public bool HasRootAccess
        {
            get
            {
                return Status == UserStatus.Root;
            }
        }

        public bool HasAdminAccess
        {
            get
            {
                return Status == UserStatus.Admin || HasRootAccess;
            }
        }

        public bool HasModeratorAccess
        {
            get
            {
                return Status == UserStatus.Moderator || HasAdminAccess;
            }
        }

        public bool HasUserAccess
        {
            get
            {
                return Status == UserStatus.User || HasModeratorAccess;
            }
        }

        public bool HasNoAccess
        {
            get
            {
                return Status == UserStatus.Unconfirmed;
            }
        }

        public string StatusName
        {
            get { return UserStatus.GetName(Status); }
        }
    }
}