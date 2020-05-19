using System;

namespace Pagene.Converter.Entry
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            await new Converter().Convert();
        }
    }
}
