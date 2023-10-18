using Moravia.Homework.Attributes;
using Moravia.Homework.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moravia.Homework.Settings.Enum
{
    public enum DocumentRepoType
    {
        [TypeToCreate(typeof(FileSystemDocumentRepo))]
        FileSystem
    }
}
