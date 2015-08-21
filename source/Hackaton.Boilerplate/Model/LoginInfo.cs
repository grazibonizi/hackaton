using MongoDB.Bson;

namespace Hackaton.Boilerplate.Model
{
    public class LoginInfo
    {
        public ObjectId Id { get; set; }
        public ObjectId UserAccountId { get; set; }
        public string TokenInfo { get; set; }
        public string DateIssued { get; set; }
    }
}
