using MySelf.Net.Demo.MyInterface;
using System;

namespace MySelf.Net.Demo.MyService
{
    public class TestServiceB : ITestServiceB
    {
        private ITestServiceA _ITestServiceA = null;
        public TestServiceB(ITestServiceA iTestServiceA)
        {
            Console.WriteLine($"{this.GetType().Name}被构造。。。");
            this._ITestServiceA = iTestServiceA;
        }


        public void Show()
        {
            Console.WriteLine($"{this.GetType().Name}  B123456");
            //不要去直接new什么，也应该IOC
        }
    }
}
