﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace INTEX2.Models;

public partial class Color
{
    public long Id { get; set; }

    public string? Value { get; set; }

    public int? Colorid { get; set; }
}
