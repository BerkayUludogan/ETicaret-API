using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;

namespace ETicaret.Mapper.AutoMapper
{
    public class Mapper : Application.Abstractions.AutoMapper.IMapper
    {
        private readonly IMapper _mapper;

        public Mapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(Mapper).Assembly); // Profil'leri otomatik yükler

            }, NullLoggerFactory.Instance);

            try
            {
                config.AssertConfigurationIsValid();// Validasyon yapılıyor mappingler doğru mu
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            _mapper = config.CreateMapper();
        }

        public TDestination Map<TDestination, TSource>(TSource source)
            => _mapper.Map<TSource, TDestination>(source);

        public IList<TDestination> Map<TDestination, TSource>(IList<TSource> sources)
            => _mapper.Map<IList<TSource>, IList<TDestination>>(sources);

        public TDestination Map<TDestination>(object source)
            => _mapper.Map<TDestination>(source);

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
           => _mapper.Map(source, destination);
    }
}
