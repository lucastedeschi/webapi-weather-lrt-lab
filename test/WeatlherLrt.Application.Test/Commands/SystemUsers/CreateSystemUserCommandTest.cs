using System.Linq;
using System.Threading.Tasks;
using WeatherLrt.Application.Commands.SystemUsers.Create;
using WeatherLrt.Application.Interfaces;
using WeatlherLrt.Application.Test.Persistence;
using Xunit;

namespace WeatlherLrt.Application.Test.Commands.SystemUsers
{
    public sealed class CreateSystemUserCommandTest
    {
        private readonly IWeatherLrtContext _context;
        private readonly CreateSystemUserCommandHandler _handler;

        public CreateSystemUserCommandTest()
        {
            _context = WeatherLrtTestContextFactory.CreateContextForSQLite();

            _handler = new CreateSystemUserCommandHandler(_context);
        }

        [Fact]
        public async Task Handler_GivenAValidRequest_ShouldCreateOneSystemUser()
        {
            var request = new CreateSystemUserCommand
            {
                Email = "name@email.com",
                Name = "name"
            };

            var response = await _handler.Handle(request, default);

            var systemUser = await _context.SystemUsers.FindAsync(response.SystemUserId);

            Assert.Empty(response.Errors);
            Assert.NotNull(systemUser);
            Assert.Equal(request.Email, systemUser.Email);
            Assert.Equal(request.Name, systemUser.Name);
            Assert.NotEqual(default, systemUser.CreatedOn);
            Assert.NotEqual(default, systemUser.ModifiedOn);
            Assert.Equal(systemUser.CreatedOn, systemUser.ModifiedOn);
        }

        [Fact]
        public async Task Handler_GivenARequestWithAShorterEmailAndAnEmptyName_ShouldRejectWithTwoValidations()
        {
            var request = new CreateSystemUserCommand
            {
                Email = "n@",
                Name = string.Empty
            };

            var response = await _handler.Handle(request, default);

            Assert.NotEmpty(response.Errors);
            Assert.Equal(2, response.Errors.Count());
        }
    }
}
