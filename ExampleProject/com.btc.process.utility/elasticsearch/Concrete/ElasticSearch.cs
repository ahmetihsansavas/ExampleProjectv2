using com.btc.process.manager.System.Abstract;
using com.btc.type.Entity.System;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace com.btc.process.utility.elasticsearch.Concrete
{
    public class ElasticSearch
    {
       private static readonly ConnectionSettings connSettings =
       new ConnectionSettings(new Uri("http://localhost:9200/"))
                       .DefaultIndex("esearchitems")
                       .DefaultMappingFor<User>(m => m
                       .PropertyName(p => p.Id, "id")
           );
      private static readonly ElasticClient elasticClient = new ElasticClient(connSettings);

        public void IndexItems(string indexName ,List<User>UserList)
        {
            if (!elasticClient.Indices.Exists(indexName).Exists)
            {
                elasticClient.Indices.Create(indexName,
                     index => index.Map<User>(
                          x => x
                         .AutoMap()
                  ));

                elasticClient.Bulk(b => b
                  .Index(indexName)
                  .IndexMany(UserList)
                   );
            }
            else
            {
                elasticClient.Indices.Delete(indexName);
                elasticClient.Indices.Create(indexName,
                    index => index.Map<User>(
                         x => x
                        .AutoMap()
                 ));

                elasticClient.Bulk(b => b
                  .Index(indexName)
                  .IndexMany(UserList)
                   );
            }
        }


        public async Task<List<User>> Get()
        {
            var response = elasticClient.Search<User>(i => i
            .Size(30)
            .Query(q => q.MatchAll())
            );
            List<User> items = new List<User>();
            foreach (var item in response.Documents)
                items.Add(item);
            return items;
        }


        public async Task<List<User>> GetName(string name)
        {
            var responsedata = elasticClient.Search<User>(s => s.Source()
           .Query(q => q
               .QueryString(qs => qs
                   .AnalyzeWildcard()
                   .Query("*" + name.ToLower() + "*")
                   .Fields(fs => fs
                       .Fields(f1 => f1.Id,
                               f2 => f2.Name,
                               f3 => f3.Surname,
                               f4 => f4.UserCode,
                               f5 => f5.CreatedBy
                       )
                   )
               )
           )
       );

            var datasend = responsedata.Documents.ToList();
            return datasend;
        }

        public async Task<User> GetById(long Id)
        {
            var responsedata = elasticClient.Search<User>(s => s.Source()
          .Query(q => q
          .Match(m => m
            .Field(f => f.Id)
            .Query(Id.ToString())
             )
            )   
       );

            var datasend = responsedata.Documents.FirstOrDefault();
            return datasend;
        }

    }
}
