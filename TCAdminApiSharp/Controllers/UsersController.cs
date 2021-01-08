namespace TCAdminApiSharp.Controllers
{
    public class UsersController
    {
        private readonly TcaClient _tcaClient;

        public UsersController(TcaClient tcaClient)
        {
            _tcaClient = tcaClient;
        }
    }
}