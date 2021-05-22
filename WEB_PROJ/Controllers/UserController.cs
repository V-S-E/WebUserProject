using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WEB_PROJ.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Добавление связи с базой данных
        DBContext _con;
        public UserController(DBContext con)
        {
            _con = con;
        }
        #endregion

        #region HTTP-запросы

        //Выборка всех пользователей со всеми атрибутами
        // GET: api/User
        [HttpGet]
        public IEnumerable<User> Get()
        {
            List<User> list = _con.Users.ToList();
            //foreach (var r in list)
            //{
            //    r.Password = null; r.PasswordSalt = null;
            //} Почему-то обнуляет эти поля в базе при выборке
            return list;
        }

        //Выборка всех пользователей по id
        // GET api/User/1
        [HttpGet("{id}")]
        public User Get(int id)
        {
            User ret = _con.Users.First(x => x.Id == id);
            ret.Password = ret.PasswordSalt = "";
            return ret;
        }

        //Регистрация пользователя
        // POST api/User/<json>
        [HttpPost]
        public string Post([FromBody] User value)
        {
            /*
             * Уведомления:
             * -Логин уже занят
             * -Пользователь с таким email уже существуе
             * -Несоответствие формата логина
             * -Несоответствие формата пароля
             * -Не все поля заполнены 
             * -Ошибка на стороне сервера
             */

            if (value != null || (value.Login != "" && value.Password != "" && value.Email != ""))
            {
                List<CheckRegistration> CheckDictionary = new List<CheckRegistration>();
                //
                bool CheckLogin()
                {
                    NpgsqlConnection connection = (NpgsqlConnection)_con.Database.GetDbConnection();
                    connection.Open();
                    NpgsqlCommand command = new NpgsqlCommand("select is_unique_login_f(@log)", connection);
                    NpgsqlParameter param1 = new NpgsqlParameter("@log", NpgsqlTypes.NpgsqlDbType.Char, 25);
                    param1.Value = value.Login;
                    command.Parameters.Add(param1);
                    bool result = (bool)command.ExecuteScalar();
                    connection.Close();
                    return !result;
                }
                CheckDictionary.Add(new CheckRegistration()
                {
                    Method = CheckLogin,
                    Message = "Логин уже занят"
                });
                //
                bool CheckMail()
                {
                    NpgsqlConnection connection = (NpgsqlConnection)_con.Database.GetDbConnection();
                    connection.Open();
                    NpgsqlCommand command = new NpgsqlCommand("select is_unique_mail_f(@mail)", connection);
                    NpgsqlParameter param1 = new NpgsqlParameter("@mail", NpgsqlTypes.NpgsqlDbType.Char, 25);
                    param1.Value = value.Email;
                    command.Parameters.Add(param1);
                    bool result = (bool)command.ExecuteScalar();
                    connection.Close();
                    return !result;
                }
                CheckDictionary.Add(new CheckRegistration()
                {
                    Method = CheckMail,
                    Message = "Пользователь с таким email уже существует"
                });
                //
                bool CheckMailFormat()
                {
                    EmailAddressAttribute ea = new EmailAddressAttribute();
                    if (ea.IsValid(value.Email))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                CheckDictionary.Add(new CheckRegistration()
                    { Method = CheckMailFormat, Message="Email не соответствует формату" });
                //
                string rule = @"[^a-zA-z\d]";
                bool CheckLoginFormat()
                {
                    return Regex.IsMatch(value.Login, rule);
                }
                CheckDictionary.Add(new CheckRegistration()
                {
                    Method = CheckLoginFormat,
                    Message = "Несоответствие формата логина"
                });
                //
                bool CheckPasswordFormat()
                {
                    return Regex.IsMatch(value.Password, rule);
                }
                CheckDictionary.Add(new CheckRegistration()
                {
                    Method = CheckPasswordFormat,
                    Message = "Несоответствие формата пароля"
                });

                //List<string> ErrorList = new List<string>(); Можно собирать ошибочные сообщения(или класс сообщений) в список
                string error_string = "";
                foreach (var r in CheckDictionary)
                {
                    bool res = r.Method();
                    if (res == true)
                    {
                        error_string += r.Message + "\n";
                    }
                }
                if (error_string != "")
                {
                    return error_string;
                }
 /*Добавление*/ else
                {
                    //try
                    //{
                    value.PasswordSalt = GenerSalt();
                    value.Password = HashFunc(value.Password, value.PasswordSalt);
                    //User u = new User(value.Login,
                    //    value.Password,
                    //    value.PasswordSalt,
                    //    value.Email);
                        _con.Users.Add(value);
                        _con.SaveChanges();
                        return "Добавление успешно";
                   // }
                    //catch
                    //{
                     //   return "Ошибка на стороне сервера";
                    //}
                }
            }
            else
            {
                return "Не все поля заполнены";
            }
        }

        //Обновление данных пользователя
        // PUT api/User/<json>
        [HttpPut]
        public string Put([FromBody] User value)
        {
            User usr = _con.Users.First(x => x.Id == value.Id);

            usr.FirstName = value.FirstName;
            usr.LastName = value.LastName;
            usr.PatronymicName = value.PatronymicName;
            usr.DateBirth = value.DateBirth;
            usr.Phone = value.Phone;

            usr.CountryId = value.CountryId;
            usr.RegionName = value.RegionName;

            usr.Skype = value.Skype;
            usr.Telegram = value.Telegram;
            usr.Vk = value.Vk;
            usr.Facebook = value.Facebook;
            usr.Instagram = value.Instagram;
            usr.Twitter = value.Twitter;
            usr.Youtube = value.Youtube;
            usr.PursePerfectMoneyUsd = value.PursePerfectMoneyUsd;

            _con.SaveChanges();
            return usr.FirstName;
        }

        // DELETE api/User/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //    _con.Users.Remove(_con.Users.First(x => x.Id == id));
        //    _con.SaveChanges();
        //}
        #endregion
        
        #region Методы
        public string HashFunc(string pas, string salt)
        {
            byte[] bsalt = Encoding.Default.GetBytes(salt);
            byte[] bpas = Encoding.Default.GetBytes(pas);
            byte[] totalstring = new byte[bpas.Length + bsalt.Length];
            //
            for (int i = 0; i < bpas.Length; i++)
            {
                totalstring[i] = bpas[i];
            }
            //
            for (int i = 0; i < bsalt.Length; i++)
            {
                totalstring[bpas.Length + i] = bsalt[i];
            }
            //
            SHA256 crypt = new SHA256CryptoServiceProvider();
            byte[] bhash = crypt.ComputeHash(totalstring);
            byte[] bhashandsalt = new byte[bhash.Length + bsalt.Length];
            for (int i = 0; i < bhash.Length; i++)
            {
                bhashandsalt[i] = bhash[i];
            }
            for (int i = 0; i < bsalt.Length; i++)
            {
                bhashandsalt[bhash.Length + i] = bsalt[i];
            }
            //

            return Convert.ToBase64String(bhashandsalt);
        }

        public string GenerSalt()
        {
            int minSaltSize = 4, maxSaltSize = 8;
            Random r = new Random();
            int saltSize = r.Next(minSaltSize, maxSaltSize);
            byte[] bsalt = new byte[saltSize];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetNonZeroBytes(bsalt);
            return Convert.ToBase64String(bsalt);
        }
        #endregion
    }

    public class CheckRegistration
    {
        public delegate bool CheckMethod();

        public CheckMethod Method;

        public string Message { get; set; }

    }

}
