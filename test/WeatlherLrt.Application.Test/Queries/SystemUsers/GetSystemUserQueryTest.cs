using System.Threading.Tasks;
using AutoMapper;
using WeatherLrt.Application.Exceptions;
using WeatherLrt.Application.Interfaces;
using WeatherLrt.Application.Profiles;
using WeatherLrt.Application.Queries.SystemUsers.Get;
using WeatherLrt.Domain.Entities;
using WeatlherLrt.Application.Test.Persistence;
using Xunit;

namespace WeatlherLrt.Application.Test.Queries.SystemUsers
{
    public sealed class GetSystemUserQueryTest
    {
        private readonly IWeatherLrtContext _context;
        private readonly IMapper _mapper;
        private readonly GetSystemUserQueryHandler _handler;

        public GetSystemUserQueryTest()
        {
            _context = WeatherLrtTestContextFactory.CreateContextForSQLite();
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile(new SystemUserProfile())).CreateMapper();
            _handler = new GetSystemUserQueryHandler(_context, _mapper);
        }

        [Fact]
        public async Task Handle_GivenAnExistentSystemUserId_ShouldReturnOneSystemUser()
        {
            var systemUser = new SystemUser
            {
                Name = "name",
                Email = "name@email.com",
            };

            _context.SystemUsers.Add(systemUser);

            await _context.SaveChangesAsync(default);

            var request = new GetSystemUserQuery(systemUser.SystemUserId);

            var response = await _handler.Handle(request, default);

            Assert.NotNull(response);
            Assert.Equal(systemUser.Email, response.Email);
            Assert.Equal(systemUser.Name, response.Name);
            Assert.Equal(systemUser.CreatedOn, response.CreatedOn);
            Assert.Equal(systemUser.ModifiedOn, response.ModifiedOn);
        }

        [Fact]
        public async Task Handle_GivenAnInexistentSystemUserId_ShouldRaiseAnException()
        {
            var request = new GetSystemUserQuery(1);

            await Assert.ThrowsAsync<NotFoundException>(async () => await _handler.Handle(request, default));
        }
    }
}
