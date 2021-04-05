using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Collections.ObjectModel;

namespace CuriosOtter.LogicGame.Gates
{
    public interface IGate
    {
        IOState Output { get; }
        ReadOnlyDictionary<string,IObservable<IOState>> OutputObservable { get; }
        IEnumerable<string> OutputNames { get; }
        IEnumerable<string> InputNames { get; }
        Sprite GateImage { get; set; }
        void SetInputStateObserverable(IObservable<IOState> state, string name);
    }
}
