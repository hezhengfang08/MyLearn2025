﻿using MySelf.Net.Demo.MyInterface;
using System;

namespace MySelf.Net.Demo.MyService
{
    public class TestServiceC : ITestServiceC
    {
        public TestServiceC(ITestServiceB iTestServiceB)
        {
            Console.WriteLine($"{this.GetType().Name}被构造。。。");
        }

        public TestServiceC(ITestServiceA testServiceA, ITestServiceB iTestServiceB)
        {
            Console.WriteLine($"{this.GetType().Name}被构造。。。");
        }


        public void Show()
        {
            Console.WriteLine($"{this.GetType().Name} C123456");
        }
    }
}
