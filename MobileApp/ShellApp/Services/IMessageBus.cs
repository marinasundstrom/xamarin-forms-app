using System;

namespace ShellApp.Services
{
    public interface IMessageBus
    {
        void Publish(object obj);
        IObservable<T> WhenPublished<T>();
    }
}