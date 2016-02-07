//namespace MySurveys.Web.Infrastructure.Mapping
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Reflection;
//    using AutoMapper;

//    public static class AutoMapperConfig
//    {
//        public static void Execute()
//        {
//            var types = Assembly.GetCallingAssembly().GetExportedTypes();

//            LoadStandardMappings(types);
//        }

//        private static void LoadStandardMappings(IEnumerable<Type> types)
//        {
//            IConfiguration conf;
//            var maps = from t in types
//                       from i in t.GetInterfaces()
//                       where
//                           i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>) && !t.IsAbstract
//                           && !t.IsInterface
//                       select new { Source = i.GetGenericArguments()[0], Destination = t };

//            foreach (var map in maps)
//            {
//                conf = new MapperConfiguration(b => b.CreateMap(map.Source, map.Destination));
//            }
//        }
//    }
//}