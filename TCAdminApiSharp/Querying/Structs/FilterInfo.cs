namespace TCAdminApiSharp.Querying.Structs;

public struct FilterInfo
{
    public string Column;

    public FilterInfo(string column)
    {
        Column = column;
    }
}