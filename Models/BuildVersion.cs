﻿using System;
using System.Collections.Generic;

namespace finminds.Models;

public partial class BuildVersion
{
    public byte SystemInformationId { get; set; }

    public string DatabaseVersion { get; set; }

    public DateTime VersionDate { get; set; }

    public DateTime ModifiedDate { get; set; }
}
