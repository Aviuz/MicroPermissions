using PBA.Abstract;
using System;

namespace PBA.Requests
{
    public class UpdateDatabaseObjectRequest : IPermissionRequest
    {
        public Type Type { get; set; }
        public object OldValue { get; set; }
        public object NewValue { get; set; }
    }
}
