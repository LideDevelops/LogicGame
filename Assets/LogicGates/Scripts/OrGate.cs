using System;
using UniRx;

namespace CuriosOtter.LogicGame.Gates
{
    public class OrGate : IGate
    {
        IOState IGate.Output { get; }
        private ReplaySubject<IOState> OutputObservable { get; set; }

        IObservable<IOState> IGate.OutputObservable => this.OutputObservable;

        int IGate.NumberOfInputStates { get; }
        private IOState[] states;

        public OrGate()
        {
            states = new IOState[2];
            OutputObservable = new ReplaySubject<IOState>(1);
        }

        void IGate.SetInputStateObserverable(IObservable<IOState> state, int inputIndex)
        {
            if (inputIndex > 1)
            {
                throw new ArgumentException("The or gate has only two inputs");
            }

            state.Subscribe(x =>
            {
                states[inputIndex] = x;
                Handle();
            });
        }

        private void Handle()
        {
            IOState nextState = states[0] | states[1]; 
            OutputObservable.OnNext(nextState);
        }
    }
}