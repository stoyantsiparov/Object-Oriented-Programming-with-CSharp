using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ValidationAttributes.Attributes;

namespace ValidationAttributes.Utils;

public static class Validator
{
    public static bool IsValid(object obj)
    {
        Type objType = obj.GetType();

        // Взимам всички пропертита които с анаследници на {MyValidationAttribute} (FullName и Age)
        PropertyInfo[] propertyInfos = objType
            .GetProperties()
            .Where(p => p.CustomAttributes
                .Any(ca => typeof(MyValidationAttribute).IsAssignableFrom(ca.AttributeType)))
            .ToArray();

        // Обикалям всички пропертита
        foreach (PropertyInfo propertyInfo in propertyInfos)
        {
            // Всимам кастнатите артибути върху даденото пропърти
            IEnumerable<MyValidationAttribute> attributes = propertyInfo
                .GetCustomAttributes()
                .Where(ca => typeof(MyValidationAttribute).IsAssignableFrom(ca.GetType()))
                .Cast<MyValidationAttribute>();

            // Обикалям всички атрибути
            foreach (MyValidationAttribute attribute in attributes)
            {
                // Проверявам всеки атрибут дали е валиден
                if (!attribute.IsValid(propertyInfo.GetValue(obj)))
                {
                    return false;
                }
            }
        }

        return true;
    }
}