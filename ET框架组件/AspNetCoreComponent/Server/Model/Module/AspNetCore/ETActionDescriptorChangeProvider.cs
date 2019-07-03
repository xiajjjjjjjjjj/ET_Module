using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Primitives;
using System.Threading;

namespace ETModel.Module.AspNetCore
{
    public class ETActionDescriptorChangeProvider : IActionDescriptorChangeProvider
    {
        public static ETActionDescriptorChangeProvider Instance { get; } = new ETActionDescriptorChangeProvider();

        public CancellationTokenSource TokenSource { get; private set; }

        public bool HasChanged { get; set; }

        public IChangeToken GetChangeToken()
        {
            TokenSource = new CancellationTokenSource();
            
            return new CancellationChangeToken(TokenSource.Token);
        }
    }
}