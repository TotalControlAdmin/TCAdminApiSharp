using System.Collections.Generic;

namespace TCAdminApiSharp.Entities.Generic;

public class TcaXmlField : Dictionary<string, object>
{
    public TcaXmlField()
    {
    }

    public TcaXmlField(IDictionary<string, object> dictionary) : base(dictionary)
    {
    }
}