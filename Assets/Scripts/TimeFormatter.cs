public class TimeFormatter : ITimeFormatter
{
    public string Format(double timeLeftInSeconds)
    {
        double minutes = timeLeftInSeconds / 60;
        double seconds = timeLeftInSeconds % 60;
        double milliseconds = timeLeftInSeconds * 1000 % 1000;
            
        return $"{minutes:F0}:{seconds:00}:{milliseconds:000}";
    }
}