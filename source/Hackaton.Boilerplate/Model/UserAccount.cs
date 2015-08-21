using MongoDB.Bson;
using System;
namespace Hackaton.Boilerplate.Model
{
    public class UserAccount
    {
        public ObjectId Id { get; set; }
        public string FirstName { get; set; }
        public string LasttName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
