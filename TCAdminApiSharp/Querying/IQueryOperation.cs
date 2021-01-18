namespace TCAdminApiSharp.Querying
{
    public interface IQueryOperation
    {
        string JsonKey { get; set; }

        string GenerateQuery();
    }
}