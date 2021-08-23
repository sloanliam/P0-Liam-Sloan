using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class User
    {

        public User(){

        }

        public User(string name){
            this.Name = name;
        }

        public User(int id, string Name, string Username, string Password, string IsAdmin){
            this.Id = Id;
            this.Name = Name;
            this.Username = Username;
            this.Password = Password;
            this.IsAdmin = IsAdmin;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string IsAdmin {get; set;}
    }
}