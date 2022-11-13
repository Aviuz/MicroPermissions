using PBA.Abstract;
using System;

namespace PBA.Requests
{
    public class CreateDatabaseObjectRequest : IPermissionRequest
    {
        public Type Type { get; set; }
        public object Object { get; set; }
    }
}
