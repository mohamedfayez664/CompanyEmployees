namespace Shared.DataTransferObjects
{
    public record CompanyDto   //only to return a response to the client
        (Guid Id,
        string Name,
        string FullAddress  //concatenate the Address and Country
        );

}
