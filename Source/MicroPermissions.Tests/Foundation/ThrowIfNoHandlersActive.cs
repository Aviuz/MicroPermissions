using MicroPermissions.MemoryRegistry;
using MicroPermissions.Tests.Foundation.Abstract;
using System.Threading.Tasks;
using Xunit;

namespace MicroPermissions.Tests.Foundation
{
    public class ThrowIfNoHandlersActive
    {
        [Fact]
        public async Task NoHandler_Throw_If_ThrowSetToTrue()
        {
            var registry = new MemoryMicroPermissionsRegistry<PermissionContext>();
            var context = new PermissionContext();
            var controller = new PermissionController<PermissionContext>(registry, context, new() { ThrowIfUnhandled = true });

            await Assert.ThrowsAsync<NotHandledPermissionRequestException>(async () => await controller.IsGrantedAsync(new EmptyRequest()));
        }

        [Fact]
        public async Task NoHandler_Ignore_If_ThrowSetToFalse()
        {
            var registry = new MemoryMicroPermissionsRegistry<PermissionContext>();
            var context = new PermissionContext();
            var controller = new PermissionController<PermissionContext>(registry, context, new() { ThrowIfUnhandled = false });

            Assert.False(await controller.IsGrantedAsync(new EmptyRequest()));
        }

        [Fact]
        public async Task NoFilter_Throw_If_ThrowSetToTrue()
        {
            var registry = new MemoryMicroPermissionsRegistry<PermissionContext>();
            var context = new PermissionContext();
            var controller = new PermissionController<PermissionContext>(registry, context, new() { ThrowIfUnhandled = true });

            await Assert.ThrowsAsync<NotHandledPermissionFilterException>(async () => await controller.FilterAsync(1));
        }

        [Fact]
        public async Task EmptyFilter_Throw_If_ThrowSetToTrue()
        {
            var registry = new MemoryMicroPermissionsRegistry<PermissionContext>();
            registry.RegisterFilter<EmptyFilter, int>();

            var context = new PermissionContext();
            var controller = new PermissionController<PermissionContext>(registry, context, new() { ThrowIfUnhandled = true });

            await Assert.ThrowsAsync<NotHandledPermissionFilterException>(async () => await controller.FilterAsync(1));
        }

        [Fact]
        public async Task NoFilter_Ignore_If_ThrowSetToFalse()
        {
            var registry = new MemoryMicroPermissionsRegistry<PermissionContext>();
            var context = new PermissionContext();
            var controller = new PermissionController<PermissionContext>(registry, context, new() { ThrowIfUnhandled = false });

            Assert.Equal(1, await controller.FilterAsync(1));
        }
    }
}
