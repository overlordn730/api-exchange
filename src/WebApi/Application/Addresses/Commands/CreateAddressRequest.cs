using MediatR;
using WebApi.Domain.Dto.Addresses;

namespace WebApi.Application.Addresses.Commands;

public class CreateAddressRequest : IRequest<AddressResponse>
{
    public int UserId { get; set; }
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string? ZipCode { get; set; }
}