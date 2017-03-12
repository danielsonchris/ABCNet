using System.Collections.Generic;

public abstract class IFoodSource
{
    public List<object> Centroids { get; set; }

    public int TrialsCount { get; set; }

    public double FitnessValue { get; set; }
}