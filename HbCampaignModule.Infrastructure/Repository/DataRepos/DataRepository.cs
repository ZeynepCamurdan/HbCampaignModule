using HbCampaignModule.Domain.Interfaces.DataIF;
using HbCampaignModule.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace HbCampaignModule.Infrastructure.Repository.DataRepos
{
    public class DataRepository : IDataRepository
    {
        private readonly PostgreSqlDbContext _postgreSqlDbContext;
        public DataRepository(PostgreSqlDbContext postgreSqlDbContext)
        {
            this._postgreSqlDbContext = postgreSqlDbContext;
        }
    }
}
