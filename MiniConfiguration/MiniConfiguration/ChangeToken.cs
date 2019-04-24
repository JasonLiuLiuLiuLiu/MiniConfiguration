using System;
using Microsoft.Extensions.Primitives;

namespace ChangeTokenPrinciple
{
    /// <summary>
    /// Propagates notifications that a change has occurred.
    /// </summary>
    public static class ChangeToken
    {
        /// <summary>
        /// Registers the <paramref name="changeTokenConsumer"/> action to be called whenever the token produced changes.
        /// </summary>
        /// <param name="changeTokenProducer">Produces the change token.</param>
        /// <param name="changeTokenConsumer">Action called when the token changes.</param>
        /// <returns></returns>
        public static ChangeTokenRegistration<Action> OnChange(Func<IChangeToken> changeTokenProducer, Action changeTokenConsumer)
        {
            if (changeTokenProducer == null)
            {
                throw new ArgumentNullException(nameof(changeTokenProducer));
            }
            if (changeTokenConsumer == null)
            {
                throw new ArgumentNullException(nameof(changeTokenConsumer));
            }

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
