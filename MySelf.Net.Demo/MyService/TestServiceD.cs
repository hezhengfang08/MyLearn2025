using MySelf.Net.Demo.MyInterface;
using System;

namespace MySelf.Net.Demo.MyService
{
    public class TestServiceD : ITestServiceD
    {
        public TestServiceD()
        {
            Console.WriteLine($"{this.GetType().Name}被构造。。。");
        }
        public void Show()
        {
            Console.WriteLine($"{this.GetType().Name} D123456");
        }
    }
}
