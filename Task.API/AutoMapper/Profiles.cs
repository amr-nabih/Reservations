using AutoMapper;
using Task.Domain.Models;
using Task.Infrastructure.Entities;

namespace Task.API.AutoMapper
{
    public class Profiles : Profile
    {
        public Profiles() {

            CreateMap<TripModel, Trip>().ReverseMap();
            CreateMap<UserModel, User>().ReverseMap();
            CreateMap<ReservationModel, Reservation>().ReverseMap();
           


        }
    }
}
