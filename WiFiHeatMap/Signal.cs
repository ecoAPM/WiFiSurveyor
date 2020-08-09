namespace WiFiHeatMap
{
    public struct Signal
    {
        public string MAC { get; set; }
        public string SSID { get; set; }
        public Frequency Frequency { get; set; }
        public decimal Strength { get; set; }
    }
}