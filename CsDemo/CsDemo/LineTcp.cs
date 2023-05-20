using QuestDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsDemo;

public static class LineTcp
{
    /// <summary>
    /// 单行插入
    /// </summary>
    /// <returns></returns>
    public static async Task OneLine()
    {
        using var ls = await LineTcpSender.ConnectAsync("localhost", 9009, tlsMode: TlsMode.Disable);

        // 
        ls.Table("metric_name")
            .Symbol("Symbol", "value")
            .Column("number", 10)
            .Column("double", 12.23)
            .Column("string", "born to shine")
            .At(new DateTime(2021, 11, 25, 0, 46, 26));
        await ls.SendAsync();
    }

    /// <summary>
    /// 多行插入
    /// </summary>
    /// <returns></returns>
    public static async Task MultiLine()
    {
        using var ls = await LineTcpSender.ConnectAsync("localhost", 9009, tlsMode: TlsMode.Disable);
        //
        for (int i = 0; i < 1E6; i++)
        {
            ls.Table("metric_name")
                .Column("counter", i)
                .AtNow();
        }
        ls.Send();
    }

    /// <summary>
    /// TLS 授权
    /// </summary>
    /// <returns></returns>

    public static async Task Auth()
    {
        using var ls = await LineTcpSender.ConnectAsync("localhost", 9009, tlsMode: TlsMode.Enable);
        await ls.AuthenticateAsync("admin", "NgdiOWDoQNUP18WOnb1xkkEG5TzPYMda5SiUOvT1K0U=");
        for (int i = 0; i < 1E6; i++)
        {
            ls.Table("metric_name")
           .Column("counter", i)
           .AtNow();
        }
        await ls.SendAsync();
    }

    public static async Task FixedIO()
    {
        using var ls = await LineTcpSender.ConnectAsync("localhost", 9009, bufferOverflowHandling: BufferOverflowHandling.SendImmediately, tlsMode: TlsMode.Disable);
        //await ls.AuthenticateAsync("admin", "NgdiOWDoQNUP18WOnb1xkkEG5TzPYMda5SiUOvT1K0U=");
        for (int i = 0; i < 1E6; i++)
        {
            ls.Table("metric_name")
            .Column("counter", i)
            .AtNow();
        }
        await ls.SendAsync();
    }
}
