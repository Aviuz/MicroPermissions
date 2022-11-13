using MicroPermissions.DataAccess;
using MicroPermissions.DataAccess.Configuration;
using MicroPermissions.DataAccess.DataLayer;
using MicroPermissions.DataAccess.Requests;
using System.Threading.Tasks;
using Xunit;

namespace MicroPermissions.Tests
{
    public class CustomPermissionContextTests
    {
        public class Entity
        {
            public string Name { get; set; }
        }

        public class MyPermissionContext : PermissionContext, IDataLayerPermissionContext
        {
            public IDataAccessRuleSet DataAccessRuleSet { get; set; }

            public string UserName { get; set; }

            public Task<IDataAccessRuleSet> GetRuleSetsAsync() => Task.FromResult(DataAccessRuleSet);
        }

        public class MyPermissionController : DataLayerPermissionController<MyPermissionContext>
        {
            public MyPermissionController(IPermissionHandlersRegistry registry) : base(registry) { }
        }

        public class MyPermissionRequest : IPermissionRequest
        {
            public string Name { get; set; }
        }

        public class MyPermissionHandler : IPermissionHandler<MyPermissionRequest>
        {
            public Task HandleRequestAsync(PermissionContext context, MyPermissionRequest request)
            {
                return Task.CompletedTask;
            }
        }

        [Fact]
        public async Task Test1Async()
        {
            var builder = new DictionaryRuleSetBuilder<MyPermissionContext>();
            builder.AllowCreate<Entity>((ctx, entity) => ctx.UserName == "admin" && entity.Name == "resource");

            var registry = new MemoryPermissionHandlerRegistry();
            registry.AddDataLayerModule();
            var context = new MyPermissionContext() { UserName = "admin", DataAccessRuleSet = builder.Build() };
            var controller = new MyPermissionController(registry);

            var request = PermissionRequests.Create(new Entity() { Name = "resource" });

            bool isGranted = await controller.IsGrantedAsync(context, request);

            Assert.True(isGranted);
        }

        [Fact]
        public async Task Test12Async()
        {
            var builder = new DictionaryRuleSetBuilder<MyPermissionContext>();
            builder.AllowCreate<Entity>((ctx, entity) => ctx.UserName == "admin" && entity.Name == "resource");

            var registry = new MemoryPermissionHandlerRegistry();
            registry.AddDataLayerModule();
            var context = new MyPermissionContext() { UserName = "user", DataAccessRuleSet = builder.Build() };
            var controller = new MyPermissionController(registry);

            var request = PermissionRequests.Create(new Entity() { Name = "resource" });

            bool isGranted = await controller.IsGrantedAsync(context, request);

            Assert.False(isGranted);
        }
    }
}
