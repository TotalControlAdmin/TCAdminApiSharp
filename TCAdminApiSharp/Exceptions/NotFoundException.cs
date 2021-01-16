using System;

namespace TCAdminApiSharp.Exceptions
{
    public class NotFoundException : Exception
    {
        public readonly Type Type;
        public readonly int[] Ids;
        
        public NotFoundException(Type type, params int[] ids) : base($"The {type.FullName} could not be found with ids: [{string.Join(", ", ids)}]")
        {
            this.Type = type;
            Ids = ids;
        }

        public NotFoundException(Type type, Exception innerException, int[] ids) : base($"The {type.FullName} could not be found", innerException)
        {
            this.Type = type;
            Ids = ids;
        }
    }
}