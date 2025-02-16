using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace MySelf.Net.Demo.NetLearnDemo.Utility
{
    public class ChangeTokenTest
    {

        public static void Show()
        {
            ////生成一次
            //Task.Run(() =>
            //{
            //    string path = @"E:\Works\learning\MyLearn2025\MySelf.Net.Demo\NetLearnDemo\bin\Debug";
            //    PhysicalFileProvider physicalFileProvider = new PhysicalFileProvider(path);
            //    var changeToken = physicalFileProvider.Watch("*.*");
            //    changeToken.RegisterChangeCallback(o => Console.WriteLine($"&&&&&&&&&&&&&&{path}文件有更新&&&&&&&&&&&&&&"),
            //      new object());
            //    Console.Read();
            //});
            //持续更新
            Task.Run(() =>
            {
                string path = @"E:\Works\learning\MyLearn2025\MySelf.Net.Demo\NetLearnDemo\bin\Debug";
                PhysicalFileProvider physicalFileProvider = new PhysicalFileProvider(path);
                ChangeToken.OnChange(
                    () => physicalFileProvider.Watch("*.*"),
                    () => Console.WriteLine($"&&&&&&&&&&&&&&{path}文件有更新&&&&&&&&&&&&&&"));

                Console.Read();

            });

        }
    }
}
