using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WeatherLrt.Application.Interfaces;
using WeatherLrt.Application.Profiles;
using WeatherLrt.Application.Queries.SystemUsers.Search;
using WeatherLrt.Domain.Entities;
using WeatlherLrt.Application.Test.Persistence;
using Xunit;

namespace WeatlherLrt.Application.Test.Queries.SystemUsers
{
    public sealed class SearchSystemUserQueryTest
    {
        private readonly IWeatherLrtContext _context;
        private readonly SearchSystemUserQueryHandler _handler;

        public SearchSystemUserQueryTest()
        {
            _context = WeatherLrtTestContextFactory.CreateContextForSQLite();
            _handler = new SearchSystemUserQueryHandler(_context, new MapperConfiguration(cfg => cfg.AddProfile(new SystemUserProfile())).CreateMapper());
        }

        [Fact]
        public async Task Handle_GivenAnExistentNameAndEmail_ShouldReturnOneSystemUser()
        {
            var systemUserA = new SystemUser
            {
                Name = "name A",
                Email = "namea@email.com",
            };

            var systemUserB = new SystemUser
            {
                Name = "name B",
                Email = "nameb@email.com",
            };

            _context.SystemUsers.Add(systemUserA);
            _context.SystemUsers.Add(systemUserB);

            await _context.SaveChangesAsync(default);

            var request = new SearchSystemUserQuery("name A", "namea@email.com");

            var response = await _handler.Handle(request, default);

            Assert.NotNull(response);
            Assert.Single(response);
            Assert.Equal(systemUserA.Name, response.Single().Name);
            Assert.Equal(systemUserA.Email, response.Single().Email);
            Assert.Equal(systemUserA.CreatedOn, response.Single().CreatedOn);
            Assert.Equal(systemUserA.ModifiedOn, response.Single().ModifiedOn);
        }

        [Fact]
        public async Task Handle_GivenAnExistentName_ShouldReturnOneSystemUser()
        {
            var systemUserA = new SystemUser
            {
                Name = "name A",
                Email = "namea@email.com",
            };

            var systemUserB = new SystemUser
            {
                Name = "name B",
                Email = "nameb@email.com",
            };

            _context.SystemUsers.Add(systemUserA);
            _context.SystemUsers.Add(systemUserB);

            await _context.SaveChangesAsync(default);

            var request = new SearchSystemUserQuery("name A", default);

            var response = await _handler.Handle(request, default);

            Assert.NotNull(response);
            Assert.Single(response);
            Assert.Equal(systemUserA.Name, response.Single().Name);
            Assert.Equal(systemUserA.Email, response.Single().Email);
            Assert.Equal(systemUserA.CreatedOn, response.Single().CreatedOn);
            Assert.Equal(systemUserA.ModifiedOn, response.Single().ModifiedOn);
        }

        [Fact]
        public async Task Handle_GivenAnExistentEmail_ShouldReturnOneSystemUser()
        {
            var systemUserA = new SystemUser
            {
                Name = "name A",
                Email = "namea@email.com",
            };

            var systemUserB = new SystemUser
            {
                Name = "name B",
                Email = "nameb@email.com",
            };

            _context.SystemUsers.Add(systemUserA);
            _context.SystemUsers.Add(systemUserB);

            await _context.SaveChangesAsync(default);

            var request = new SearchSystemUserQuery(default, "namea@email.com");

            var response = await _handler.Handle(request, default);

            Assert.NotNull(response);
            Assert.Single(response);
            Assert.Equal(systemUserA.Name, response.Single().Name);
            Assert.Equal(systemUserA.Email, response.Single().Email);
            Assert.Equal(systemUserA.CreatedOn, response.Single().CreatedOn);
            Assert.Equal(systemUserA.ModifiedOn, response.Single().ModifiedOn);
        }

        [Fact]
        public async Task Handle_GivenAnInexistentNameAndEmail_ShouldNotReturnASystemUser()
        {
            var request = new SearchSystemUserQuery("name A", "namea@email.com");

            var response = await _handler.Handle(request, default);

            Assert.NotNull(response);
            Assert.Empty(response);
        }
    }
}
