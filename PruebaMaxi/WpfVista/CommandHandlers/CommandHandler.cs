﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfVista.CommandHandlers
{
    public class CommandHandler : ICommand
    {
        //private Action _action;

        //public CommandHandler(Action action)
        //{
        //    _action = action;
        //}

        //public event EventHandler CanExecuteChanged
        //{
        //    add { CommandManager.RequerySuggested += value; }
        //    remove { CommandManager.RequerySuggested -= value; }
        //}

        //public bool CanExecute(object parameter)
        //{
        //    return true;
        //}

        //public void Execute(object parameter)
        //{
        //    _action();
        //}

        private Action<object> _action;
        private bool _canExecute;
        public CommandHandler(Action<object> action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action(parameter);
        }
    }
}
