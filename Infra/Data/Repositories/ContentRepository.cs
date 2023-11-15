using Application.Repositories;
using Dapper;
using Domain.Entities.CourseAggregate;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repositories
{
    public class ContentRepository : IContentRepository
    {
        private readonly MySqlConnection _connection;
        public ContentRepository(DataContext context)
        {
            _connection = context.connection;
        }

        public async Task<int> Create(Content content)
        {
            var id = await _connection.ExecuteScalarAsync<int>($@"INSERT INTO contents (trailId, orderNumber, title, contentType, contentUrl, isVisible)
                VALUES (@TrailId, @OrderNumber, @Title, @ContentType, @ContentUrl, @IsVisible);
                SELECT LAST_INSERT_ID();", content);

            return id;
        }

        public async Task<bool> Delete(int id)
        {
            await _connection.ExecuteAsync($@"DELETE FROM contents WHERE id = @Id", new { Id = id });
            return false;
        }

        public async Task<Content?> Get(int id)
        {
            var content = await _connection.QueryFirstOrDefaultAsync<Content>($@"SELECT * FROM contents WHERE id = @Id", new { Id = id });
            return content;
        }

        public async Task<IEnumerable<Content>> GetAll()
        {
            var contents = await _connection.QueryAsync<Content>($@"SELECT * FROM contents");
            return contents;
        }

        public async Task<IEnumerable<Content>> GetByTrail(int trailId)
        {
            var contents = await _connection.QueryAsync<Content>($@"SELECT * FROM contents WHERE trailId = @TrailId", new { TrailId = trailId });
            return contents;
        }

        public async Task<bool> Update(Content content)
        {
            await _connection.ExecuteAsync($@"UPDATE contents SET trailId = @TrailId, orderNumber = @OrderNumber, title = @Title, contentType = @ContentType, contentUrl = @ContentUrl, isVisible = @IsVisible WHERE id = @Id", content);
            return true;
        }
    }
}
