using MicroPermissions.MemoryRegistry;
using MicroPermissions.Tests.Foundation.Abstract;
using System.Threading.Tasks;
using Xunit;

namespace MicroPermissions.Tests.Foundation
{
    public class FilterTests
    {
        [Fact]
        public async Task OneIncrementTest()
        {
            var registry = new MemoryMicroPermissionsRegistry<PermissionContext>();
            var context = new PermissionContext();

            registry.RegisterFilter<IncrementFilter, int>();

            var controller = new PermissionController<PermissionContext>(registry, context);
            int value = await controller.FilterAsync(0);

            Assert.Equal(1, value);
        }

        [Fact]
        public async Task ThreeIncrementTest()
        {
            var registry = new MemoryMicroPermissionsRegistry<PermissionContext>();
            var context = new PermissionContext();

            registry.RegisterFilter<IncrementFilter, int>();
            registry.RegisterFilter<IncrementFilter, int>();
            registry.RegisterFilter<IncrementFilter, int>();

            var controller = new PermissionController<PermissionContext>(registry, context);
            int value = await controller.FilterAsync(1);

            Assert.Equal(4, value);
        }

        [Fact]
        public async Task NoneIncrementTest()
        {
            var registry = new MemoryMicroPermissionsRegistry<PermissionContext>();
            var context = new PermissionContext();
            var controller = new PermissionController<PermissionContext>(registry, context);
            controller.ThrowIfNotHandled = false;

            int value = await controller.FilterAsync(7);

            Assert.Equal(7, value);
        }
    }
}
