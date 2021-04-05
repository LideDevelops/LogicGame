using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace CuriosOtter.LogicGame.Gates
{
    public interface IGate
    {
        IOState Output { get; }
        IObservable<IOState> OutputObservable { get; }
        int NumberOfInputStates { get; }
        void SetInputStateObserverable(IObservable<IOState> state, int inputIndex);
    }
}
