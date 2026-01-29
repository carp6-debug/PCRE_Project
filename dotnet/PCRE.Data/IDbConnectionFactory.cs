using System.Data;

namespace PCRE.Data;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}