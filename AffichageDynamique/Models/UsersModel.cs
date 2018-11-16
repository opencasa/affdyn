using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AffichageDynamique.Models
{
    public class UsersModel
    {
        public int id { get; set; }
        public string tri { get; set; }
        public string name { get; set; }
        public string firstName { get; set; }
        public string email { get; set; }
        public string telephone { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int gender_id { get; set; }
        [DataType(DataType.Date)]
        public DateTime birthdate { get; set; }
        [DataType(DataType.Date)]
        public DateTime entry_date { get; set; }
        public int user_service { get; set; }
        public int user_function { get; set; }
        public int block { get; set; }
        public int sendEmail { get; set; }
        [DataType(DataType.Date)]
        public DateTime registerDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime lastvisitDate { get; set; }
        public string activation { get; set; }
        public string _params { get; set; }
        [DataType(DataType.Date)]
        public DateTime lastResetTime { get; set; }
        public int resetCount { get; set; }
        public string otpKey { get; set; }
        public string otep { get; set; }
        public int requireReset { get; set; }
        public int state { get; set; }
        public bool admin { get; set; }
        public string ldap_login { get; set; }
        public string ldap_password { get; set; }
        public string avatar_file { get; set; }
        public bool session_save { get; set; }
        public bool items_by_page { get; set; }

        public string Message { get; set; }
    }
}
