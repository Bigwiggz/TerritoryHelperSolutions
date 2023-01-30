using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerritoryHelperSolutionsWinForm.Services;

public class ProgressBarService
{
    private int _counter;
    private readonly int _maxValue;
    public ProgressBarService(int maxValue)
    {
        _maxValue = maxValue;
    }

    public async Task Run()
    {
        // Simulating some work
        for (int i = 0; i < _maxValue; i++)
        {
            Interlocked.Increment(ref _counter);
            Thread.Sleep(50);
        }
    }

    public double PercentComplete
    {
        get { return (double)_counter / _maxValue; }
    }
}
