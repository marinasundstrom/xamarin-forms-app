using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace ShellApp.Services
{
    public class MessageBus : IMessageBus, IDisposable
    {
        Subject<object> subject = new Subject<object>();
        private static MessageBus instance;

        public IObservable<T> WhenPublished<T>() => subject.OfType<T>().AsObservable();

        public void Publish(object obj) => subject.OnNext(obj);

        public void Dispose()
        {
            subject.Dispose();
        }

        public static IMessageBus Instance => instance ??= new MessageBus();
    }
}

