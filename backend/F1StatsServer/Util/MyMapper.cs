using AutoMapper;
using F1StatsServer.Dto;
using F1StatsServer.Model;

namespace F1StatsServer.Util
{
    public static class MyMapper<TDestination, TSource>
    {
        private static Mapper _myMapper = new Mapper(new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();
                cfg.CreateMap<GrandPrix, GrandPrixDto>();
            }
            ));


        public static TDestination Map(TSource source)
        {
            return _myMapper.Map<TSource, TDestination>(source);
        }

        public static GrandPrix Map(GrandPrixDto source)
        {
            return new GrandPrix{
                Name = source.Name,
                SeasonId = source.SeasonId,
                HasSprint = source.HasSprint,
                YouTubeUrl = source.YoutubeUrl
            };
        }

        public static List<TDestination> MapList(List<TSource> source)
        {
            return _myMapper.Map<List<TSource>, List<TDestination>>(source);
        }
    }
}
