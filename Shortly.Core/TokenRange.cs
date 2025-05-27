namespace Shortly.Core;

public record TokenRange
{
    public TokenRange(long start, long end)
    {
        if (end < start)
            throw new ArgumentException("End Must Be Greater Than Or Equal The Start");
        
        Start = start;
        End = end;
    }

    public long Start { get; }
    public long End { get; }
}