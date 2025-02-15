using MySelf.Net.Demo.MyInterface;
using System;

namespace MySelf.Net.Demo.MyService
{
    public class TestServiceEV2 : ITestServiceE
    {
        public TestServiceEV2()
        {
            Console.WriteLine($"{this.GetType().Name} --V2被构造。。。");
        }

        public void Show()
        {
            Console.WriteLine($"{this.GetType().Name} E123456  V3");
        }
    }
}
