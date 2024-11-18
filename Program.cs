using AoC_2023_03;
using System.Text;

EnginePartProcessor enginePartProcessor = new EnginePartProcessor();

char[,] processedInput = enginePartProcessor.ProcessInput("TestInput");
int rows = processedInput.GetLength(0);
int columns = processedInput.GetLength(1);

int finalAnswer = enginePartProcessor.CalculateEngineParts(processedInput, rows, columns);

Console.WriteLine($"Final Answer: {finalAnswer}");


