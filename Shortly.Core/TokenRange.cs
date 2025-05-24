namespace Shortly.Core;

public record TokenRange
{
    public long Start { get; }
    public long End { get; }
    
    public TokenRange(long start, long end)
    {
        Start = start;
        End = end;
    }
    
}