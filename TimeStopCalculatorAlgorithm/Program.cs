// See https://aka.ms/new-console-template for more information

namespace LineStoppageCalculator;

// Simple interval struct
public struct Interval
{
    public DateTime Start;
    public DateTime End;

    public Interval(DateTime s, DateTime e)
    {
        Start = s;
        End = e;
    }

    // Returns true if this interval contains t (inclusive start, exclusive end)
    public bool Contains(DateTime t)
    {
        return t >= Start && t < End;
    }

    // Intersect this interval with another; returns null if they don't overlap
    public Interval? Intersect(Interval other)
    {
        var s = Start > other.Start ? Start : other.Start;
        var e = End < other.End ? End : other.End;
        if (s < e) return new Interval(s, e);
        return null;
    }
}

internal class Program
{
    private static void Main(string[] args)
    {
        // 1) define global window: 8:00–20:00 on some day
        var day = DateTime.Today.Date;
        var globalWindow = new Interval(day.AddHours(8), day.AddHours(23));

        // 2) sample stop data per asset
        var stops = new Dictionary<int, List<Interval>>
        {
            [1] =
            [
                new Interval(day.AddHours(10), day.AddHours(14)),
                new Interval(day.AddHours(15), day.AddHours(17)),
                new Interval(day.AddHours(20), day.AddHours(23))
            ],
            [2] =
            [
                new Interval(day.AddHours(11), day.AddHours(13)),
                new Interval(day.AddHours(14), day.AddHours(15)),
                new Interval(day.AddHours(16), day.AddHours(19)),
                new Interval(day.AddHours(22), day.AddHours(23))
            ],
            [3] =
            [
                new Interval(day.AddHours(11), day.AddHours(15)),
                new Interval(day.AddHours(17), day.AddHours(21))
            ],
            [4] =
            [
                new Interval(day.AddHours(10), day.AddHours(12)),
                new Interval(day.AddHours(18), day.AddHours(21))
            ]
        };

        // 3) sample placement data per asset
        var placements = new Dictionary<int, List<Interval>>
        {
            [1] = new() { new Interval(day.AddHours(8), DateTime.MaxValue) },
            [2] = new()
            {
                new Interval(day.AddHours(10), day.AddHours(20)),
                new Interval(day.AddHours(22), DateTime.MaxValue)
            },
            [3] = new()
            {
                new Interval(day.AddHours(11), day.AddHours(15)),
                new Interval(day.AddHours(17), DateTime.MaxValue)
            },
            [4] = new()
            {
                new Interval(day.AddHours(10), day.AddHours(12)),
                new Interval(day.AddHours(17), day.AddHours(21))
            }
        };

        // 4) compute “effective stops” = stops ∩ placement ∩ globalWindow
        var effectiveStops = new Dictionary<int, List<Interval>>();
        foreach (var assetId in stops.Keys)
        {
            var list = new List<Interval>();
            foreach (var stop in stops[assetId])
            foreach (var place in placements[assetId])
            {
                // stop ∩ place
                var sp = stop.Intersect(place);
                if (sp == null) continue;
                // then ∩ globalWindow
                var sg = sp.Value.Intersect(globalWindow);
                if (sg != null) list.Add(sg.Value);
            }

            effectiveStops[assetId] = list;
        }

        // 5) build a sorted list of all breakpoints
        var times = new SortedSet<DateTime>();
        times.Add(globalWindow.Start);
        times.Add(globalWindow.End);
        foreach (var kv in effectiveStops)
        foreach (var iv in kv.Value)
        {
            times.Add(iv.Start);
            times.Add(iv.End);
        }

        foreach (var kv in placements)
        foreach (var iv in kv.Value)
        {
            // we only care about when placement enters/exits globalWindow
            var ip = iv.Intersect(globalWindow);
            if (ip != null)
            {
                times.Add(ip.Value.Start);
                times.Add(ip.Value.End);
            }
        }

        // 6) sweep through each consecutive pair, check if all placed assets are also stopped
        double totalMinutes = 0;
        var timeList = times.ToList();
        var assetIds = placements.Keys;

        for (var i = 0; i < timeList.Count - 1; i++)
        {
            var segStart = timeList[i];
            var segEnd = timeList[i + 1];
            var midPoint = segStart + TimeSpan.FromTicks((segEnd - segStart).Ticks / 2);

            // find which assets are “on the line” at this midpoint
            var onLine = assetIds
                .Where(id => placements[id]
                    .Any(iv => iv.Contains(midPoint)))
                .ToList();

            if (onLine.Count == 0)
                continue; // no assets, skip

            // check that *every* one of those assets is also stopped
            var allStopped = onLine.All(id =>
                effectiveStops[id].Any(iv => iv.Contains(midPoint))
            );

            if (allStopped)
                totalMinutes += (segEnd - segStart).TotalMinutes;
        }

        Console.WriteLine($"Total full‐line stoppage: {totalMinutes} minutes");
    }
}