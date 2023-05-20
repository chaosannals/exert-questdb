using CsDemo;
using QuestDB;

Console.WriteLine("Start");


await LineTcp.OneLine();
await LineTcp.MultiLine();

//await LineTcp.Auth();

await LineTcp.FixedIO();

Console.WriteLine("End");
