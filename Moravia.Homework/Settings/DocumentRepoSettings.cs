﻿using Moravia.Homework.Settings.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moravia.Homework.Settings
{
    public class DocumentRepoSettings
  {
    public DocumentRepoType? RepoType { get; set; }
    public DocumentRepoMode Mode { get; set; }
    public string? Location { get; set; }
  }
}
