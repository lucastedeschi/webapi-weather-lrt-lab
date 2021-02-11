using System;
using System.Collections.Generic;
using FluentValidation;

namespace WeatherLrt.Application.Extensions
{
    public static class FluentValidationExtension
    {
        private const string Comma = ", ";

        public static IRuleBuilderOptions<T, TProperty> SubsetOf<T, TProperty, TEnum>(this IRuleBuilder<T, TProperty> ruleBuilder, TEnum enumeration) where TEnum : Type where TProperty : IEnumerable<char>
        {
            return ruleBuilder.Must((root, item, ctx) =>
            {
                var enumerationNames = Enum.GetNames(enumeration);
                ctx.MessageFormatter.AppendArgument("ExpectedElements", string.Join(Comma, enumerationNames));

                return Array.IndexOf(enumerationNames, item) > -1;
            })
            .WithMessage("{PropertyName} must be a subset of {ExpectedElements} items.");
        }
    }
}
