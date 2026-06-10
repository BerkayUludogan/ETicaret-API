using AutoMapper;
using AutoMapperIMapper = AutoMapper.IMapper; 
using Microsoft.Extensions.Logging.Abstractions;

namespace ETicaret.Mapper.AutoMapper
{
    public class Mapper : ETicaret.Application.Common.Abstractions.AutoMapper.IMapper
    {
        private readonly AutoMapperIMapper _mapper;

        public Mapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(Mapper).Assembly);
            }, NullLoggerFactory.Instance);

            config.AssertConfigurationIsValid();

            _mapper = config.CreateMapper();
        }

        public TDestination Map<TDestination, TSource>(TSource source)
            => _mapper.Map<TDestination>(source);

        public IList<TDestination> Map<TDestination, TSource>(IList<TSource> sources)
            => _mapper.Map<IList<TDestination>>(sources);

        public TDestination Map<TDestination>(object source)
            => _mapper.Map<TDestination>(source);

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
            => _mapper.Map(source, destination);
    }
}