namespace Statistics;
public static class LeastSquares
{
    private static (double value, int occurrences) DetectMultipleOccurrences(double[] values)
    {
        var set = new HashSet<double>();
        foreach (var item in values)
        {
            if (!set.Add(item))
                return (item, values.Count(x => x == item));
        }

        return (0, 0);
    }
    public static (double slope, double intercept) GetSlopeAndIntercept(double[] independent, double[] dependent)
    {
        ArgumentNullException.ThrowIfNull(independent);
        ArgumentNullException.ThrowIfNull(dependent);
        if (independent.Length != dependent.Length)
            throw new ArgumentException("Arrays of independent and dependent values must have the same length " +
                "corresponding to the number of samples");
        if (independent.Length < 2)
            throw new ArgumentException("At least 2 samples are required for further calculation");

        /* Check for multiple occurrences in the input arrays */
        double value, occurrences;
        (value, occurrences) = DetectMultipleOccurrences(independent);
        if (occurrences > 1)
            throw new ArgumentException(string.Format("A number \"{0}\" appears {1} times within the independent values array", value, occurrences));
        (value, occurrences) = DetectMultipleOccurrences(dependent);
        if (occurrences > 1)
            throw new ArgumentException(string.Format("A number \"{0}\" appears {1} times within the dependent values array", value, occurrences));

        double slope;
        double intercept;
        double slopeNumerator = 0;
        double slopeDenumerator = 0;
        var independentAvg = independent.Average();
        var dependentAvg = dependent.Average();

        /* Calculate the slope of the regression line */
        for (var i = 0; i < independent.Length; i++)
        {
            slopeNumerator += (independent[i] - independentAvg) * (dependent[i] - dependentAvg);
            slopeDenumerator += Math.Pow(independent[i] - independentAvg, 2);
        }
        slope = slopeNumerator / slopeDenumerator;

        /* Calculate the y-intercept */
        intercept = dependentAvg - slope * independentAvg;

        return (slope, intercept);
    }

    public static double GetCoefficientOfDetermination(double[] independent, double[] dependent, double slope, double intercept)
    {
        ArgumentNullException.ThrowIfNull(independent);
        ArgumentNullException.ThrowIfNull(dependent);
        if (independent.Length != dependent.Length)
            throw new ArgumentException("Arrays of independent and dependent values must have the same length " +
                "corresponding to the number of samples");
        if (independent.Length < 2)
            throw new ArgumentException("At least 2 samples are required for further calculation"); ;

        double residualSumOfSquares = 0;
        double totalSumOfSquares = 0;
        var dependentAvg = dependent.Average();

        for (var i = 0; i < dependent.Length; i++)
        {
            residualSumOfSquares += Math.Pow(dependent[i] - (independent[i] * slope + intercept), 2);
            totalSumOfSquares += Math.Pow(dependent[i] - dependentAvg, 2);
        }

        return 1 - residualSumOfSquares / totalSumOfSquares;
    }
}