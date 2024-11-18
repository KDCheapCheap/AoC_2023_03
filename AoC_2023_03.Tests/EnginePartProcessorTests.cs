namespace AoC_2023_03.Tests
{
    public class EnginePartProcessorTests
    {
        private readonly EnginePartProcessor _processor;

        private char[,] testProcessedInput = new char[,]
        {
            { '4', '6', '7', '.', '.', '1', '1', '4', '.', '.' },
            { '.', '.', '.', '*', '.', '.', '.', '.', '.', '.' },
            { '.', '.', '3', '5', '.', '.', '6', '3', '3', '.' },
            { '.', '.', '.', '.', '.', '.', '#', '.', '.', '.' },
            { '6', '1', '7', '*', '.', '.', '.', '.', '.', '.' },
            { '.', '.', '.', '.', '.', '+', '.', '5', '8', '.' },
            { '.', '.', '5', '9', '2', '.', '.', '.', '.', '.' },
            { '.', '.', '.', '.', '.', '.', '7', '5', '5', '.' },
            { '.', '.', '.', '$', '.', '*', '.', '.', '.', '.' },
            { '.', '6', '6', '4', '.', '5', '9', '8', '.', '.' }
        };

        public EnginePartProcessorTests()
        {
            _processor = new EnginePartProcessor();
        }

        [Fact]
        public void CalculateEngineParts_ShouldCalculateCorrectAnswer()
        {
            //Arrange
            int rows = testProcessedInput.GetLength(0);
            int columns = testProcessedInput.GetLength(1);

            //Act
            var result = _processor.CalculateEngineParts(testProcessedInput, rows, columns);

            //Assert
            Assert.Equal(4361, result); // Replace 21 with your expected sum
        }

        [Fact]
        public void ProcessNumberInGrid_ShouldGetFullNumber()
        {
            //Arrange
            int rows = testProcessedInput.GetLength(0);
            int columns = testProcessedInput.GetLength(1);

            //Act
            bool result = _processor.ProcessNumberInGrid([0, 0], rows, columns, testProcessedInput, out int partNumber, out int loopPos);

            //Assert
            Assert.Equal(467, partNumber);
        }
        
        [Fact]
        public void ProcessNumberInGrid_ShouldDeterminePart()
        {
            //Arrange
            int rows = testProcessedInput.GetLength(0);
            int columns = testProcessedInput.GetLength(1);

            //Act
            bool result = _processor.ProcessNumberInGrid([0, 0], rows, columns, testProcessedInput, out int partNumber, out int loopPos);

            //Assert
            Assert.True(result);
        }
        
        [Fact]
        public void ProcessNumberInGrid_ShouldDetermineNotPart()
        {
            //Arrange
            int rows = testProcessedInput.GetLength(0);
            int columns = testProcessedInput.GetLength(1);

            //Act
            bool result = _processor.ProcessNumberInGrid([0, 5], rows, columns, testProcessedInput, out int partNumber, out int loopPos);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void CheckAdjacentCharForSymbol_ShouldFindSymbol()
        {
            //Arrange
            int rows = testProcessedInput.GetLength(0);
            int columns = testProcessedInput.GetLength(1);

            //Act
            bool result = _processor.CheckAdjacentCharForSymbol([5, 2], rows, columns, testProcessedInput);

            //Assert
            Assert.True(result);
        }
        
        [Fact]
        public void CheckAdjacentCharForSymbol_ShouldFindNoSymbols()
        {
            //Arrange
            int rows = testProcessedInput.GetLength(0);
            int columns = testProcessedInput.GetLength(1);

            //Act
            bool result = _processor.CheckAdjacentCharForSymbol([5, 7], rows, columns, testProcessedInput);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void ProcessInput_ShouldReturnCorrectArray()
        {
            //Act
            char[,] result = _processor.ProcessInput("TestInput");

            //Assert
            Assert.Equal(testProcessedInput, result);
        }

        [Fact]
        public void ValidateInput_ShouldThrowExceptionForInvalidInput()
        {
            // Arrange
            var invalidInput = new[] { "123", "12", "12345" };

            // Act & Assert
            var ex = Assert.Throws<InvalidDataException>(() => _processor.ValidateInput(invalidInput));
            Assert.Equal("Input file does not have equal line length.", ex.Message);
        }
    }
}