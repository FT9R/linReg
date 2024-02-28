using System.Globalization;

var streamReader = StreamReader.Null;
var samplesPath = Path.Combine(Environment.CurrentDirectory, "samples.csv");
var consoleKey = ConsoleKey.None;

/* App description */
Console.WriteLine("This is a console application designed to find the best-fit linear equation 'yi = axi + b'\n" +
    $"for a samples dataset(xi,yi) provided by the user in: \n\"{samplesPath}\" file.");
Console.WriteLine("Where 'xi' and 'yi' are independent and dependent values from the dataset, respectively.\n" +
    "'a' is the slope of the line and 'b' is the y-intercept.");
Console.WriteLine("The sample file should be located in the same directory as this application and contain \n" +
    "data separated by a dot between the integer and fractional parts and a comma between \nthe values as follows:");
Console.WriteLine("- - -\nx1,y1\nx2,y2\n. . .\nxn,yn\n- - -\n");

while (true)
{
    List<double> independentValues = [];
    List<double> dependentValues = [];

    try
    {
        streamReader = new StreamReader(samplesPath);
    }
    catch (Exception e)
    {
        ExceptionHandler(e);

        continue;
    }
    GetValuesFromSamplesSet(independentValues, dependentValues);
    streamReader.Close();

    try
    {
        var (slope, intercept) = Statistics.LeastSquares.GetSlopeAndIntercept([.. independentValues], [.. dependentValues]);
        Console.WriteLine("Samples processed:\t{0}", independentValues.Count);
        Console.WriteLine("Line slope:\t\t{0:N8}", slope);
        Console.WriteLine("y-intercept:\t\t{0:N8}", intercept);
        Console.WriteLine("R-Squared:\t\t{0:N8}", Statistics.LeastSquares.GetCoefficientOfDetermination([.. independentValues], [.. dependentValues], slope, intercept));
    }
    catch (Exception e)
    {
        ExceptionHandler(e);

        continue;
    }

    Console.WriteLine("Press Enter to continue or Esc to exit\n");
    do
        consoleKey = Console.ReadKey(true).Key;
    while ((consoleKey != ConsoleKey.Enter) && (consoleKey != ConsoleKey.Escape));
    if (consoleKey == ConsoleKey.Escape)
        Environment.Exit(0);
}

bool GetValuesFromSamplesSet(List<double> independentValues, List<double> dependentValues)
{
    while (true)
    {
        var sampleLine = streamReader.ReadLine();
        if (sampleLine is null)
            return true;

        var sampleSeparated = sampleLine.Split(',', 3);

        if (sampleSeparated.Length != 2)
        {
            Console.WriteLine("Warning: invalid input data format");

            return false;
        }

        if (!double.TryParse(sampleSeparated[0], CultureInfo.InvariantCulture, out var independent))
        {
            Console.WriteLine("Warning: the independent value is not a number in the {0}th sample", independentValues.Count + 1);

            return false;
        }
        if (!double.TryParse(sampleSeparated[1], CultureInfo.InvariantCulture, out var dependent))
        {
            Console.WriteLine("Warning: the dependent value is not a number in the {0}th sample", dependentValues.Count + 1);

            return false;
        }
        independentValues.Add(independent);
        dependentValues.Add(dependent);
    }
}

void ExceptionHandler(Exception e)
{
    Console.WriteLine("An exception has occurred:\n" + e.StackTrace);
    Console.WriteLine($"> {e.Message}");
    Console.WriteLine("Press Enter to continue or Esc to exit\n");
    do
        consoleKey = Console.ReadKey(true).Key;
    while ((consoleKey != ConsoleKey.Enter) && (consoleKey != ConsoleKey.Escape));
    if (consoleKey == ConsoleKey.Escape)
        Environment.Exit(0);
}