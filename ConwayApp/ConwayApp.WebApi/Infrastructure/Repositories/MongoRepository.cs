using ConwayApp.WebApi.Application.Core.Configuration;
using ConwayApp.WebApi.Application.Interfaces;
using ConwayApp.WebApi.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace ConwayApp.WebApi.Infrastructure.Repositories
{
    public class MongoRepository<TDocument> : INoSqlRepository<TDocument> where TDocument : IDocument
    {
        private readonly IMongoCollection<TDocument> _collection;

        public MongoRepository(IMongoDBSettings settings)
        {
            var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<TDocument>(typeof(TDocument).Name);
        }


        public IQueryable<TDocument> AsQueryable()
        {
            return _collection.AsQueryable();
        }
        

        public IEnumerable<TDocument> FilterBy(Expression<Func<TDocument, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).ToEnumerable();
        }


        public TDocument FilterOneBy(Expression<Func<TDocument, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).SingleOrDefault();
        }


        public TDocument FindById(string id)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
            return _collection.Find(filter).SingleOrDefault();
        }


        public void InsertOne(TDocument document)
        {
            _collection.InsertOne(document);
        }
        

        public void UpdateOne(TDocument document, UpdateDefinition<TDocument> updateDefinition)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            _collection.UpdateOne(filter,updateDefinition);
        }


        public void DeleteOne(Expression<Func<TDocument, bool>> filterExpression)
        {
            _collection.FindOneAndDelete(filterExpression);
        }


        public long CountDocuments(Expression<Func<TDocument, bool>> filterExpression)
        {
            var count = _collection.CountDocuments(filterExpression);
            return count;
        }
    }
}