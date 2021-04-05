using System;
using System.Collections;
using System.Collections.Generic;
using CuriosOtter.LogicGame.Gates;
using UnityEngine;

namespace CuriosOtter.LogicGame.SignalGenerator
{
    public interface ISignalGenerator
    {
        IObservable<IOState> Output { get; }
        float Interval { get; set; }
        void Begin();
        void Stop();
        
    }
}
