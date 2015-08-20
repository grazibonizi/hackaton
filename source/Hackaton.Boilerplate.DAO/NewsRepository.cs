using Hackaton.Boilerplate.Abstraction.DAO;
using Hackaton.Boilerplate.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hackaton.Boilerplate.DAO
{
    public class NewsRepository : IRepositoryAsync<News>
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _db;
        private readonly IMongoCollection<News> _col;

        public NewsRepository()
        {
            _client = new MongoClient(ConfigurationManager.ConnectionStrings["mongodb"].ConnectionString);
            _db = _client.GetDatabase("crud");
            _col = _db.GetCollection<News>("news");
        }

        public async Task<IList<News>> GetAll()
        {
            var allNews = await _col.Find(new BsonDocument()).ToListAsync();
            return allNews;
        }

        public async Task<IList<News>> Get(
            Expression<Func<News, bool>> where
        )
        {
            var filers = Builders<News>.Filter.Where(where);
            var allNews = await _col.Find(filers).ToListAsync();
            return allNews;
        }

        public async Task Update(params News[] itens)
        {
            if (itens == null || itens.Length == 0)
            {
                throw new ArgumentException("news");
            }

            foreach (var item in itens)
            {
                var filter = Builders<News>.Filter.Where(w => w.Id == item.Id);
                await _col.ReplaceOneAsync(filter, item);
            }
        }

        public async Task Delete(params News[] itens)
        {
            if (itens == null || itens.Length == 0)
            {
                throw new ArgumentException("news");
            }

            foreach (var item in itens)
            {
                var filter = Builders<News>.Filter.Where(w => w.Id == item.Id);
                await _col.DeleteOneAsync(filter);
            }
        }

        public async Task Insert(params News[] itens)
        {
            if (itens == null || itens.Length == 0)
            {
                throw new ArgumentException("news");
            }

            await _col.InsertManyAsync(itens);
        }
    }
}
