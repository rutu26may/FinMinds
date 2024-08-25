using System;
using System.Collections.Generic;

namespace finminds.Models;

public partial class TradingBook
{
    public int Id { get; set; }

    public string BookName { get; set; }

    public string BookStatus { get; set; }

    public int? BookLivePosition { get; set; }
}
