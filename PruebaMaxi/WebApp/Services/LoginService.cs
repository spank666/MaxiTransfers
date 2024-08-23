using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApp.BDD;
using WebApp.Dtos;

namespace WebApp.Services
{
    public class LoginService : ILoginService
    {
        private readonly SqlServerDbContext _sqlServer;
        public LoginService(SqlServerDbContext sqlServer)
        {
            _sqlServer = sqlServer;
        }
        public async Task<bool> ValidateUser(CredentialDto CredentialDto)
        {
            bool response = false;
            try
            {
                var parameters = new SqlParameter[] {
                    new SqlParameter() {
                        ParameterName = "@UserName",
                        SqlDbType =  System.Data.SqlDbType.VarChar,
                        Size = 50,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = CredentialDto.User
                    },
                    new SqlParameter() {
                        ParameterName = "@Pass",
                        SqlDbType =  System.Data.SqlDbType.VarChar,
                        Size = 50,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = CredentialDto.Password
                    },
                    new SqlParameter() {
                        ParameterName = "@UserExist",
                        SqlDbType =  System.Data.SqlDbType.Bit,
                        Direction = System.Data.ParameterDirection.Output
                    }
                };

                using (var command = _sqlServer.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[dbo].[SPR_VALIDATE_USER]";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddRange(parameters);

                    await _sqlServer.Database.OpenConnectionAsync();

                    using (var result = await command.ExecuteReaderAsync())
                    {

                        response = Convert.ToBoolean(parameters[2].Value);
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                return response;
            }
        }
    }
}
