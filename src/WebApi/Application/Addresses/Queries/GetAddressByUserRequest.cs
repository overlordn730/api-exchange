using MediatR;
using WebApi.Domain.Dto.Addresses;

namespace WebApi.Application.Addresses.Queries;

public class GetAddressesByUserRequest : IRequest<IEnumerable<AddressResponse>>
{
    public int UserId { get; set; }
}