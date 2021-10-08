using System;

namespace Blog.Application.Common.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string name, object key)
            : base($"Resource  \'{name}\' (via key {key}) was not found.)")
        {
        }
    }
}