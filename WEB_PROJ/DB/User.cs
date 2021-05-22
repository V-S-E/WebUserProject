using System;
using System.Collections.Generic;

#nullable disable

namespace WEB_PROJ
{
    public partial class User
    {
        public User() { }
        public User(string login, string password, string password_salt, string mail)
        {
            Login = login;
            Password = password;
            PasswordSalt = password_salt;
            Email = mail;
        }
        public string Login { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public string Email { get; set; }
        public bool? IsVerificated { get; set; }
        public int? StatusId { get; set; }
        public int? ParentId { get; set; }
        public int? CountryId { get; set; }
        public string RegionName { get; set; }
        public DateTime? DateBirth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PatronymicName { get; set; }
        public string Phone { get; set; }
        public string Skype { get; set; }
        public string Telegram { get; set; }
        public string Vk { get; set; }
        public string Twitter { get; set; }
        public string Youtube { get; set; }
        public string PursePerfectMoneyUsd { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public int Id { get; set; }

        public virtual Status Status { get; set; }
    }
}
