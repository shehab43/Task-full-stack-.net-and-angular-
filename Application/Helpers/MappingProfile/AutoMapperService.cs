using AutoMapper.QueryableExtensions;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers.MappingProfile
{
   public static class AutoMapperService
    {
        public static IMapper Mapper { get; set; }

        public static T Map<T>(this object Source)
        {
            return Mapper.Map<T>(Source);
        }

        public static IQueryable<T> Project<T>(this IQueryable<Object> Source)
        {
            return Source.ProjectTo<T>(Mapper.ConfigurationProvider);
        }
    }
}
