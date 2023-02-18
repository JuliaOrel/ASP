using ASP_DZ_6_Books.Models;
using ASP_DZ_6_Books.Models.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DZ_6_Books.AutoMapperProfiles
{
    public class BookProfile: Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDTO>().ReverseMap();
        }
    }
}
