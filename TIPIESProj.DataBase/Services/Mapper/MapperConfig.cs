using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TIPIESProj.DataBase.Models;

namespace TIPIESProj.DataBase.Services
{
    public class MapperConfig
    {
        public static Mapper GetMapper<T>()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<T, T>());
            return new Mapper(config);
        }
    }
}
