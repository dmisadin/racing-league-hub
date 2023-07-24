using AutoMapper;

namespace F1StatsServer.Util
{
    public static class MyMapper<TDestination, TSource>
    {
        private static readonly Mapper _myMapper = new(new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();
            }
            ));


        public static TDestination Map(TSource source)
        {
            return _myMapper.Map<TSource, TDestination>(source);
        }

        public static List<TDestination> MapList(List<TSource> source)
        {
            return _myMapper.Map<List<TSource>, List<TDestination>>(source);
        }
    }
}
