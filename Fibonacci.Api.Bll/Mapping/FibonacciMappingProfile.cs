using AutoMapper;
using Fibonacci.Api.Contracts.Responses;
using Fibonacci.Client.Contracts;

namespace Fibonacci.Api.Bll.Mapping
{
    public class FibonacciMappingProfile : Profile
    {
        public FibonacciMappingProfile()
        {
            CreateMap<CalculateNextFibonacciResponse, NextFibonacciCalculatedResultMessage>()
                .ForMember(d => d.GeneratedOn, o => o.MapFrom(s => DateTime.UtcNow));

            // When complex domain objects and persistence (POCO) arise, mapping from DTOs is required, should go here

        }
    }
}
