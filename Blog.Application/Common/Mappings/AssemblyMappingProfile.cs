using System;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace Blog.Application.Common.Mappings
{
    public class AssemblyMappingProfile : Profile
    {
        public AssemblyMappingProfile(Assembly assembly)
        {
            CreateMappingFromAssembly(assembly);
        }

        private void CreateMappingFromAssembly(Assembly assembly)
        {
            var isMapFrom = new Func<Type, bool>(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>));

            var mapTypes = assembly.GetExportedTypes()
                .Where(type => type.GetInterfaces().Any(isMapFrom));

            foreach (var type in mapTypes)
            {
                var iMapFrom = type.GetInterfaces().First(isMapFrom);
                var genericType = iMapFrom.GetGenericArguments()[0];
                var instance = Activator.CreateInstance(type);

                // Get method from the instance, if it's null use the default implementation  from the interface. 
                var method = type.GetMethod("Map") ??
                             typeof(IMapFrom<>).MakeGenericType(genericType).GetMethod("Map");

                // method can not be null, because type does implement the interface and it has a default implementation
                method!.Invoke(instance, new object[] {this});
            }
        }
    }
}