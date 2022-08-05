using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.Library.Domain.Utils
{
    internal class MapperService
    {
        private readonly TypeAdapterConfig m_typeAdapterConfig;

        public MapperService()
        {
            m_typeAdapterConfig = new TypeAdapterConfig();
        }

        public IMapperConfig<TSource, TDestination> CreateMap<TSource, TDestination>() 
        {
            return new MapperConfig<TSource, TDestination>(m_typeAdapterConfig.ForType<TSource, TDestination>());
        }

        public TDestination To<TDestination>(object src)
        {
            return src.Adapt<TDestination>(m_typeAdapterConfig);
        }
    }

    internal class MapperConfig<TSource, TDestination> : IMapperConfig<TSource, TDestination>
    {
        private readonly TypeAdapterSetter<TSource, TDestination> m_ExternalConfigInstance;

        public MapperConfig(TypeAdapterSetter<TSource,TDestination> ins)
        {
            m_ExternalConfigInstance = ins;
        }

        public IMapperConfig<TSource, TDestination> AfterMapping(Action<TSource, TDestination> action)
        {
            m_ExternalConfigInstance.AfterMapping(action);
            return this;
        }

        public IMapperConfig<TSource, TDestination> AfterMapping(Action<TDestination> action)
        {
            m_ExternalConfigInstance.AfterMapping(action);
            return this;
        }
    }

    interface IMapperConfig<TSource,TDestination>
    {
        IMapperConfig<TSource, TDestination> AfterMapping(Action<TSource,TDestination> action);

        IMapperConfig<TSource, TDestination> AfterMapping(Action<TDestination> action);
    }
}
