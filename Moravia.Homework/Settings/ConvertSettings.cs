﻿using Moravia.Homework.Settings.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moravia.Homework.Settings
{
  public class ConvertSettings
  {
    public DocumentRepoSettings RepoSettings { get; set; }
    public Type SerializerType { get; set; }
    public Type DocumentType { get; set; }
  }
}
