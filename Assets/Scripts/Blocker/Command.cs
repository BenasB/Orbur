using System;

namespace Blocker
{
    [AttributeUsage(AttributeTargets.Method)]
    sealed class Command : Attribute
    {
    }
}