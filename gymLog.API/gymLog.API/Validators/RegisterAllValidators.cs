using FluentValidation;
using System.Reflection;

namespace gymLog.API.Validators
{


    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAllValidators(this IServiceCollection services, Assembly assembly)
        {
            var validatorTypes = assembly.GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface)
                .Where(t => t.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>)));

            foreach (var type in validatorTypes)
            {
                var interfaceType = type.GetInterfaces()
                    .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>));
                services.AddTransient(interfaceType, type);
            }

            return services;
        }
    }


}
