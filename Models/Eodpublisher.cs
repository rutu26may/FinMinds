using System;
using System.Collections.Generic;

namespace finminds.Models;

public partial class Eodpublisher
{
    public int Id { get; set; }

    public string SystemName { get; set; }

    public string PubStatus { get; set; }

    public DateTime? EodDate { get; set; }
}
