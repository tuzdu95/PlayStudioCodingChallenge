using Microsoft.Extensions.Options;
using PlayStudioCodingChallenge.Application.AbstractRepository;
using PlayStudioCodingChallenge.Domain.Entities;
using PlayStudioCodingChallenge.Persistence.DbConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayStudioCodingChallenge.Persistence.Repositories
{
    public class PlayerRepository : BaseRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(IOptions<MongoConfiguration> databaseSettings) : base(databaseSettings)
        {
        }
    }
}
