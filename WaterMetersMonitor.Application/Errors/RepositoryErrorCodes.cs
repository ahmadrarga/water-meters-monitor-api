using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterMetersMonitor.Application.Errors
{
    public enum RepositoryErrorCodes
    {
        EntityIsNull = 1,
        EntityNotFound = 2,
        ConstraintsNotMatched = 3
    }
}
