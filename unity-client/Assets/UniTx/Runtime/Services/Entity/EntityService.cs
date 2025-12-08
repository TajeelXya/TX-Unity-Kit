using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UniTx.Runtime.IoC;

namespace UniTx.Runtime.Services
{
    public sealed class EntityService : IService, IEntityService, IInjectable
    {
        private readonly Dictionary<string, IEntity> _registry = new();

        private IContentService _contentService;
        private IResolver _resolver;

        public void Inject(IResolver resolver)
        {
            _contentService = resolver.Resolve<IContentService>();
            _resolver = resolver;
        }

        public UniTask InitialiseAsync(CancellationToken cToken = default)
        {
            var data = _contentService.GetAllData<IEntityData>();

            foreach (var datum in data)
            {
                var entity = datum.CreateEntity();
                entity.Inject(_resolver);
                entity.Initialise();
                _registry[entity.Id] = entity;
            }

            return UniTask.CompletedTask;
        }

        public void Reset()
        {
            foreach (var entity in _registry.Values)
            {
                entity.Reset();
            }

            _registry.Clear();
        }

        public TEntity Get<TEntity>(string id)
            where TEntity : IEntity
        {
            if (_registry.TryGetValue(id, out var asset) && asset is TEntity typedAsset)
            {
                return typedAsset;
            }

            throw new KeyNotFoundException($"Entity with Id '{id}' not found.");
        }

        public IEnumerable<TEntity> GetAll<TEntity>()
            where TEntity : IEntity
            => _registry.Values.OfType<TEntity>();
    }
}