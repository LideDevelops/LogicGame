using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UniRx;
using UnityEngine;

namespace CuriosOtter.LogicGame.Gates
{
    public class OrGate : IGate
    {
        public IOState Output { get; private set; }

        ReadOnlyDictionary<string, IObservable<IOState>> IGate.OutputObservable => new ReadOnlyDictionary<string, IObservable<IOState>>(_outputObservable);

        public IEnumerable<string> OutputNames { get; }
        public IEnumerable<string> InputNames { get; }
        private ReplaySubject<IOState> OutputObservable { get; set; }


        public Sprite GateImage { get; set; }


        private Dictionary<string, IOState> states;
        private Dictionary<string, IObservable<IOState>> _outputObservable;

        public OrGate()
        {
            OutputObservable = new ReplaySubject<IOState>(1);
            OutputObservable.Subscribe(x => Output = x);
            Output = IOState.Off;
            GateImage = Resources.Load<Sprite>("OrGateGraphics/Or Gate");
            OutputNames = new[] {"Q"};
            InputNames = new[] {"A", "B"};
            states = new Dictionary<string, IOState>();
            foreach (var name in InputNames)
            {
                states.Add(name, IOState.Off);
            }

            _outputObservable = new Dictionary<string, IObservable<IOState>>();
            foreach (var name in OutputNames)
            {
                _outputObservable.Add(name, OutputObservable);
            }
        }

        void IGate.SetInputStateObserverable(IObservable<IOState> state, string inputName)
        {
            if (!InputNames.Contains(inputName))
            {
                throw new ArgumentException($"The or gate has only two inputs with name {string.Join(", ", inputName)}");
            }

            state.Subscribe(x =>
            {
                states[inputName] = x;
                Handle();
            });
        }

        private void Handle()
        {
            IOState nextState = IOState.Off;
            foreach (var state in states.Select(x => x.Value))
            {
                nextState = state | nextState;
            }
            OutputObservable.OnNext(nextState);
        }
    }
}