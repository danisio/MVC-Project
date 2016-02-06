namespace MySurveys.Web.Infrastructure.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using AutoMapper;

    public static class AutoMapperConfig
    {
        public static void Execute()
        {
            var types = Assembly.GetCallingAssembly().GetExportedTypes();

            LoadStandardMappings(types);

            LoadCustomMappings(types);
        }

        private static void LoadStandardMappings(IEnumerable<Type> types)
        {
            var maps = from t in types
                       from i in t.GetInterfaces()
                       where
                           i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>) && !t.IsAbstract
                           && !t.IsInterface
                       select new { Source = i.GetGenericArguments()[0], Destination = t };

            foreach (var map in maps)
            {
                //// var conf = new MapperConfiguration(b=>b.CreateMap(map.Source, map.Destination)); 
                Mapper.CreateMap(map.Source, map.Destination); // TODO 
            }
        }

        private static void LoadCustomMappings(IEnumerable<Type> types)
        {
            var maps = from t in types
                       from i in t.GetInterfaces()
                       where typeof(IHaveCustomMappings).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface
                       select (IHaveCustomMappings)Activator.CreateInstance(t);

            foreach (var map in maps)
            {
                map.CreateMappings(Mapper.Configuration);
            }
        }
    }
}