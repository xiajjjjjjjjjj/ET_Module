using ETModel;
using ETModel.Module.AspNetCore;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;

namespace ETHotfix.Module.AspNetCore
{
    [ObjectSystem]
    public class AspNetCoreComponent_LoadSystem : LoadSystem<AspNetCoreComponent>
    {
        public override void Load(AspNetCoreComponent self)
        {
            ApplicationPartManager partManager = self.ServiceProvider.GetService<ApplicationPartManager>();

            partManager.ApplicationParts.Clear();

            partManager.ApplicationParts.Add(new AssemblyPart(Game.EventSystem.Get(DLLType.Hotfix)));

            ETActionDescriptorChangeProvider.Instance.HasChanged = true;

            ETActionDescriptorChangeProvider.Instance.TokenSource.Cancel();
        }
    }
}