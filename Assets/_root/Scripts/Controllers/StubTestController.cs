using System;

namespace TestSpace
{
    internal class StubTestController : ITestController
    {
        public event Action<TestObjectEnum, string, bool> TestObjectRepeat;

        public void Destroy() { }

        public void Init() { }

        public void SetTestLength(int testLength) { }
    }
}
