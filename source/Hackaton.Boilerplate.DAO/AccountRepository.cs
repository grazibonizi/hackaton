using Hackaton.Boilerplate.Abstraction.DAO;
using Hackaton.Boilerplate.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hackaton.Boilerplate.DAO
{
    public class AccountRepository : IRepositoryAsync<UserAccount>
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _db;
        private readonly IMongoCollection<UserAccount> _col;

        public AccountRepository(IMongoClient client)
        {
            _client = client;
            _db = _client.GetDatabase("crud");
            _col = _db.GetCollection<UserAccount>("user_account");
        }

        public async Task<IList<UserAccount>> GetAll()
        {
            var allNews = await _col.Find(new BsonDocument()).ToListAsync();
            return allNews;
        }

        public async Task<IList<UserAccount>> Get(
            Expression<Func<UserAccount, bool>> where
        )
        {
            var filers = Builders<UserAccount>.Filter.Where(where);
            var allNews = await _col.Find(filers).ToListAsync();
            return allNews;
        }

        public async Task Update(params UserAccount[] itens)
        {
            if (itens == null || itens.Length == 0)
            {
                throw new ArgumentException("itens");
            }

            foreach (var item in itens)
            {
                var filter = Builders<UserAccount>.Filter.Where(w => w.Id == item.Id);
                await _col.ReplaceOneAsync(filter, item);
            }
        }

        public async Task Delete(params UserAccount[] itens)
        {
            if (itens == null || itens.Length == 0)
            {
                throw new ArgumentException("itens");
            }

            foreach (var item in itens)
            {
                var filter = Builders<UserAccount>.Filter.Where(w => w.Id == item.Id);
                await _col.DeleteOneAsync(filter);
            }
        }

        public async Task Insert(params UserAccount[] itens)
        {
            if (itens == null || itens.Length == 0)
            {
                throw new ArgumentException("itens");
            }

            await _col.InsertManyAsync(itens);
        }
    }
}
