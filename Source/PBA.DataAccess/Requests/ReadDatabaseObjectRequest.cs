using PBA.Abstract;
using System;

namespace PBA.Requests
{
    public class ReadDatabaseObjectRequest : IPermissionRequest
    {
        public Type Type { get; set; }
        public object Object { get; set; }
    }
}
