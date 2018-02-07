﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerApp.Helpers.ResourcesHelpers.Implementations
{
    public static class ResourceHelper
    {
        public static string GetResourceValue(Type resourceType, string resourceName)
        {
            if (resourceType == null) throw new ArgumentNullException("ResourceType jest nullem.");
            if (String.IsNullOrWhiteSpace(resourceName)) throw new ArgumentException("ResourceName ma nieprawidłową wartość (jest nullem, zawiera same białe znaki lub jest pustym stringiem).");


            PropertyInfo property = resourceType.GetProperty(resourceName, BindingFlags.Public | BindingFlags.Static);
            if (property == null)
            {
                throw new InvalidOperationException("Zasób nie zawiera właściwości podanej nazwie.");
            }
            if (property.PropertyType != typeof(string))
            {
                throw new InvalidOperationException("Właściwość zasobu nie jest typem string.");
            }
            return (string)property.GetValue(null, null); 
        }
    }
}
