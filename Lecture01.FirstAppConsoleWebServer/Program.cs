using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var ip = IPAddress.Parse("127.0.0.1");
var port = 8080;

var listener = new TcpListener(ip, port);

listener.Start();
Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine($"Слушаем на {ip}:{port}");

while (true)
{
	var client = await listener.AcceptTcpClientAsync();

	Console.ForegroundColor = ConsoleColor.Cyan;
	Console.WriteLine($"Подключился клиент {client.Client.RemoteEndPoint}");

	using var networkStream = client.GetStream();
	using var stramReader = new StreamReader(networkStream);

	Console.ForegroundColor = ConsoleColor.Cyan;
	Console.WriteLine($"Данные запроса:");
	Console.ForegroundColor = ConsoleColor.White;

	var requestHeaders = string.Empty;
	while (stramReader.ReadLine() is string headersLine && !string.IsNullOrWhiteSpace(headersLine))
		requestHeaders += headersLine + '\n';

	Console.WriteLine(requestHeaders);

	var match = Regex.Match(requestHeaders.ToString(), @"(?im)^\s*Content-Length\s*:\s*(\d+)\s*$");
	if (match.Success)
	{
		char[] b = new char[int.Parse(match.Groups[1].Value)];
		stramReader.ReadBlock(b);
		Console.WriteLine(new string(b));
	}

	Console.WriteLine();

	var respone = $@"
			HTTP/1.1 200 OK
			Date: {DateTime.UtcNow:R}
			Server: Super Server 3000
			Content-Type: text/html; charset=utf-8
			Connection: close

			Hello from our server <br> <img src='https://media.istockphoto.com/id/1443562748/ru/%D1%84%D0%BE%D1%82%D0%BE/%D0%BC%D0%B8%D0%BB%D0%B0%D1%8F-%D1%80%D1%8B%D0%B6%D0%B0%D1%8F-%D0%BA%D0%BE%D1%88%D0%BA%D0%B0.jpg?s=612x612&w=0&k=20&c=k8RwP4usK_LCpQ1bPn3fNDLk3vtfptH7CEcEMZw_K1A='>
	".Replace("\t", "").Remove(0, 2);

	Console.ForegroundColor = ConsoleColor.Cyan;
	Console.WriteLine($"Данные ответа:");
	Console.ForegroundColor = ConsoleColor.White;
	Console.WriteLine(respone);

	var bytes = Encoding.UTF8.GetBytes(respone);

	networkStream.Write(bytes, 0, bytes.Length);
	networkStream.Flush();

	Console.WriteLine(new string('=', Console.WindowWidth));
	Console.WriteLine();
	Console.WriteLine();
}