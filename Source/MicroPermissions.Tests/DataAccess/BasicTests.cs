using MicroPermissions.DataAccess;
using MicroPermissions.DataAccess.Requests;
using MicroPermissions.Tests.DataAccess.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MicroPermissions.Tests.DataAccess
{
    public class CustomPermissionContextTests : DataAccessTest
    {
        [Fact]
        public async Task Test1Async()
        {
            LoginAs("admin");
            var controller = BuildController(builder =>
            {
                builder.AllowCreate<Entity>((ctx, entity) => ctx.UserName == "admin" && entity.Name == "resource");
            });

            var request = PermissionRequests.Create(new Entity() { Name = "resource" });

            bool isGranted = await controller.IsGrantedAsync(request);

            Assert.True(isGranted);
        }

        [Fact]
        public async Task Test12Async()
        {
            LoginAs("user");
            var controller = BuildController(builder =>
            {
                builder.AllowCreate<Entity>((ctx, entity) => ctx.UserName == "admin" && entity.Name == "resource");
            });

            var request = PermissionRequests.Create(new Entity() { Name = "resource" });

            bool isGranted = await controller.IsGrantedAsync(request);

            Assert.False(isGranted);
        }
    }

    public class FilterQuery : DataAccessTest
    {
        List<QueryObject> Items = new List<QueryObject> {
             new QueryObject("Book", "John"),
             new QueryObject("Broom", "George"),
             new QueryObject("Fork", "Sully"),
             new QueryObject("Knife", "John"),
             new QueryObject("Rugsack", "George"),
         };

        [Fact]
        public async Task FilterOnlyJohnsBooks()
        {
            LoginAs("John");
            var controller = BuildController(builder =>
            {
                builder.AllowRead<QueryObject>(ctx => entity => ctx.UserName == entity.Owner);
            });

            var filteredData = await Items.AsQueryable().WhereHasAccessAsync(controller);
            Assert.NotNull(filteredData);

            var array = filteredData.ToArray();
            Assert.Equal(2, array.Length);
            Assert.Equal("Book", array[0].Name);
            Assert.Equal("Knife", array[1].Name);
        }

        [Fact]
        public async Task ReadAll()
        {
            LoginAs("John");
            var controller = BuildController(builder =>
            {
                builder.AllowRead<QueryObject>(ctx => entity => true);
            });

            var filteredData = await Items.AsQueryable().WhereHasAccessAsync(controller);
            Assert.NotNull(filteredData);

            var array = filteredData.ToArray();
            Assert.Equal(5, array.Length);
        }

        class QueryObject
        {
            public string Name { get; set; }
            public string Owner { get; set; }

            public QueryObject(string name, string owner)
            {
                Name = name;
                Owner = owner;
            }
        }
    }
}
