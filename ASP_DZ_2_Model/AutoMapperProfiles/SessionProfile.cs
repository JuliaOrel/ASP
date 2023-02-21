using ASP_DZ_2_Model.Models;
using ASP_DZ_2_Model.Models.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DZ_2_Model.AutoMapperProfiles
{
    public class SessionProfile:Profile
    {
        public SessionProfile()
        {
            CreateMap<Session, SessionDTO>().ReverseMap();

        }
    }
}
