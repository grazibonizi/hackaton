using MongoDB.Bson;
using System;

namespace Hackaton.Boilerplate.Model
{
    public class News
    {
        public ObjectId Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime PublishDate { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool Active { get; set; }
    }
}
