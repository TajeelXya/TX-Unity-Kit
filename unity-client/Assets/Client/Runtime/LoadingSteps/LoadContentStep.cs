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
        private IContentLoader _contentLoader;

        public void Inject(IResolver resolver)
        {
            _contentService = resolver.Resolve<IContentService>();
            _contentLoader = resolver.Resolve<IContentLoader>();
        }

        public async override UniTask InitialiseAsync(CancellationToken cToken = default)
        {
            await _contentLoader.LoadContentAsync(new[] { "mg_content", "ev_content" }, cToken);
            var mgDemo = _contentService.GetAllData<DemoContentData>().ToArray();

            await _contentLoader.UnloadContentAsync(new[] { "mg_content" }, cToken);
            var demo = _contentService.GetAllData<DemoContentData>().ToArray();
        }
    }
}