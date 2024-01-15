using ConwayApp.WebApi.Domain.Entities;
using MongoDB.Driver;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq.Expressions;

namespace ConwayApp.WebApi.Application.Interfaces
{
    public interface INoSqlRepository<TDocument> where TDocument : IDocument
    {
        IQueryable<TDocument> AsQueryable();

        IEnumerable<TDocument> FilterBy(Expression<Func<TDocument, bool>> filterExpression);

        TDocument FindById(string id);

        void InsertOne(TDocument document);

        void UpdateOne(TDocument document, UpdateDefinition<TDocument> updateDefinition);

        void DeleteOne(Expression<Func<TDocument, bool>> filterExpression);

        long CountDocuments(Expression<Func<TDocument, bool>> filterExpression);

        TDocument FilterOneBy(Expression<Func<TDocument, bool>> filterExpression);

    }
}