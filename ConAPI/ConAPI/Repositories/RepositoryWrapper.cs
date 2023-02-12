using ConAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConAPI.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly AppDbContext _context;
        private IUserRepository _user;
        private ISessionTokenRepository _sessionToken;
        private IProjectRepository _project;
        private IOfferRepository _offer;
        private IRatingRepository _rating;

        public RepositoryWrapper(AppDbContext context)
        {
            _context = context;
        }

        public IRatingRepository Rating
        {
            get
            {
                if (_rating == null) _rating = new RatingRepository(_context);
                return _rating;
            }
        }

        public IUserRepository User 
        { get
            {
                if (_user == null) _user = new UserRepository(_context);
                return _user;
            }
        }

        public ISessionTokenRepository SessionToken
        {
            get
            {
                if (_sessionToken == null) _sessionToken = new SessionTokenRepository(_context);
                return _sessionToken;
            }
        }

        public IProjectRepository Project
        {
            get
            {
                if (_project == null) _project = new ProjectRepository(_context);
                return _project;
            }
        }

        public IOfferRepository Offer
        {
            get
            {
                if (_offer == null) _offer = new OfferRepository(_context);
                return _offer;
            }
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
