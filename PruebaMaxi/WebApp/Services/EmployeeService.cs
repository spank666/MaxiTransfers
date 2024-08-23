using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApp.BDD;
using WebApp.Dtos;

namespace WebApp.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly SqlServerDbContext _sqlServer;
        public EmployeeService(SqlServerDbContext sqlServer)
        {
            _sqlServer = sqlServer;
        }
        public async Task<bool> Add(EmployeeDto employee)
        {
            bool response = false;
            try
            {
                var parameters = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@FirstName",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 100,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = employee.Nombres
                        },
                        new SqlParameter() {
                            ParameterName = "@LastName",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 100,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = employee.Apellidos
                        },
                        new SqlParameter() {
                            ParameterName = "@DateOfBirth",
                            SqlDbType =  System.Data.SqlDbType.DateTime,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = employee.Fecha
                        },
                        new SqlParameter() {
                            ParameterName = "@EmployeeNumber",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = employee.NumeroEmpleado
                        },
                        new SqlParameter() {
                            ParameterName = "@Curp",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 18,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = employee.CURP
                        },
                        new SqlParameter() {
                            ParameterName = "@Ssn",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 11,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = employee.SSN
                        },
                        new SqlParameter() {
                            ParameterName = "@PhoneNumber",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 10,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = employee.Telefono
                        },
                        new SqlParameter() {
                            ParameterName = "@Nationality",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 50,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = employee.Nacionalidad
                        },
                        new SqlParameter() {
                            ParameterName = "@Id",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Output,
                        }
                };

                using(var command = _sqlServer.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[dbo].[SPR_ADD_EMPLOYEE]";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddRange(parameters);

                    await _sqlServer.Database.OpenConnectionAsync();

                    using (var result = await command.ExecuteReaderAsync())
                    {

                        response = Convert.ToInt32(parameters[8].Value) > 0;
                    }
                }
                return response;
            }
            catch(Exception ex)
            {
                return response;
            }
        }

        public async Task<List<EmployeeDto>> AllEmployees()
        {
            List<EmployeeDto> list = new List<EmployeeDto>();
            try
            {
                using (var command = _sqlServer.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[dbo].[SPR_GET_ALL_EMPLOYEES]";
                    command.CommandType = CommandType.StoredProcedure;

                    await _sqlServer.Database.OpenConnectionAsync();

                    using (var result = await command.ExecuteReaderAsync())
                    {
                        while (result.Read())
                        {
                            var item = new EmployeeDto();
                            item.Id = Convert.ToInt32(result["Id"]);
                            item.Nombres = result["FirstName"].ToString();
                            item.Apellidos = result["LastName"].ToString();
                            item.Fecha = Convert.ToDateTime(result["DateOfBirth"]);
                            item.NumeroEmpleado = Convert.ToInt32(result["EmployeeNumber"]);
                            item.CURP = result["Curp"].ToString();
                            item.SSN = result["Ssn"].ToString();
                            item.Telefono = result["PhoneNumber"].ToString();
                            item.Nacionalidad = result["Nationality"].ToString();
                            list.Add(item);
                        }
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                return list;
            }
        }

        public async Task<bool> Delete(int Id)
        {
            bool response = false;
            try
            {
                var parameters = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@Id",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = Id
                        },
                        new SqlParameter() {
                            ParameterName = "@HasError",
                            SqlDbType =  System.Data.SqlDbType.Bit,
                            Direction = System.Data.ParameterDirection.Output,
                        }
                };

                using (var command = _sqlServer.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[dbo].[SPR_DELETE_EMPLOYEE]";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddRange(parameters);

                    await _sqlServer.Database.OpenConnectionAsync();

                    using (var result = await command.ExecuteReaderAsync())
                    {

                        response = !Convert.ToBoolean(parameters[1].Value);
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                return response;
            }
        }

        public async Task<bool> Update(EmployeeDto employee)
        {
            bool response = false;
            try
            {
                var parameters = new SqlParameter[] {
                    new SqlParameter() {
                        ParameterName = "@Id",
                        SqlDbType =  System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = employee.Id
                    },
                    new SqlParameter() {
                        ParameterName = "@FirstName",
                        SqlDbType =  System.Data.SqlDbType.VarChar,
                        Size = 100,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = employee.Nombres
                    },
                    new SqlParameter() {
                        ParameterName = "@LastName",
                        SqlDbType =  System.Data.SqlDbType.VarChar,
                        Size = 100,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = employee.Apellidos
                    },
                    new SqlParameter() {
                        ParameterName = "@DateOfBirth",
                        SqlDbType =  System.Data.SqlDbType.DateTime,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = employee.Fecha
                    },
                    new SqlParameter() {
                        ParameterName = "@EmployeeNumber",
                        SqlDbType =  System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = employee.NumeroEmpleado
                    },
                    new SqlParameter() {
                        ParameterName = "@Curp",
                        SqlDbType =  System.Data.SqlDbType.VarChar,
                        Size = 18,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = employee.CURP
                    },
                    new SqlParameter() {
                        ParameterName = "@Ssn",
                        SqlDbType =  System.Data.SqlDbType.VarChar,
                        Size = 11,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = employee.SSN
                    },
                    new SqlParameter() {
                        ParameterName = "@PhoneNumber",
                        SqlDbType =  System.Data.SqlDbType.VarChar,
                        Size = 10,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = employee.Telefono
                    },
                    new SqlParameter() {
                        ParameterName = "@Nationality",
                        SqlDbType =  System.Data.SqlDbType.VarChar,
                        Size = 50,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = employee.Nacionalidad
                    },
                    new SqlParameter() {
                        ParameterName = "@HasError",
                        SqlDbType =  System.Data.SqlDbType.Bit,
                        Direction = System.Data.ParameterDirection.Output
                    }
                };

                using (var command = _sqlServer.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[dbo].[SPR_UPDATE_EMPLOYEE]";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddRange(parameters);

                    await _sqlServer.Database.OpenConnectionAsync();

                    using (var result = await command.ExecuteReaderAsync())
                    {

                        response = !Convert.ToBoolean(parameters[9].Value);
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
