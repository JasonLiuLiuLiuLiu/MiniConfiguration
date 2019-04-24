using System;
using Microsoft.Extensions.Primitives;

namespace ChangeTokenPrinciple
{
    /// <summary>
    /// Propagates notifications that a change has occurred.
    /// </summary>
    public static class ChangeToken
    {
        public static ChangeTokenRegistration<Action> OnChange(Func<IChangeToken> changeTokenProducer, Action changeTokenConsumer)
        {
            return new ChangeTokenRegistration<Action>(changeTokenProducer, callback => callback(), changeTokenConsumer);
        }
    }
    public class ChangeTokenRegistration<TAction>
    {
        private readonly Func<IChangeToken> _changeTokenProducer;
        private readonly Action<TAction> _changeTokenConsumer;
        private readonly TAction _state;

        public ChangeTokenRegistration(Func<IChangeToken> changeTokenProducer, Action<TAction> changeTokenConsumer, TAction state)
        {
            _changeTokenProducer = changeTokenProducer;
            _changeTokenConsumer = changeTokenConsumer;
            _state = state;

            var token = changeTokenProducer();

            RegisterChangeTokenCallback(token);
        }

        private void OnChangeTokenFired()
        {
            var token = _changeTokenProducer();

            try
            {
                _changeTokenConsumer(_state);
            }
            finally
            {
                // We always want to ensure the callback is registered
                RegisterChangeTokenCallback(token);
            }
        }
        private void RegisterChangeTokenCallback(IChangeToken token)
        {
            token.RegisterChangeCallback(_ => OnChangeTokenFired(), this);
        }
    }
}
