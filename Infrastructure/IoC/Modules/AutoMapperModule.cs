using Application.CQRS;
using Autofac;
using AutoMapper;
using System.Reflection;

namespace Infrastructure.IoC.Modules;

public class AutoMapperModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var assembly = new[] { typeof(MediatRAutoFacMarker).Assembly };


        var allTypes = assembly
                      .Where(a => !a.IsDynamic && a.GetName().Name != nameof(AutoMapper))
                      .Distinct()
                      .SelectMany(a => a.DefinedTypes)
                      .ToArray();

        var openTypes = new[] {
                            typeof(IValueResolver<,,>),
                            typeof(IMemberValueResolver<,,,>),
                            typeof(ITypeConverter<,>),
                            typeof(IValueConverter<,>),
                            typeof(IMappingAction<,>)
            };

        foreach (var type in openTypes.SelectMany(openType =>
         allTypes.Where(t => t.IsClass && !t.IsAbstract && ImplementsGenericInterface(t.AsType(), openType))))
        {
            builder.RegisterType(type.AsType()).InstancePerDependency();
        }

        builder.Register<IConfigurationProvider>(ctx => new MapperConfiguration(cfg => cfg.AddMaps(assembly))).SingleInstance();

        builder.Register<IMapper>(ctx => new Mapper(ctx.Resolve<IConfigurationProvider>(), ctx.Resolve)).InstancePerDependency();

    }

    private static bool ImplementsGenericInterface(Type type, Type interfaceType)
           => IsGenericType(type, interfaceType) || type.GetTypeInfo().ImplementedInterfaces.Any(@interface => IsGenericType(@interface, interfaceType));

    private static bool IsGenericType(Type type, Type genericType)
              => type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == genericType;

}

