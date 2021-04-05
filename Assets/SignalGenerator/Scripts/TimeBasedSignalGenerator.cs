using System;
using CuriosOtter.LogicGame.Gates;
using UniRx;
using UnityEngine;
using Utility;

namespace CuriosOtter.LogicGame.SignalGenerator
{
    
    [RequireComponent(typeof(SpriteRenderer))]
    public class TimeBasedSignalGenerator : MonoBehaviour, ISignalGenerator
    {
        private Subject<IOState> _output;
        private float lastChange;
        private IOState currentState;
        [SerializeField]
        private float _interval;
        public IObservable<IOState> Output => _output;
        private IDisposable updateSubscribor;
        private SpriteRenderer renderer;

        public float Interval
        {
            get => _interval;
            set => _interval = value;
        }

        public void Start()
        {
            renderer = GetComponent<SpriteRenderer>();
            _output = new Subject<IOState>();
            _output.OnNext(IOState.Off);
            renderer.sprite = Resources.Load<Sprite>("SignalGeneratorGraphics/SignalGenerator");
            Begin();
            ConnectionPointSpawner.SpawnConnectionPoints(transform, GetComponent<SpriteRenderer>(), new []{"Q"}, false);
        }
        
        private void SubscribeToUpdates()
        {
            updateSubscribor = Observable.EveryUpdate().Subscribe(x =>
            {
                if (lastChange + _interval < Time.time)
                {
                    lastChange = Time.time;
                    switch (currentState)
                    {
                        case IOState.Off:
                            currentState = IOState.On;
                            _output.OnNext(IOState.On);
                            break;
                        case IOState.On:
                            currentState = IOState.Off;
                            _output.OnNext(IOState.Off);
                            break;
                        default:
                            currentState = IOState.Off;
                            _output.OnNext(IOState.Off);
                            break;
                    }
                }
            });
        }

        public void Begin()
        {
            SubscribeToUpdates();
        }

        public void Stop()
        {
            updateSubscribor.Dispose();
        }
    }
}