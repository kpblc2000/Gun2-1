using Gun2Core.Infrastructure.Commands.Base;
using System;

namespace Gun2Core.Infrastructure.Commands
{
    internal class RelayCommand : Command
    {

        public RelayCommand(Action<object> Execute, Func<object, bool> CanExecute = null)
        {
            _Execute = Execute ?? throw new ArgumentNullException(nameof(Execute));
            _CanExecute = CanExecute;
        }

        public override bool CanExecute(object parameter)
        {
            return _CanExecute?.Invoke(parameter) ?? true;
        }

        public override void Execute(object parameter)
        {
            _Execute(parameter);
        }

        private Action<object> _Execute;
        private Func<object, bool> _CanExecute;
    }
}
