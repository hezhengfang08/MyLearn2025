using MySelf.Net.Demo.MyInterface;
using System;

namespace MySelf.Net.Demo.MyService
{
    public class TestServiceE : ITestServiceE
    {
        public TestServiceE(ITestServiceC serviceC)
        {
            Console.WriteLine($"{this.GetType().Name}被构造。。。");
        }

        public void Show()
        {
            Console.WriteLine($"{this.GetType().Name} E123456  V2");
        }
    }
}
