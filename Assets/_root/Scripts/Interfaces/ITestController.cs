using System;

namespace TestSpace
{
    internal interface ITestController : IController
    {
        event Action<TestObjectEnum, string, bool> TestObjectRepeat;

        void Init();
        void SetTestLength(int testLength);
    }
}
