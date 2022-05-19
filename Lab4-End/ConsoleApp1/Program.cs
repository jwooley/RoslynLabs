// See https://aka.ms/new-console-template for more information
using ConsoleApp1;
using CsvSerializer;

Console.WriteLine(new Person { FirstName = "John", LastName = "Doe" }.ToCsv());