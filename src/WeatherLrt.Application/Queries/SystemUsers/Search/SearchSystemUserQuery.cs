using System.Collections.Generic;
using MediatR;
using WeatherLrt.Application.Queries.Common;

namespace WeatherLrt.Application.Queries.SystemUsers.Search
{
    public sealed class SearchSystemUserQuery : IRequest<List<SystemUserResponse>>
    {
        public SearchSystemUserQuery(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public string Name { get; }

        public string Email { get; }
    }
}
