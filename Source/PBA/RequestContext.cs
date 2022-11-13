namespace PBA
{
    public class RequestContext
    {
        public object Identity { get; set; }
        public bool Success { get; set; }

        public void GrantAccess() => Success = true;
    }
}
