using AutoMapper;

namespace TIPIESProj.DataBase.Services.Mappers
{
    public class MapperConfig
    {
        public Mapper GetMapper<T>()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<T, T>());
            return new Mapper(config);
        }
    }
}
