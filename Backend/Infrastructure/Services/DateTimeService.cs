using System;
using ShellApp.Application.Common.Interfaces;

namespace ShellApp.Infrastructure.Services
{
    class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
