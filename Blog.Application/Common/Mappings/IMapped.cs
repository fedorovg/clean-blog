using AutoMapper;

namespace Blog.Application.Common.Mappings
{
    public interface IMapped<T>
    {
        void Map(Profile profile)
        {
            profile.CreateMap(typeof(T), GetType());
            profile.CreateMap(GetType(), typeof(T));
        }
    }
}