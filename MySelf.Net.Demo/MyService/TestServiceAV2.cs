using MySelf.Net.Demo.MyInterface;
using System;

namespace MySelf.Net.Demo.MyService
{
    public class TestServiceAV2 : ITestServiceA
    {
        public TestServiceAV2()
        {
            Console.WriteLine($"{this.GetType().Name} V2被构造。。。");
        }

        public void Show()
        {
            Console.WriteLine($"{this.GetType().Name} A123456  V2");
        }
    }
}
