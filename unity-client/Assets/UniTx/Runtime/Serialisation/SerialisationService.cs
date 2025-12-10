using DG.Tweening;
using System;
using UnityEngine;
using UniTx.Runtime.IoC;

namespace UniTx.Runtime.Serialisation
{
    public class SerialisationService : ISerialisationService, IInjectable, IInitialisable, IResettable
    {
        private readonly Serialiser _serialiser = new();

        private Tween _saveTween;

        public void Inject(IResolver resolver) => _serialiser.Inject(resolver);

        public void Initialise()
        {
            var interval = UniStatics.Config.SaveInterval;
            _saveTween = DOVirtual.DelayedCall(interval, _serialiser.SerialiseDirty, false).SetLoops(-1);
        }

        public void Reset()
        {
            _saveTween.Kill();
            _serialiser.Reset();
        }

        public void Save(ISavedData data)
        {
            if (data?.Id == null)
            {
                UniStatics.LogInfo("Failed to save data because data or data Id is null.", this, Color.red);
                return;
            }

            _serialiser.MarkDirty(data);
        }

        public T Load<T>(string id)
            where T : ISavedData, new()
        {
            if (id == null)
            {
                UniStatics.LogInfo("Failed to load data because data Id is null.", this, Color.red);
                throw new ArgumentNullException(nameof(id));
            }

            return _serialiser.Deserialise<T>(id);
        }
    }
}