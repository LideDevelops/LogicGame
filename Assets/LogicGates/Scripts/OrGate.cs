using System;
using UniRx;

namespace CuriosOtter.LogicGame.Gates
{
    public class OrGate : IGate
    {
        public IOState Output { get; private set; }
        private ReplaySubject<IOState> OutputObservable { get; set; }

        IObservable<IOState> IGate.OutputObservable => this.OutputObservable;

        int IGate.NumberOfInputStates { get; }
        private IOState[] states;

        public OrGate()
        {
            states = new IOState[2];
            OutputObservable = new ReplaySubject<IOState>(1);
            OutputObservable.Subscribe(x => Output = x);
            Output = IOState.Off;
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