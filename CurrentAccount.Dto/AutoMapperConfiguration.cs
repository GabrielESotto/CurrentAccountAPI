using AutoMapper;

namespace CurrentAccount.Dto
{
    public static class AutoMapperConfiguration
    {
        public static MapperConfiguration RegisterMappings()
        {
            var config = new MapperConfiguration(ps =>
            {

            });

            return config;
        }
    }
}
