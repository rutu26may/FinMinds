using System;
using System.Collections.Generic;

namespace finminds.Models;

public partial class TradeFlow
{
    public int Id { get; set; }

    public string TradeId { get; set; }

    public string LoadStatus { get; set; }

    public DateTime? LoadDate { get; set; }
}
