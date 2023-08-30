using System.Text.Json;
using System.Text;

namespace Ecommerce.Shared.Utils
{
    public static class Utils
    {
        public static byte[] Serializer<T>(T data, JsonSerializerOptions? options = null)
        {
            if (options == null)
            {
                options = new JsonSerializerOptions();
            }
            var json = JsonSerializer.Serialize(data, options);
            return Encoding.ASCII.GetBytes(json);
        }
    }
}
