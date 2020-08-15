using System.Collections.Generic;

namespace ManagerAPI.Domain.Entities
{
    public static class EntityStringBuilder
    {
        public static string BuildString<T>(T entity, params string[] properties)
        {
            var type = entity.GetType();
            var values = new List<string>();
            foreach (var prop in properties)
            {
                var property = type.GetProperty(prop);

                if (property != null)
                {
                    values.Add(property.GetValue(entity)?.ToString());
                }
            }

            return string.Join(", ", values);
        }
    }
}