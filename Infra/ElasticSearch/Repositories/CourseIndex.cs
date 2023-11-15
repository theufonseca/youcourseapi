using Application.Models;
using Application.Repositories;
using Nest;

namespace Infra.ElasticSearch.Repositories
{
    public class CourseIndex : ICourseIndex
    {
        private readonly IElasticClient elasticClient;

        public CourseIndex(IElasticClient elasticClient)
        {
            this.elasticClient = elasticClient;
        }

        public async Task CreateOrUpdateDocumentAsync(CourseModel courseModel)
        {
            await elasticClient.IndexDocumentAsync(courseModel);
        }

        public async Task DeleteDocumentAsync(int id)
        {
            await elasticClient.DeleteAsync<CourseModel>(id);
        }
    }
}
