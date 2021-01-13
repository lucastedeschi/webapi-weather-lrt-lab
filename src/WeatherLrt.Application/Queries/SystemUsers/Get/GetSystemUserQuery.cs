using MediatR;
using WeatherLrt.Application.Queries.Common;

namespace WeatherLrt.Application.Queries.SystemUsers.Get
{
    public sealed class GetSystemUserQuery : IRequest<SystemUserResponse>
    {
        public GetSystemUserQuery(long systemUserId)
        {
            SystemUserId = systemUserId;
        }

        public long SystemUserId { get; }
    }
}
