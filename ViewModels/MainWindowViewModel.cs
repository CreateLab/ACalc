using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using ACalc.Models;
using ReactiveUI;

namespace ACalc.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        

        private double _firstValue;
        private double _secondValue;
        private Operation _operation = Operation.Add;


        public double ShownValue
        {
            get => _secondValue;
            set => this.RaiseAndSetIfChanged(ref _secondValue, value);
        }
        
        public ReactiveCommand<int, Unit> AddNumbCommand { get; }
        public ReactiveCommand<Unit, Unit> BSpaceCommand { get; }
        public ReactiveCommand<Operation, Unit> DoOperationCommand { get; }
        public MainWindowViewModel()
        {
            AddNumbCommand = ReactiveCommand.Create<int>(AddNumb);
            DoOperationCommand = ReactiveCommand.Create<Operation>(DoOperation);
            BSpaceCommand = ReactiveCommand.Create(BSpace);
           RxApp.DefaultExceptionHandler = Observer.Create<Exception>(
                ex => Console.Write("next"),
                ex => Console.Write("Unhandled rxui error"));
          
        }

        private void AddNumb(int value)
        {
           
            ShownValue = ShownValue * 10 + value;
        }

        public void C()
        {
            ShownValue = 0;
            _operation = Operation.Add;
            _firstValue = 0;
        }

        public void Exit()
        {
            Environment.Exit(0);
        }
        public void BSpace()
        {
            ShownValue = (int)ShownValue / 10;
        }
        private void DoOperation(Operation operation)
        {
            switch (_operation)
            {
                case Operation.Add:
                {
                    _firstValue += _secondValue;
                    ShownValue = 0;
                    break;
                }
                case Operation.Sub:
                {
                    _firstValue -= _secondValue;
                    ShownValue = 0;
                    break;
                }
                case Operation.Mul:
                {
                    _firstValue *= _secondValue;
                    ShownValue = 0;
                    break;
                }
                case Operation.Div:
                {
                    _firstValue /= _secondValue;
                    ShownValue = 0;
                    break;
                }
            }

            if (operation == Operation.Res)
            {
                ShownValue = _firstValue;
                _operation = Operation.Add;
                _firstValue = 0;
            }
            else
            {
                _operation = operation;
            }
        }
    }
}