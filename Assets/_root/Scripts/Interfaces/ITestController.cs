namespace TestSpace
{
    internal interface ITestController : IController
    {
        void Init();
        void SetTestLength(int testLength);
    }
}
