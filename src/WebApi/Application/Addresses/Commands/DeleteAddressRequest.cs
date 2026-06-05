using MediatR;

namespace WebApi.Application.Addresses.Commands;

public class DeleteAddressRequest : IRequest
{
    public int Id { get; set; }
}