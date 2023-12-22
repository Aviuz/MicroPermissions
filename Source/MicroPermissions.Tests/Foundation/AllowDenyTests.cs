using MicroPermissions.MemoryRegistry;
using MicroPermissions.Tests.Foundation.Abstract;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MicroPermissions.Tests.Foundation
{
    public class AllowDenyTests
    {
        [Fact]
        public async Task TestSimpleAllow()
        {
            var registry = new MemoryMicroPermissionsRegistry<PermissionContext>();
            var context = new PermissionContext();

            registry.RegisterHandler<EmptyPositiveHandler, EmptyRequest>();

            var controller = new PermissionController<PermissionContext>(registry, context, new());

            Assert.True(await controller.IsGrantedAsync(new EmptyRequest()));
        }

        [Fact]
        public async Task TestAllowAndNeutral()
        {
            var registry = new MemoryMicroPermissionsRegistry<PermissionContext>();
            var context = new PermissionContext();

            registry.RegisterHandler<EmptyPositiveHandler, EmptyRequest>();
            registry.RegisterHandler<EmptyNeutralHandler, EmptyRequest>();

            var controller = new PermissionController<PermissionContext>(registry, context, new());

            Assert.True(await controller.IsGrantedAsync(new EmptyRequest()));
        }

        [Fact]
        public async Task TestDeny()
        {
            var registry = new MemoryMicroPermissionsRegistry<PermissionContext>();
            var context = new PermissionContext();

            registry.RegisterHandler<EmptyNegativeHandler, EmptyRequest>();

            var controller = new PermissionController<PermissionContext>(registry, context, new());

            Assert.False(await controller.IsGrantedAsync(new EmptyRequest()));
        }

        [Fact]
        public async Task TestDenyAndAllow()
        {
            var registry = new MemoryMicroPermissionsRegistry<PermissionContext>();
            var context = new PermissionContext();

            registry.RegisterHandler<EmptyNegativeHandler, EmptyRequest>();
            registry.RegisterHandler<EmptyPositiveHandler, EmptyRequest>();

            var controller = new PermissionController<PermissionContext>(registry, context, new());

            Assert.False(await controller.IsGrantedAsync(new EmptyRequest()));
        }

        [Fact]
        public async Task TestAllowAndDeny()
        {
            var registry = new MemoryMicroPermissionsRegistry<PermissionContext>();
            var context = new PermissionContext();

            registry.RegisterHandler<EmptyPositiveHandler, EmptyRequest>();
            registry.RegisterHandler<EmptyNegativeHandler, EmptyRequest>();

            var controller = new PermissionController<PermissionContext>(registry, context, new());

            Assert.False(await controller.IsGrantedAsync(new EmptyRequest()));
        }

        [Fact]
        public async Task TestDenyAndDeny()
        {
            var registry = new MemoryMicroPermissionsRegistry<PermissionContext>();
            var context = new PermissionContext();

            registry.RegisterHandler<EmptyNegativeHandler, EmptyRequest>();
            registry.RegisterHandler<EmptyNegativeHandler, EmptyRequest>();

            var controller = new PermissionController<PermissionContext>(registry, context, new());

            Assert.False(await controller.IsGrantedAsync(new EmptyRequest()));
        }

        [Fact]
        public async Task TestAllowNeutralAndDeny()
        {
            var registry = new MemoryMicroPermissionsRegistry<PermissionContext>();
            var context = new PermissionContext();

            registry.RegisterHandler<EmptyPositiveHandler, EmptyRequest>();
            registry.RegisterHandler<EmptyNeutralHandler, EmptyRequest>();
            registry.RegisterHandler<EmptyNegativeHandler, EmptyRequest>();

            var controller = new PermissionController<PermissionContext>(registry, context, new());

            Assert.False(await controller.IsGrantedAsync(new EmptyRequest()));
        }
    }
}
