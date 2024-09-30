using ArtGallery.Models;
using AutoMapper;

namespace ArtGallery.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Artist, ArtistView>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Account.UserName));
            CreateMap<ArtistEdit, Artist>();
            CreateMap<ArtistCreate, Artist>();
            CreateMap<ArtistCreate, Account>();
            CreateMap<Artwork, ArtworkView>()
                .ForMember(dest => dest.ArtistName, opt => opt.MapFrom(src => src.Artist.ArtistName));
            CreateMap<ArtworkEdit, Artwork>().ReverseMap();
            CreateMap<ArtworkCreate, Artwork>();
            CreateMap<Auction, AuctionView>()
                .ForMember(dest => dest.ArtworkTitle, opt => opt.MapFrom(src => src.Artwork.Title))
                .ForMember(dest => dest.ImageURL, opt => opt.MapFrom(src => src.Artwork.ImageURL))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Account.UserName));
            CreateMap<AuctionEdit, Auction>().ReverseMap();
            CreateMap<AuctionCreate, Auction>();
            CreateMap<Customer, CustomerView>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Account.UserName));
            CreateMap<CustomerEdit, Customer>();
            CreateMap<CustomerCreate, Customer>();
            CreateMap<CustomerCreate, Account>();
            CreateMap<Exhibition, ExhibitionView>().ReverseMap();
            CreateMap<Register, Account>();
            CreateMap<Register, Customer>();
            CreateMap<Register, Artist>();
        }
    }
}
