using AutoMapper; 

namespace Blog.Application.Common.Mappings
{
    public interface IMapFrom<T>
    {
        void Map(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}