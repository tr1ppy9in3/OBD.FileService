﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBD.FileService.Files.UseCases.Tags;

public class TagModel
{
    public required string Name { get; set; }

    public string? Description { get; set; } = default;
}
