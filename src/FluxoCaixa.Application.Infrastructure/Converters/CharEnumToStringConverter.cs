using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FluxoCaixa.Application.Infrastructure.Converters
{
    public class CharEnumToStringConverter<TEnum> : ValueConverter<TEnum, string> where TEnum : struct, Enum
    {
        public CharEnumToStringConverter()
            : base(
                enumValue => ToProvider(enumValue),
                stringValue => FromProvider(stringValue))
        {
        }

        private static string ToProvider(TEnum @enum)
        {
            // Converte o enum para string
            return @enum.ToString();
        }

        private static TEnum FromProvider(string value)
        {
            // Converte a string de volta para o enum
            return Enum.Parse<TEnum>(value);
        }
    }

}
