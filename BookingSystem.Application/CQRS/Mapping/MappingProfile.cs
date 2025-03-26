using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BookingSystem.Application.CQRS.Commands;
using BookingSystem.Core.DTOs;
using BookingSystem.Core.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookingSystem.Application.CQRS.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateReservationCommand, Reservation>().ReverseMap();
            CreateMap<UpdateReservationCommand, Reservation>().ReverseMap();
            CreateMap<Reservation, ReservationDto>().ReverseMap();
            CreateMap<Reservation, AddReservationDto>().ReverseMap();
            CreateMap<Reservation, GetReservationResponseDTO>()
            .ForMember(dest => dest.TripName, opt => opt.MapFrom(src => src.Trip != null ? src.Trip.Name : "Unknown"))
            .ForMember(dest => dest.ReservedByName, opt => opt.MapFrom(src => src.ReservedBy != null ? src.ReservedBy.UserName : "Unknown"));

        }
    }
}
