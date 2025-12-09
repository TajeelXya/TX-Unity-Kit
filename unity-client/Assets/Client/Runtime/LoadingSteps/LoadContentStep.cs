using Cysharp.Threading.Tasks;
using System.Linq;
using System.Threading;
using UniTx.Runtime.Bootstrap;
using UniTx.Runtime.Content;
using UniTx.Runtime.IoC;

namespace Client.Runtime
{
    public sealed class LoadContentStep : LoadingStepBase, IInjectable
    {
        private IContentService _contentService;

        public void Inject(IResolver resolver)
        {
            _contentService = resolver.Resolve<IContentService>();
        }

        public async override UniTask InitialiseAsync(CancellationToken cToken = default)
        {
            await _contentService.LoadContentAsync(new[] { "mg_content", "ev_content" }, cToken);
            var mgDemo = _contentService.GetAllData<DemoContentData>().ToArray();

            await _contentService.UnloadContentAsync(new[] { "mg_content" }, cToken);
            var demo = _contentService.GetAllData<DemoContentData>().ToArray();
        }
    }
}