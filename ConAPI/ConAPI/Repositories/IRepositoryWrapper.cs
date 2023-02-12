using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConAPI.Repositories
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        ISessionTokenRepository SessionToken { get; }
        IProjectRepository Project { get; }
        IOfferRepository Offer { get; }
        IRatingRepository Rating { get; }
        Task SaveAsync();
    }
}
