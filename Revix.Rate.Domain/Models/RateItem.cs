namespace Revix.Rate.Domain.Models;

public class RateItem {
    public int? id { get; set; }
    public string name { get; set; }
    public string symbol { get; set; }
    public string slug { get; set; }
    public int? cmc_rank { get; set; }
    public int? num_market_pairs { get; set; }
    public int? circulating_supply { get; set; }
    public int? total_supply { get; set; }
    public int? max_supply { get; set; }
    public DateTime? last_updated { get; set; }
    public DateTime? date_added { get; set; }
    public List<string> tags { get; set; }
    public string platform { get; set; }
    public string self_reported_circulating_supply { get; set; }
    public string self_reported_market_cap { get; set; }
    public Quote quote { get; set; }
}